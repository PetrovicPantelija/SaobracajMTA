"""Staticka analiza: koje forme su dostupne iz zadate pocetne klase (WinForms, C#).

Koraci:
1. Iz svakog .cs fajla uklanja komentare, a stringove i char literale menja oznakama.
2. Pronalazi namespace-ove, klase (i partial delove), metode i kod na nivou klase.
3. Od pocetne klase radi fixpoint: u "zivim" metodama trazi `new T(`, `typeof(T)`,
   staticke reference na pomocne klase i pozive metoda po imenu.
   Event handler je ziv samo ako je negde zakacen (+=) ili pozvan.
"""
import json
import os
import re
import sys
from collections import defaultdict, deque

ROOT = sys.argv[1]
START = sys.argv[2]          # puno ime pocetne klase, npr. Saobracaj.NewMain
OUT = sys.argv[3]
FIRMA = sys.argv[4] if len(sys.argv) > 4 else "Leget"

FORM_BASES = {
    "Form", "System.Windows.Forms.Form", "RibbonForm", "Syncfusion.Windows.Forms.Tools.RibbonForm",
    "Office2010Form", "Syncfusion.Windows.Forms.Office2010Form", "MetroForm",
    "MetroFramework.Forms.MetroForm", "Syncfusion.Windows.Forms.MetroForm", "SfForm",
    "Syncfusion.WinForms.Controls.SfForm", "Office2007Form", "Syncfusion.Windows.Forms.Office2007Form",
}
CONTROL_BASES = {"UserControl", "System.Windows.Forms.UserControl", "Control"}
KEYWORDS = {"if", "for", "foreach", "while", "switch", "catch", "using", "lock", "return", "new",
            "typeof", "sizeof", "nameof", "base", "this", "fixed", "checked", "unchecked", "default"}


def strip(src):
    """Uklanja komentare; stringove menja sa "\x01n\x01" (n = indeks u listi stringova)."""
    out, strings = [], []
    i, n = 0, len(src)
    while i < n:
        c = src[i]
        nx = src[i + 1] if i + 1 < n else ""
        if c == "/" and nx == "/":
            j = src.find("\n", i)
            i = n if j < 0 else j
            continue
        if c == "/" and nx == "*":
            j = src.find("*/", i + 2)
            j = n if j < 0 else j + 2
            out.append("\n" * src.count("\n", i, j))
            i = j
            continue
        if c == "'":
            j = i + 1
            if j < n and src[j] == "\\":
                j += 2
                while j < n and src[j] != "'":
                    j += 1
            else:
                j += 1
            out.append("' '")
            i = j + 1
            continue
        if c in "@$" or c == '"':
            k = i
            verbatim = interp = False
            while k < n and src[k] in "@$":
                verbatim |= src[k] == "@"
                interp |= src[k] == "$"
                k += 1
            if k < n and src[k] == '"' and (k > i or c == '"'):
                j = k + 1
                buf = []
                depth = 0
                while j < n:
                    ch = src[j]
                    if verbatim:
                        if ch == '"':
                            if j + 1 < n and src[j + 1] == '"':
                                buf.append('"'); j += 2; continue
                            if depth == 0:
                                break
                    else:
                        if ch == "\\":
                            buf.append(src[j:j + 2]); j += 2; continue
                        if ch == '"' and depth == 0:
                            break
                        if ch == "\n" and not interp:
                            break
                    if interp:
                        if ch == "{":
                            if j + 1 < n and src[j + 1] == "{":
                                buf.append("{{"); j += 2; continue
                            depth += 1
                        elif ch == "}" and depth > 0:
                            depth -= 1
                    buf.append(ch)
                    j += 1
                strings.append("".join(buf))
                out.append('"\x01%d\x01"' % (len(strings) - 1))
                out.append("\n" * "".join(buf).count("\n"))
                i = j + 1
                continue
        if c == "#" and (i == 0 or src[i - 1] == "\n" or src[:i].split("\n")[-1].strip() == ""):
            j = src.find("\n", i)
            i = n if j < 0 else j
            continue
        out.append(c)
        i += 1
    return "".join(out), strings


def match_braces(text):
    stack, pairs = [], {}
    for i, ch in enumerate(text):
        if ch == "{":
            stack.append(i)
        elif ch == "}" and stack:
            pairs[stack.pop()] = i
    return pairs


RE_NS = re.compile(r"\bnamespace\s+([\w.]+)\s*([{;])")
RE_CLASS = re.compile(r"\b(class|struct|interface|enum)\s+(\w+)\s*(<[^>{]*>)?\s*(?::\s*([^{]+?))?\s*(where\s[^{]*)?\{")
RE_USING = re.compile(r"^\s*using\s+(?:(\w+)\s*=\s*)?([\w.]+)\s*;", re.M)


class Member:
    __slots__ = ("cls", "name", "body", "start", "kind", "live", "regions", "note")

    def __init__(self, cls, name, body, start, kind):
        self.cls, self.name, self.body, self.start, self.kind = cls, name, body, start, kind
        self.live = False
        self.regions = ([], [], [])
        self.note = ""


classes = {}           # fq -> dict(files, bases, members, kind, ns, usings, aliases)
by_simple = defaultdict(set)
file_strings = {}


def split_members(cls_fq, body, base_offset, file):
    """Deli telo klase na metode i kod na nivou klase."""
    members = []
    pairs = match_braces(body)
    i, n = 0, len(body)
    seg_start = 0
    init_parts = []
    while i < n:
        ch = body[i]
        if ch == "{":
            header = body[seg_start:i].strip()
            close = pairs.get(i, n - 1)
            hdr_flat = re.sub(r"\s+", " ", header)
            if re.search(r"\b(class|struct|interface|enum)\s+\w+", hdr_flat):
                pass  # ugnjezdena klasa; obradjuje se posebno
            else:
                h = re.sub(r"\s*:\s*(base|this)\s*\(.*\)\s*$", "", hdr_flat)
                h = re.sub(r"\s+where\s+.*$", "", h)
                name = None
                if h.endswith(")"):
                    depth, k = 0, len(h) - 1
                    while k >= 0:
                        if h[k] == ")":
                            depth += 1
                        elif h[k] == "(":
                            depth -= 1
                            if depth == 0:
                                break
                        k -= 1
                    m = re.search(r"([\w]+)\s*(<[^<>]*>)?\s*$", h[:k])
                    if m and m.group(1) not in KEYWORDS and "=" not in h[:k]:
                        name = m.group(1)
                if name:
                    members.append(Member(cls_fq, name, body[i:close + 1], base_offset + i, "method"))
                else:
                    init_parts.append(header + " " + body[i:close + 1])
            i = close + 1
            seg_start = i
            continue
        if ch == ";":
            stmt = body[seg_start:i]
            flat = re.sub(r"\s+", " ", stmt).strip()
            m = re.match(r"^(.*?\))\s*=>\s*(.*)$", flat, re.S)
            name = None
            if m and "=" not in m.group(1).split("(")[0]:
                h = m.group(1)
                mm = re.search(r"([\w]+)\s*(<[^<>]*>)?\s*\($", h[: h.find("(") + 1])
                if mm and mm.group(1) not in KEYWORDS:
                    name = mm.group(1)
            if name:
                members.append(Member(cls_fq, name, stmt, base_offset + seg_start, "method"))
            else:
                init_parts.append(stmt)
            seg_start = i + 1
        i += 1
    init = Member(cls_fq, "<init>", "\n".join(init_parts), base_offset, "init")
    members.append(init)
    return members


for dirpath, dirnames, filenames in os.walk(ROOT):
    dirnames[:] = [d for d in dirnames if d.lower() not in ("bin", "obj", ".git", ".vs")]
    for fn in filenames:
        if not fn.endswith(".cs"):
            continue
        path = os.path.join(dirpath, fn)
        rel = os.path.relpath(path, ROOT)
        src = open(path, encoding="utf-8-sig", errors="replace").read()
        text, strings = strip(src)
        file_strings[rel] = strings
        usings, aliases = [], {}
        for m in RE_USING.finditer(text):
            if m.group(1):
                aliases[m.group(1)] = m.group(2)
            else:
                usings.append(m.group(2))
        pairs = match_braces(text)
        ns_ranges = []
        for m in RE_NS.finditer(text):
            if m.group(2) == "{":
                o = m.end() - 1
                ns_ranges.append((o, pairs.get(o, len(text)), m.group(1)))
            else:
                ns_ranges.append((m.end(), len(text), m.group(1)))
        cls_ranges = []
        for m in RE_CLASS.finditer(text):
            o = m.end() - 1
            cls_ranges.append((o, pairs.get(o, len(text)), m.group(2), m.group(1), m.group(4)))
        for (o, c, name, kind, bases) in cls_ranges:
            ns = ".".join(r[2] for r in ns_ranges if r[0] < o < r[1])
            outer = [r for r in cls_ranges if r[0] < o < r[1]]
            outer.sort(key=lambda r: r[0])
            fq = ".".join(x for x in [ns] + [r[2] for r in outer] + [name] if x)
            info = classes.setdefault(fq, {"files": set(), "bases": set(), "members": [], "kind": kind,
                                           "ns": ns, "usings": set(), "aliases": {}, "name": name})
            info["files"].add(rel)
            info["usings"].update(usings)
            info["aliases"].update(aliases)
            if bases:
                for b in re.split(r",(?![^<]*>)", bases):
                    b = re.sub(r"<.*>", "", b).strip()
                    if b:
                        info["bases"].add(b)
            body = text[o + 1:c]
            # izbaci ugnjezdene klase iz tela
            for (o2, c2, *_r) in cls_ranges:
                if o < o2 < c:
                    hdr_start = text.rfind("\n", 0, o2)
                    a, b2 = hdr_start - (o + 1), c2 - (o + 1) + 1
                    if a >= 0:
                        body = body[:a] + " " * (b2 - a) + body[b2:]
            for mem in split_members(fq, body, o + 1, rel):
                mem.body = mem.body
                info["members"].append((rel, mem))
            by_simple[name].add(fq)


def ns_chain(ns):
    parts = ns.split(".") if ns else []
    return [".".join(parts[:k]) for k in range(len(parts), -1, -1)]


def resolve(name, ctx_fq):
    """Razresava (eventualno kvalifikovano) ime tipa u kontekstu klase ctx_fq."""
    info = classes[ctx_fq]
    name = re.sub(r"\s+", "", name)
    if name.startswith("global::"):
        name = name[8:]
    first = name.split(".")[0]
    if first in info["aliases"]:
        name = info["aliases"][first] + name[len(first):]
        if name in classes:
            return name
    # ugnjezdene klase u kontekstu
    outer = ctx_fq
    while outer:
        cand = outer + "." + name
        if cand in classes:
            return cand
        outer = outer.rsplit(".", 1)[0] if "." in outer else ""
    for pre in ns_chain(info["ns"]):
        cand = (pre + "." + name) if pre else name
        if cand in classes:
            return cand
    for u in sorted(info["usings"]):
        cand = u + "." + name
        if cand in classes:
            return cand
    return None


def base_chain(fq, seen=None):
    seen = seen or set()
    out = []
    for b in classes[fq]["bases"]:
        r = resolve(b, fq)
        if r and r not in seen:
            seen.add(r)
            out.append(r)
            out.extend(base_chain(r, seen))
        elif not r:
            out.append(b)
    return out


def kind_of(fq):
    chain = base_chain(fq)
    names = set(chain) | {c.split(".")[-1] for c in chain}
    if names & FORM_BASES or any(c in FORM_BASES for c in chain):
        return "form"
    if names & CONTROL_BASES:
        return "control"
    return "class"


KIND = {fq: kind_of(fq) for fq in classes}

RE_NEW = re.compile(r"\bnew\s+((?:global::)?[A-Za-z_][\w]*(?:\s*\.\s*[A-Za-z_]\w*)*)\s*(?:<[^<>()]*>)?\s*[({]")
RE_TYPEOF = re.compile(r"\btypeof\s*\(\s*([\w.]+)\s*\)")
RE_GEN = re.compile(r"<\s*([\w.]+)\s*>\s*\(")
RE_QUAL = re.compile(r"(?<![\w.])([A-Z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\.\s*[A-Za-z_]\w*")
RE_IDENT = re.compile(r"[A-Za-z_]\w*")
RE_DOTCALL_RAW = re.compile(r"(\w+|\))\s*\.\s*([A-Za-z_]\w*)\s*(?=[(;,)])")


class _DotCall:
    """Poziv metode preko drugog objekta (x.M(...) ili x.M kao delegat), bez `this.M` i `base.M`."""
    @staticmethod
    def findall(body):
        return [m.group(2) for m in RE_DOTCALL_RAW.finditer(body) if m.group(1) not in ("this", "base")]


RE_DOTCALL = _DotCall
COND_TOKENS = re.compile(r"\bcompany\b|\bKorisnik\b|\bkorisnik\b|frmLogovanje\.user|\bcompanyosn\b")


RE_FIRM_CMP = re.compile(r'(?:\b[\w]+\s*\.\s*)*\b(Firma|company|companyosn)\b(?:\s*\.\s*\w+\s*\(\s*\))*\s*(==|!=)\s*"([^"]*)"')
RE_FIRM_CMP_R = re.compile(r'"([^"]*)"\s*(==|!=)\s*(?:\b[\w]+\s*\.\s*)*\b(Firma|company|companyosn)\b')
RE_FIRM_EQ = re.compile(r'(?:\b[\w]+\s*\.\s*)*\b(Firma|company|companyosn)\b(?:\s*\.\s*\w+\s*\(\s*\))*\s*\.\s*Equals\s*\(\s*"([^"]*)"\s*\)')
RE_USER = re.compile(r'\b(Korisnik|korisnik|user|User)\b')


def restore(text, strings):
    return re.sub("\x01(\\d+)\x01", lambda m: strings[int(m.group(1))] if int(m.group(1)) < len(strings) else "", text)


def eval_cond(cond):
    """True/False ako uslov zavisi samo od firme; None ako ne moze da se odredi."""
    if not re.search(r"\b(Firma|company|companyosn)\b", cond):
        return None
    e = RE_FIRM_CMP.sub(lambda m: str((m.group(3) == FIRMA) == (m.group(2) == "==")), cond)
    e = RE_FIRM_CMP_R.sub(lambda m: str((m.group(1) == FIRMA) == (m.group(2) == "==")), e)
    e = RE_FIRM_EQ.sub(lambda m: str(m.group(2) == FIRMA), e)
    if re.fullmatch(r"[\sTrueFalse()!&|]*", e):
        py = e.replace("&&", " and ").replace("||", " or ")
        py = re.sub(r"!(?!=)", " not ", py)
        try:
            return bool(eval(py, {"__builtins__": {}}, {"True": True, "False": False}))
        except Exception:
            return None
    return None


def _paren_end(s, i):
    depth = 0
    while i < len(s):
        if s[i] == "(":
            depth += 1
        elif s[i] == ")":
            depth -= 1
            if depth == 0:
                return i
        i += 1
    return len(s) - 1


def _skip_ws(s, i):
    while i < len(s) and s[i] in " \t\r\n":
        i += 1
    return i


def _stmt_end(s, i, pairs):
    i = _skip_ws(s, i)
    if i >= len(s):
        return len(s) - 1
    if s[i] == "{":
        return pairs.get(i, len(s) - 1)
    m = re.match(r"if\s*\(", s[i:])
    if m:
        p = _paren_end(s, i + m.end() - 1)
        e = _stmt_end(s, p + 1, pairs)
        j = _skip_ws(s, e + 1)
        if s.startswith("else", j) and not re.match(r"\w", s[j + 4:j + 5] or " "):
            return _stmt_end(s, j + 4, pairs)
        return e
    depth = 0
    while i < len(s):
        ch = s[i]
        if ch in "({[":
            depth += 1
        elif ch in ")}]":
            depth -= 1
        elif ch == ";" and depth <= 0:
            return i
        i += 1
    return len(s) - 1


def regions(body, strings):
    """Vraca (izvrsava_se_za_firmu, ne_izvrsava_se, zavisi_od_korisnika) kao liste (od, do)."""
    pairs = match_braces(body)
    run, skip, user = [], [], []
    for m in re.finditer(r"\bif\s*\(", body):
        p0 = m.end() - 1
        p1 = _paren_end(body, p0)
        cond = restore(body[p0 + 1:p1], strings)
        then_s = _skip_ws(body, p1 + 1)
        then_e = _stmt_end(body, then_s, pairs)
        j = _skip_ws(body, then_e + 1)
        else_r = None
        if body.startswith("else", j) and not re.match(r"\w", body[j + 4:j + 5] or " "):
            else_r = (j + 4, _stmt_end(body, j + 4, pairs))
        v = eval_cond(cond)
        if v is True:
            run.append((then_s, then_e))
            if else_r:
                skip.append(else_r)
        elif v is False:
            skip.append((then_s, then_e))
            if else_r:
                run.append(else_r)
        elif RE_USER.search(cond):
            user.append((then_s, then_e))
            if else_r:
                user.append(else_r)
    return run, skip, user


def inside(pos, rs):
    return any(a <= pos <= b for a, b in rs)


RE_WIRE = re.compile(r"((?:this\s*\.\s*)?(?:\w+\s*\.\s*)*)(\w+)\s*\+=\s*(?:new\s+[\w.]+\s*\(\s*)?(?:this\s*\.\s*)?(\w+)\s*\)?\s*;")
RE_VIS = re.compile(r"(?:this\s*\.\s*)?(\w+)\s*\.\s*Visible\s*=\s*(true|false)\b")
RE_ADD = re.compile(r"this\s*\.\s*(\w+)(?:\s*\.\s*\w+)*\s*\.\s*(?:Controls|Items|DropDownItems|Panel\.Controls)\s*\.\s*Add(?:Range)?\s*\(([^;]*)\)\s*;")
RE_CMS = re.compile(r"this\s*\.\s*(\w+)\s*\.\s*(?:ContextMenuStrip|ContextMenu)\s*=\s*this\s*\.\s*(\w+)\s*;")


def class_ui(fq):
    """Ugnjezdavanje kontrola, kacenje dogadjaja i kontrole sakrivene za firmu."""
    info = classes[fq]
    parent = {}
    wires = defaultdict(set)     # handler -> kontrole (None = sama forma)
    hidden, shown, user_hidden = set(), set(), set()
    for rel, m in info["members"]:
        strings = file_strings[rel]
        for a in RE_ADD.finditer(m.body):
            for child in re.findall(r"this\s*\.\s*(\w+)", a.group(2)):
                parent.setdefault(child, a.group(1))
        for a in RE_CMS.finditer(m.body):
            parent.setdefault(a.group(2), a.group(1))
        for w in RE_WIRE.finditer(m.body):
            chain = [t for t in re.findall(r"\w+", w.group(1)) if t != "this"]
            ctrl = chain[0] if chain else None
            wires[w.group(3)].add(ctrl)
        run, skip, user = regions(m.body, strings)
        for v in RE_VIS.finditer(m.body):
            if inside(v.start(), run):
                (hidden if v.group(2) == "false" else shown).add(v.group(1))
            elif inside(v.start(), user) and v.group(2) == "false":
                user_hidden.add(v.group(1))
        m.regions = (run, skip, user)
    hidden -= shown

    def is_hidden(c, hs):
        seen = set()
        while c and c not in seen:
            if c in hs:
                return True
            seen.add(c)
            c = parent.get(c)
        return False

    return {"parent": parent, "wires": wires, "hidden": hidden, "user_hidden": user_hidden,
            "is_hidden": is_hidden}


UI = {}

reached = {}            # fq -> (parent_fq, via_method, file)
edges = defaultdict(list)   # src_fq -> [(dst_fq, method, file, cond_snippet)]
called_names = set()
skipped_for_firm = []
live_q = deque()


def mark_live(fq, mem, rel):
    if mem.live:
        return
    mem.live = True
    live_q.append((fq, mem, rel))


def reach(fq, parent, via, rel):
    if fq in reached:
        return
    reached[fq] = (parent, via, rel)
    info = classes[fq]
    ui = UI[fq] = class_ui(fq)
    for r, m in info["members"]:
        live = False
        if m.kind == "init" or m.name == info["name"] or m.name in ("InitializeComponent", "Dispose"):
            live = True
        elif re.match(r"(On[A-Z]\w*|WndProc|ProcessCmdKey|ProcessDialogKey|ToString)$", m.name):
            live = True
        elif m.name in called_names:
            live = True
        elif m.name in ui["wires"]:
            ctrls = ui["wires"][m.name]
            visible = [c for c in ctrls if c is None or not ui["is_hidden"](c, ui["hidden"])]
            if visible:
                live = True
                only_user = all(c is not None and ui["is_hidden"](c, ui["user_hidden"]) for c in visible)
                if only_user:
                    m.note = "samo za odredjene korisnike"
            else:
                m.note = "kontrola sakrivena za " + FIRMA
        if live:
            mark_live(fq, m, r)
    for b in base_chain(fq):
        if b in classes:
            reach(b, fq, "<base>", rel)


def cond_for(body, pos):
    """Vraca uslov (if/else if/?:) koji obuhvata poziciju, ako pominje firmu ili korisnika."""
    window = body[max(0, pos - 600):pos]
    hits = [m for m in re.finditer(r"\bif\s*\(([^{};]*)\)", window)]
    for m in reversed(hits):
        if COND_TOKENS.search(m.group(1)):
            return m.group(1).strip()
    return ""


reach(START, None, "<start>", sorted(classes[START]["files"])[0])

while live_q:
    fq, mem, rel = live_q.popleft()
    body = mem.body
    info = classes[fq]
    targets = []
    for m in RE_NEW.finditer(body):
        targets.append((m.group(1), m.start(), "new"))
    for m in RE_TYPEOF.finditer(body):
        targets.append((m.group(1), m.start(), "typeof"))
    for m in RE_GEN.finditer(body):
        targets.append((m.group(1), m.start(), "generic"))
    for m in RE_QUAL.finditer(body):
        targets.append((m.group(1), m.start(), "static"))
    run_r, skip_r, user_r = mem.regions
    for name, pos, how in targets:
        if inside(pos, skip_r):
            r0 = resolve(name, fq)
            if r0 and KIND.get(r0) == "form":
                skipped_for_firm.append((fq, mem.name, r0, rel))
            continue
        r = resolve(name, fq)
        if not r:
            # mozda je zadnji deo clan (npr. Klasa.Metoda) - probaj prefikse
            parts = re.sub(r"\s+", "", name).split(".")
            for k in range(len(parts) - 1, 0, -1):
                r = resolve(".".join(parts[:k]), fq)
                if r:
                    break
        if not r or r == fq:
            continue
        if how == "static" and KIND.get(r) == "form":
            continue  # staticki pristup formi (npr. frmLogovanje.connectionString) ne otvara formu
        cond = "korisnik" if (inside(pos, user_r) or mem.note) else ""
        edges[fq].append((r, mem.name, rel, cond, how))
        reach(r, fq, mem.name, rel)
    # pozivi metoda po imenu
    idents = set(RE_IDENT.findall(RE_WIRE.sub(" ", body)))
    for r2, m2 in info["members"]:
        if not m2.live and m2.name in idents:
            mark_live(fq, m2, r2)
    # base klase: isto ime u baznim klasama
    for b in base_chain(fq):
        if b in classes:
            for r2, m2 in classes[b]["members"]:
                if not m2.live and m2.name in idents:
                    mark_live(b, m2, r2)
    new_called = set(RE_DOTCALL.findall(body)) - called_names
    if new_called:
        called_names.update(new_called)
        for cfq in list(reached):
            for r2, m2 in classes[cfq]["members"]:
                if not m2.live and m2.name in new_called:
                    mark_live(cfq, m2, r2)

# sve forme u projektu
all_forms = sorted(fq for fq, k in KIND.items() if k == "form")
reached_forms = sorted(fq for fq in reached if KIND.get(fq) == "form")

# mrtvi handleri koji otvaraju forme (za izvestaj)
dead_opens = []
for fq in reached:
    for rel, m in classes[fq]["members"]:
        if m.live or m.kind != "method":
            continue
        for mm in RE_NEW.finditer(m.body):
            r = resolve(mm.group(1), fq)
            if r and KIND.get(r) == "form":
                dead_opens.append((fq, m.name, r, rel, m.note))

result = {
    "start": START,
    "classes_total": len(classes),
    "forms_total": len(all_forms),
    "reached": {fq: {"parent": p, "via": v, "file": f, "kind": KIND.get(fq),
                     "files": sorted(classes[fq]["files"])} for fq, (p, v, f) in reached.items()},
    "edges": {k: v for k, v in edges.items()},
    "all_forms": {fq: sorted(classes[fq]["files"]) for fq in all_forms},
    "reached_forms": reached_forms,
    "dead_opens": dead_opens,
    "skipped_for_firm": skipped_for_firm,
    "notes": {fq + "." + m.name: m.note for fq in reached for _, m in classes[fq]["members"] if m.note and m.live},
    "hidden": {fq: sorted(UI[fq]["hidden"]) for fq in reached if UI.get(fq) and UI[fq]["hidden"]},
    "strings": {},
}
json.dump(result, open(OUT, "w"), ensure_ascii=False, indent=1)
print("klasa:", len(classes), "formi:", len(all_forms), "dostupno klasa:", len(reached),
      "dostupno formi:", len(reached_forms), "mrtvih otvaranja:", len(dead_opens), "preskoceno za firmu:", len(skipped_for_firm))
