# SoftverLogistika
 
## Kratak opis projekta

SoftverLogistika je sistem za praćenje pošiljki u logistici razvijen kao rešenje projektnog zadataka za praksu .NET developera. Cilj projekta je da omogući osnovnu funkcionalnost upravljanja pošiljkama korišćenjem modernih tehnologija i principa clean architecture. Aplikacija je implementirana sa ASP.NET (Web api) Minimal API za backend i Blazor WebAssembly (WASM) za frontend.

### Glavne funkcionalnosti:
- Prikaz liste svih pošiljki.
- Detaljan pregled pojedinačne pošiljke.
- Dodavanje, izmena i brisanje pošiljki.
- Validacija unosa podataka na klijentskoj i serverskoj strani.
- Pretraga i filtriranje pošiljki po nazivu i statusu.
- Osnovna autorizacija za upravljanje podacima.
- Minimalistički dizajn sa pastelno obojenim akcentima.

### Ključni aspekti implementacije:
- Backend implementira REST API sa CRUD operacijama koristeći mock podatke.
- Frontend omogućava dinamičnu navigaciju između stranica i sinhronizaciju validacije.
- Korisnička interakcija je unapređena porukama o ishodu i greškama.

## Koraci za pokretanje aplikacije

Da biste pokrenuli aplikaciju, pratite sledeće korake:

### 1. Instalacija potrebnih paketa
U folderu gde se nalazi `.sln` fajl projekta, otvorite terminal i izvršite sledeću komandu da biste instalirali sve potrebne NuGet pakete: 
```bash
dotnet restore
```
### 2. Čišćenje projekta
Pre gradnje projekta, izvršite čišćenje kako biste očistili projekat:
```bash
dotnet clean
```
### 3. Bildovanje (gradnja) projekta
Izvršite sledeću komandu da biste bildovali projekat:
```bash
dotnet build
```
### 4. Pokretanje backend projekta
Za pokretanje backend-a koristite sledeću komandu:
- **HTTPS opcija:**  -- obavezna jer je front end namešten da komunicira sa ovom adresom
 ```bash
dotnet run --project ./SoftverLogistikaBack/SoftverLogistikaBack.csproj --urls "https://localhost:7261"
```
### 5. Pokretanje frontend projekta
Za pokretanje frontend aplikacije, koristite sledeću komandu:
- **HTTPS opcija:**
 ```bash
dotnet run --project ./SoftverLogistikaFront/SoftverLogistikaFront.csproj --urls "https://localhost:7036"
```
- **HTTP opcija:**
 ```bash
dotnet run --project ./SoftverLogistikaFront/SoftverLogistikaFront.csproj"
```
### 6. Pregled sajta

Nakon što pokrenete aplikaciju, možete pristupiti frontend delu aplikacije u svom pretraživaču unošenjem odgovarajuće adrese, u zavisnosti od opcije koju ste odabrali prilikom pokretanja:

- **Ako ste frontend pokrenuli na `http://localhost:5173`:**
  Otvorite pretraživač i posetite [http://localhost:5173](http://localhost:5173).

- **Ako ste frontend pokrenuli na `https://localhost:7036`:**
  Otvorite pretraživač i posetite [https://localhost:7036](https://localhost:7036).

### 7. Swagger dokumentacija

Ako želite da pregledate ili testirate API endpoint-ove, možete koristiti Swagger UI. Nakon što pokrenete backend na `https://localhost:7261`, otvorite pretraživač i posetite sledeću adresu:

- [https://localhost:7261/swagger](https://localhost:7261/swagger)

Swagger UI omogućava vizuelno testiranje API endpoint-ova i pregled dokumentacije.

 


  
