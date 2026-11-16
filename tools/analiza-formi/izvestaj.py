"""Pravi docs/FORME-LEGET.md iz analize dostupnosti formi (pokrece analiza2.py u istom procesu)."""
import os
import re
import runpy
import subprocess
import sys
from collections import defaultdict, deque

ROOT, OUT_MD = sys.argv[1], sys.argv[2]
SKRIPTA = os.path.join(os.path.dirname(os.path.abspath(__file__)), "analiza.py")
sys.argv = [SKRIPTA, ROOT, "Saobracaj.NewMain", os.devnull, "Leget"]
G = runpy.run_path(SKRIPTA, run_name="__main__")


def git_verzija():
    try:
        return subprocess.check_output(["git", "-C", ROOT, "log", "-1", "--format=%h od %cd", "--date=format:%d.%m.%Y."],
                                       text=True).strip()
    except Exception:
        return "nepoznata verzija"

classes, KIND, reached, edges, UI = G["classes"], G["KIND"], G["reached"], G["edges"], G["UI"]
file_strings, restore, dead_opens = G["file_strings"], G["restore"], G["dead_opens"]
skipped = G["skipped_for_firm"]
resolve = G["resolve"]


def main_file(fq):
    fs = sorted(classes[fq]["files"])
    nd = [f for f in fs if not f.endswith(".Designer.cs")]
    return (nd or fs)[0].replace("\\", "/")


def short(fq):
    return fq.split(".")[-1]


def control_text(fq, ctrl):
    for rel, m in classes[fq]["members"]:
        mm = re.search(r"this\s*\.\s*" + re.escape(ctrl) + r"\s*\.\s*Text\s*=\s*\"\x01(\d+)\x01\"", m.body)
        if mm:
            s = file_strings[rel][int(mm.group(1))].strip()
            if s:
                return s
    return None


def caption(src, method, dst):
    """Kako se iz forme src otvara dst: naziv kartice, tekst dugmeta ili ime metode."""
    for rel, m in classes[src]["members"]:
        if m.name != method:
            continue
        mm = re.search(r"\"\x01(\d+)\x01\"\s*,\s*\(\s*\)\s*=>\s*new\s+[\w.]*\b" + re.escape(short(dst)) + r"\b", m.body)
        if mm:
            return "kartica „%s“" % file_strings[rel][int(mm.group(1))].strip()
    ui = UI.get(src)
    if ui and method in ui["wires"]:
        fallback = None
        for c in sorted(x for x in ui["wires"][method] if x):
            t = control_text(src, c)
            ev = event_of(src, c, method)
            if t and t != c:
                t = re.sub(r"\s+", " ", t)
                return "„%s“" % t
            if ev and "DoubleClick" in ev:
                fallback = fallback or "dvoklik u `%s`" % c
            elif ev:
                fallback = fallback or "`%s` (%s)" % (c, ev)
        if fallback:
            return fallback
    if method.endswith("KeyDown") or method.endswith("KeyUp"):
        return "prečica na tastaturi (`%s`)" % method
    if method in ("<init>", short(src)):
        return "pri otvaranju forme"
    if method == "<base>":
        return "bazna klasa"
    return "metoda `%s`" % method


def event_of(fq, ctrl, method):
    for rel, m in classes[fq]["members"]:
        mm = re.search(r"\b" + re.escape(ctrl) + r"\s*\.\s*(\w+)\s*\+=\s*(?:new\s+[\w.]+\s*\(\s*)?(?:this\s*\.\s*)?" + re.escape(method) + r"\b", m.body)
        if mm:
            return mm.group(1)
    return None


# graf: samo ivice ka klasama koje su dostupne
adj = defaultdict(list)
for s, lst in edges.items():
    for d, m, f, c, h in lst:
        if d in reached and d != s and d != "Saobracaj.NewMain":
            adj[s].append((d, m, c))

nm_edges = [(d, m) for d, m, f, c, h in edges["Saobracaj.NewMain"] if KIND.get(d) == "form"]
nm_ui = UI["Saobracaj.NewMain"]
modules = []
seen_roots = set()
for d, m in nm_edges:
    if d in seen_roots:
        continue
    seen_roots.add(d)
    btn = None
    for c in sorted(x for x in nm_ui["wires"].get(m, []) if x):
        btn = control_text("Saobracaj.NewMain", c)
    if m == "ShowHome":
        btn = "Početni ekran (Dashboard)"
    if m == "button1_Click":
        btn = "Dugme „!!!“ (samo korisnik test)"
    modules.append((btn or m, d, m))


def bfs(root):
    parent = {root: None}
    order = [root]
    q = deque([root])
    while q:
        x = q.popleft()
        for y, m, c in adj[x]:
            if y not in parent:
                parent[y] = (x, m, c)
                order.append(y)
                q.append(y)
    return parent, order


def form_tree(root, parent):
    """Stablo samo od formi: pomocne klase se preskacu, a njihova deca vezuju za najblizu formu."""
    kids = defaultdict(list)
    via = {}
    for n, p in parent.items():
        if p is None or KIND.get(n) != "form":
            continue
        x, m, c = p
        chain_m, chain_c = m, c
        while x is not None and KIND.get(x) != "form":
            px = parent.get(x)
            if px is None:
                break
            x, chain_m, chain_c = px[0], px[1], px[2] or chain_c
        if x is None:
            continue
        kids[x].append(n)
        via[n] = (x, chain_m, chain_c)
    return kids, via


form_modules = defaultdict(list)
out = []
w = out.append
total_forms = sum(1 for k in KIND.values() if k == "form")
reached_forms = sorted((fq for fq in reached if KIND.get(fq) == "form"), key=lambda x: short(x).lower())

mod_data = []
for title, root, m in modules:
    parent, order = bfs(root)
    forms = [x for x in order if KIND.get(x) == "form"]
    for f in forms:
        form_modules[f].append(title)
    mod_data.append((title, root, m, parent, forms))

w("# Forme dostupne za Leget (ulaz: NewMain)")
w("")
w("Ovaj dokument pokazuje koje forme korisnik firme Leget može da otvori iz novog glavnog menija `NewMain`, i kojim putem. "
  "Napravljen je statičkom analizom koda (opis na kraju) i ne menja ništa u projektu.")
w("")
w("## Ukratko")
w("")
w("- U projektu postoji **%d formi**. Iz `NewMain` je za Leget dostupno **%d** (%d %%)." %
  (total_forms, len(reached_forms), round(100 * len(reached_forms) / total_forms)))
w("- Ostale forme se iz novog menija za Leget ne mogu otvoriti: pripadaju starom meniju `MainP`, drugim firmama ili su mrtav kod.")
w("- Šta korisnik zaista vidi zavisi još i od njegovih prava u bazi (tabele `MainNovi`, `MainNoviPrava` i kartice). Analiza pokazuje šta kod dozvoljava.")
w("- Za redizajn u stilu Sava ovo je realan obim posla: %d formi umesto %d." % (len(reached_forms), total_forms))
w("")
w("## Put do NewMain")
w("")
w("1. `Program.cs` u folderu programa nalazi `Leget.txt` i otvara `Sifarnici/frmLogovanjeSecond` („Prijava na Leget plus nalog“).")
w("2. Na prijavi postoje dva polja za izbor izgleda:")
w("   - „Stari izgled“ (podrazumevano uključeno) otvara stari glavni meni `MainP`. On nije deo ove analize.")
w("   - „Novi izgled“ otvara `MainLeget/NewMain`.")
w("   - Ako su uključena oba, otvaraju se oba menija.")
w("3. Klik na naslov „Prijava na Leget plus nalog“ otvara `Administracija/frmSistemskaTabela`. To je skrivena prečica, dostupna i pre prijave.")
w("")
w("## Levi meni u NewMain")
w("")
w("| Dugme | Prva forma | Formi u modulu |")
w("|---|---|---|")
btn_order = ["Logistika uvoza", "Logistika izvoza", "Logistika direktnih klijenata", "Drumski transport",
             "Železnički transport", "Operacije skladišta", "Intermodalni terminal", "Prijem i otprema vozova",
             "Prijem i otprema kamiona", "Depo", "Operacije pretovari", "PTI", "Održavanje", "Kapija", "Finansije",
             "Podešavanja sistema", "Administracija"]
by_title = {t.strip(): (root, forms) for t, root, m, parent, forms in mod_data}
for b in btn_order:
    if b in by_title:
        root, forms = by_title[b]
        w("| %s | `%s` | %d |" % (b, short(root), len(forms)))
    else:
        w("| %s | — | 0 (dugme samo menja boju, ne otvara formu) |" % b)
w("")
w("Još u istom prozoru:")
w("")
for t, root, m, parent, forms in mod_data:
    if t.strip() not in btn_order:
        if m == "button1_Click":
            w("- Dugme „!!!“ u zaglavlju otvara `%s` (%d forma). Vidljivo je samo korisniku `test`." % (short(root), len(forms)))
        else:
            w("- %s: `%s`." % (t, short(root)))
w("- Dugme „VSD“ otvara `VSD/VSDMain`, ali je sakriveno za sve firme osim VSD. Zato ga ova lista ne računa.")
w("- „Početni meni“, „Dashboard“, „Povratak na predhodni meni“ i „Logout“ ne otvaraju nove forme.")
w("")
w("## Forme po modulima")
w("")
w("Stablo pokazuje prvi put kojim se stiže do forme: ispod svake forme su forme koje ona otvara, uz natpis dugmeta ili naziv kartice. "
  "Do iste forme se često stiže i drugim putem; sva mesta su u abecednoj listi ispod.")
for title, root, m, parent, forms in mod_data:
    if m == "ShowHome":
        continue
    w("")
    w("### %s (%d)" % (title.strip(), len(forms)))
    w("")
    kids, via = form_tree(root, parent)

    def rec(n, depth, seen):
        label = "**%s** — `%s`" % (short(n), main_file(n))
        if n in via:
            x, mm, cc = via[n]
            label += " — " + caption(x, mm, n)
            if cc:
                label += " (samo za određene korisnike)"
        w("  " * depth + "- " + label)
        for k in sorted(kids.get(n, []), key=lambda z: short(z).lower()):
            if k not in seen:
                seen.add(k)
                rec(k, depth + 1, seen)

    rec(root, 0, {root})

w("")
w("## Sve dostupne forme (abecedno)")
w("")
w("| Forma | Fajl | Modul |")
w("|---|---|---|")
for f in reached_forms:
    mods = sorted(set(t.strip() for t in form_modules.get(f, [])))
    w("| %s | `%s` | %s |" % (short(f), main_file(f), ", ".join(mods) or "—"))

w("")
w("## Forme do kojih kod vodi, ali za Leget nisu dostupne")
w("")
hidden = [x for x in dead_opens if x[4] and x[2] not in reached]
dead = [x for x in dead_opens if not x[4] and x[2] not in reached]
skip = [x for x in skipped if x[2] not in reached and KIND.get(x[2]) == "form"]


def uniq(rows):
    s, outl = set(), []
    for r in rows:
        k = (r[0], r[1], r[2])
        if k not in s:
            s.add(k)
            outl.append(r)
    return outl


w("Ove forme se ne mogu otvoriti iz `NewMain` za Leget, iako ih neka dostupna forma pominje. "
  "U listi su samo forme kojima nijedan drugi put nije otvoren.")
w("")
w("**Kontrola je sakrivena za Leget.** Najčešće je to stara traka menija `meniHeader`, koja se za Leget krije, a umesto nje se prikazuje `panelHeader` sa dugmadima.")
w("")
w("| Forma | Kroz | Otvara |")
w("|---|---|---|")
for c, mth, t, rel, note in sorted(uniq(hidden), key=lambda r: (short(r[2]).lower(), short(r[0]).lower())):
    w("| %s | %s → %s | `%s` |" % (short(t), short(c), caption(c, mth, t), main_file(t)))
w("")
w("**Kod se izvršava samo za druge firme.** Grana `if (Firma == ...)` koja za Leget ne važi.")
w("")
w("| Forma | Kroz | Otvara |")
w("|---|---|---|")
for c, mth, t, rel in sorted(uniq(skip), key=lambda r: short(r[2]).lower()):
    w("| %s | %s → `%s` | `%s` |" % (short(t), short(c), mth, main_file(t)))
w("")
w("**Handler nije zakačen ni za jednu kontrolu (mrtav kod).** Metoda postoji i otvara formu, ali je ništa ne poziva.")
w("")
w("| Forma | Mrtva metoda | Otvara |")
w("|---|---|---|")
for c, mth, t, rel, note in sorted(uniq(dead), key=lambda r: (short(r[2]).lower(), short(r[0]).lower())):
    w("| %s | %s → `%s` | `%s` |" % (short(t), short(c), mth, main_file(t)))

w("")
w("## Kako je rađena analiza")
w("")
w("- Analiza polazi od `NewMain` i prati svaki `new Forma(...)`, uključujući i one u lambdama, npr. `OtvoriFormuSaPravom(\"Kartica\", () => new X())`. Prati i pomoćne klase koje otvaraju forme.")
w("- Komentarisan kod se ne računa.")
w("- Metoda se računa samo ako je zakačena za događaj kontrole (`+=` u Designer-u) ili je poziva drugi kod koji se izvršava.")
w("- Uslovi `Firma == \"...\"` i `company == \"...\"` se računaju kao za Leget. Grane za druge firme se ne računaju.")
w("- Kontrole koje se za Leget sakrivaju (`X.Visible = false` u Leget grani) ne računaju se, kao ni sve unutar njih: meniji, stavke, kontekstni meniji.")
w("- Ograničenja:")
w("  - Prava korisnika iz baze nisu uzeta u obzir.")
w("  - Kontrola koja se sakriva ili prikazuje u zavisnosti od podataka u toku rada računa se kao vidljiva.")
w("  - Do iste forme može da se stigne i iz starog menija `MainP`, ali to ovde nije prikazano.")
w("- Analiza je rađena nad verzijom koda %s (grana `master`). Skripta je u `tools/analiza-formi/` i može ponovo da se pokrene posle izmena u kodu." % git_verzija())
w("")
open(OUT_MD, "w", encoding="utf-8").write("\n".join(out) + "\n")
print("napisano", OUT_MD, len(out), "redova")
