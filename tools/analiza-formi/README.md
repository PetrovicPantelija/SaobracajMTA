# Analiza formi

Pravi `docs/FORME-LEGET.md`: listu svih formi koje se za Leget mogu otvoriti iz glavnog menija `NewMain`, sa putem do svake forme.

Pokreće se iz korena projekta (potreban je Python 3, bez dodatnih paketa):

```
python tools\analiza-formi\izvestaj.py Saobracaj docs\FORME-LEGET.md
```

- `analiza.py` čita sav C# kod i pravi graf: koja forma otvara koju. Prati samo kod koji se stvarno izvršava (zakačeni handleri, grane za firmu Leget, kontrole koje nisu sakrivene).
- `izvestaj.py` pokreće analizu i piše Markdown izveštaj.

Analiza samo čita kod i ništa u projektu ne menja.
