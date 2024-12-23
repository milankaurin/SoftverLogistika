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

## Struktura Projekta
Projekat je podeljen na 3 manja pomoćna projekta: **Deljeni podaci**, **SoftverLogistikaBack** i **SoftverLogistikaFront**.

### 1. Deljeni podaci

Projekat DeljeniPodaci predstavlja centralno mesto za definisanje modela podataka koji se koriste kako na backendu, tako i na frontendu. Ovaj projekat eliminiše dupliranje koda i omogućava deljenje zajedničke logike između različitih delova aplikacije. Klasa **Posiljka** je glavna klasa ovog modula i predstavlja osnovni entitet aplikacije. Ona sadrži atribute poput Id-a, naziva, statusa, datuma kreiranja i isporuke... 

Takođe, **Posiljka** uključuje pomoćne metode i rečnik za konverziju statusa u tekstualni format, omogućavajući lakšu manipulaciju podacima u aplikaciji.

### 2. Backend projekat

Backend deo projekta koristi **ASP.NET Core Web API** za obradu zahteva i upravljanje podacima. Projekat je organizovan u sledeće foldere:

#### Struktura foldera:

- **`Properties`**
  - Sadrži fajl `launchSettings.json`, koji definiše konfiguracije za pokretanje aplikacije (lokalni URL-ovi, portovi itd.).

- **`Endpoints`**
  - Sadrži `PosiljkaEndpoints.cs`, gde su definisani svi API endpointi za rad sa pošiljkama.

- **`logs`**
  - Služi za skladištenje logova aplikacije u tekstualnim fajlovima. Ovi fajlovi prate aktivnosti aplikacije i pomažu u dijagnostici.

- **`Services`**
  - Sadrži `PosiljkaService.cs`, gde je implementirana poslovna logika za rad sa pošiljkama, uključujući interakciju između endpointa i baze podataka.

- **`Validators`**
  - Sadrži `PosiljkaValidator.cs`, koji koristi biblioteku **FluentValidation** za validaciju podataka (npr. obavezna polja, validacija datuma i statusa pošiljke).

- **`Program.cs`**
  - Ulazna tačka aplikacije. Ovaj fajl registruje sve servise, middleware komponente, i podešava osnovne API konfiguracije, uključujući integraciju sa **Swagger**-om za automatsku dokumentaciju API-ja.
 
 ### 3. Frontend projekat

 Frontend deo projekta koristi **Blazor WebAssembly** za razvoj interaktivnog korisničkog interfejsa. Projekat je organizovan u sledeće foldere:

#### Struktura foldera:

- **`Properties`**
  - Sadrži fajl `launchSettings.json`, koji definiše konfiguracije za pokretanje aplikacije, uključujući URL-ove i portove za lokalno pokretanje.

- **`wwwroot`**
  - Sadrži statičke resurse aplikacije, poput CSS fajlova, slika, ikonica i drugih statičkih fajlova koji su dostupni klijentu.

- **`Components`**
  - **`PosiljkaForma.razor`**: Komponenta koja pruža formu za kreiranje i izmenu pošiljki.
  - **`PosiljkaTable.razor`**: Komponenta za prikazivanje liste pošiljki u tabelarnom formatu, sa podrškom za akcije kao što su izmena, brisanje i prikaz detalja.

- **`Layout`**
  - **`MainLayout.razor`**: Glavni raspored aplikacije, koji definiše osnovni izgled stranica.
  - **`NavMenu.razor`**: Navigacioni meni koji omogućava lako kretanje kroz aplikaciju.

- **`Pages`**
  - **`DetaljiPosiljke.razor`**: Stranica za prikaz detalja pojedinačne pošiljke.
  - **`Home.razor`**: Početna stranica sa pregledom svih pošiljki.
  - **`IzmenaPosiljke.razor`**: Stranica za izmenu postojećih pošiljki.
  - **`NovaPosiljka.razor`**: Stranica za dodavanje nove pošiljke.

- **`Services`**
  - **`AuthService.cs`**: Servis za simulaciju autorizacije i provere statusa prijave.
  - **`PosiljkaService.cs`**: Servis za komunikaciju sa backend API-jem radi obrade pošiljki (kreiranje, ažuriranje, brisanje, dohvatanje).

- **Ostali fajlovi**
  - **`_Imports.razor`**: Fajl za globalne usluge i namespace-ove korišćene u projektu.
  - **`App.razor`**: Glavna ulazna tačka aplikacije, gde su definisane rute za sve stranice.
  - **`Program.cs`**: Konfiguracija servisa i pokretanje aplikacije.

 


  
