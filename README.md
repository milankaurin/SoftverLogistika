# SoftverLogistika - Opis, Struktura, Tehnologije i Uputstvo za testiranje funkcionalnosti. Slabosti projekta i mogućnosti za dalji razvoj.
 
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

### 1. Klonirajte projekat ili ga preuzmite kao zip arhivu i potom otpakujte

### 2. Instalacija potrebnih paketa
U folderu gde se nalazi `.sln` fajl projekta, otvorite terminal i izvršite sledeću komandu da biste instalirali sve potrebne NuGet pakete: 
```bash
dotnet restore
```
### 3. Čišćenje projekta
Pre gradnje projekta, izvršite čišćenje kako biste očistili projekat:
```bash
dotnet clean
```
### 4. Bildovanje (gradnja) projekta
Izvršite sledeću komandu da biste bildovali projekat:
```bash
dotnet build
```
### 5. Pokretanje backend projekta
Za pokretanje backend-a koristite sledeću komandu:
- **HTTPS opcija:**  -- obavezna jer je front end namešten da komunicira sa ovom adresom
 ```bash
dotnet run --project ./SoftverLogistikaBack/SoftverLogistikaBack.csproj --urls "https://localhost:7261"
```
Ako želite odmah da pregledate Swagger u pretraživaču nalepite adresu : [https://localhost:7261/swagger](https://localhost:7261/swagger)
### 6. Pokretanje frontend projekta
Za pokretanje frontend aplikacije, otvorite NOV terminal (i ne isključujte terminal gde je backend) i koristite sledeću komandu:
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

- **Ako ste frontend pokrenuli na prvoj, HTTPS opciji `https://localhost:7036`:**
  Otvorite pretraživač i posetite [https://localhost:7036](https://localhost:7036).

- **Ako ste frontend pokrenuli na drugoj, HTTP opciji `http://localhost:5173`:**
  Otvorite pretraživač i posetite [http://localhost:5173](http://localhost:5173).

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

- **`Components`** -- ponovo iskoristive komponente
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

 ## Korišćene tehnologije i biblioteke

Projekat **SoftverLogistika** koristi različite tehnologije i biblioteke za implementaciju funkcionalnosti i postizanje modularnosti i efikasnosti. Ovo su ključne tehnologije i biblioteke koje su korišćene:

### Backend tehnologije i biblioteke
**ASP.NET Core Web API (Minimal API)**: 
  - Verzija: `8.0`.
  - Minimal API je korišćen za implementaciju backend servisa, pružajući jednostavan i efikasan način za kreiranje RESTful API endpoint-a. Ova arhitektura omogućava lakšu konfiguraciju i manji kod u poređenju sa tradicionalnim kontrolerima, dok zadržava punu funkcionalnost ASP.NET Core ekosistema.
  - Omogućava efikasno upravljanje HTTP zahtevima (GET, POST, PUT, DELETE) i lako integrisanje sa klijentima putem JSON formata.
 
 **FluentValidation**: Ova biblioteka omogućava fleksibilnu validaciju unosa podataka kroz definisanje pravila validacije u modelima.
  
**Serilog.AspNetCore**: Verzija `9.0.0`. Omogućava jednostavno logovanje podataka u tekstualne fajlove unutar `logs` foldera, pomažući u praćenju aktivnosti aplikacije.
  

### Frontend tehnologije i biblioteke
**Blazor WebAssembly**: Frontend aplikacija je razvijena pomoću Blazor WebAssembly tehnologije, što omogućava klijentsko renderovanje i interaktivno korisničko iskustvo.

  - `HttpClient` je konfigurisan sa osnovnim URL-om za komunikaciju sa backend API-jem.
  - **Blazor komponentni model**: Koristi se za izgradnju ponovo upotrebljivih komponenti kao što su tabele (`PosiljkaTable.razor`) i forme (`PosiljkaForma.razor`).
  - **Bootstrap 5**: Koristi se za stilizaciju aplikacije, pružajući unapred definisane komponente za korisnički interfejs.

### Glavne funkcionalnosti implementirane tehnologijama:
1. **Validacija podataka**: `FluentValidation` omogućava proveru unosa na serveru, kao i prilagođene poruke o greškama.
2. **Logovanje**: `Serilog` beleži važne informacije o radu aplikacije u log fajlove.
3. **Dokumentacija API-ja**: Integracija sa Swagger UI omogućava jednostavan pregled i testiranje svih dostupnih endpoint-a.
4. **Komunikacija frontend-backend**: `HttpClient` je konfigurisan da koristi osnovni URL backend API-ja (`https://localhost:7261/api/`) za slanje HTTP zahteva.
5. **Frontend navigacija**: `Blazor` omogućava lakšu navigaciju između stranica uz pomoć komponenti kao što su `NavMenu.razor` i `App.razor`.

 ## Testiranje funkcionalnosti
 
Ovo uputstvo sadrži korake za testiranje osnovnih funkcionalnosti aplikacije.

### 1. Pokretanje aplikacije
Pre nego što započnete testiranje, uverite se da ste uspešno pokrenuli aplikaciju, prateći korake navedene u sekciji "Koraci za pokretanje aplikacije".

### 2. Početna stranica
Početna stranica se učitava sa spiskom postojećih pošiljki. Levo se nalazi **NavMenu** za navigaciju kroz sajt a u gornjem desnom uglu se nalazi dugme **Login** koje služi za prijavljivanje i odjavljivanje. Ovo nam omogućava pristup zaštićenim opcijama za upravljanje postojećim pošiljkama. 

### 3. Kreiranje nove pošiljke
Sa leve strane u okviru **NavMenu** se nalazi dugme ka formi za kreiranje novih pošiljki. U formu unosimo podatke i kreiramo pošiljku. Ako je akcija uspešna dobićemo informaciju o tome i bićemo preusmereni na glavnu stranu gde će nova pošiljka biti dodata na listu. Ukoliko kreiranje nije uspešno dobićemo odgovor (primarno validacija na frontu a potom i na back endu-retko) o tome koje polje u formi nismo pravilno uneli.

### 4. Ažuriranje pošiljke
Na glavnoj strani u tabeli (**ukoliko smo ulogovani**) u koloni Akcije biće prikazane opcije za izmenu, brisanje i ažuriranje pošiljke. Kliknemo dugme **Izmeni** za željenu pošiljku. To nas vodi ka novoj strani gde imamo istu formu kao za kreiranje. Nakon toga je ponašanje isto kao u prethodnom slučaju korišćenja.

### 5. Brisanje pošiljke
Kao i kod ažuriranja, ukoliko smo ulogovani imamo dugme **Obriši** koje briše željenu pošiljku, lista se odmah osvežava pa je promena odmah vidljiva

### 6. Pomoćne funkcionalnosti
#### Pretraga i filtriranje pošiljki
 Iznad tabele imamo textbox gde unosimo tekst po kom pretražujemo pošiljku. Pored textbox-a se nalaze 3 check box-a koji služe za filtriranje pošiljki na osnovu Statusa (Na putu, U Skladištu, Isporučeno).

#### Prikaz detalja pošiljke
 U koloni za akcije imamo dugme **Detalji** koje nas vodi ka novoj stranici gde će nam biti prikazani detalji o pošiljci koji nisu vidljivi u tabeli (konkretno podaci o primaocu i pošiljaocu).

  ## Ranjivost projekta i mogućnost za dalji razvoj
  Iako projekat zadovoljava osnovne funkcionalnosti za upravljanje pošiljkama i pruža dobro korisničko iskustvo, postoje određene slabosti i potencijali za unapređenje:

### REŠENO:  Slabost u sistemu autorizacije

Opis problema: Trenutna implementacija koristi jednu globalnu promenljivu za čuvanje statusa autorizacije. Ovaj pristup uzrokuje probleme kada se koristi više instanci front-enda, jer prijava jednog korisnika utiče na sesiju drugog korisnika.
Mogućnost za unapređenje: Prelazak na korišćenje JWT (JSON Web Token) ili Guid. EDIT: REŠENO korišćenjem Liste jedinstvenog tipa GUID. Radi na nivou SessionStorage

### Ograničena povratna informacija prilikom validacije na backendu

Opis problema: Trenutni sistem validacije na backendu koristi generičke poruke u slučaju grešaka. Iako frontend ima stroge validacione mehanizme, ukoliko dođe do greške koja probije te provere, backend ne vraća specifične informacije o grešci, već samo generičku poruku.
Mogućnost za unapređenje: Poboljšanje validacije na backendu i implementacija povratnih specifičnih poruka grešaka (detalji o poljima koja nisu prošla validaciju). To bi omogućilo korisnicima i developerima jasniju povratnu informaciju i lakše ispravljanje grešaka.

### REŠENO: Problem pri pokretanju preko Visual Studio okruženja  

Opis problema: Kada se projekat pokreće kroz Visual Studio, može doći do situacije u kojoj se frontend učita pre nego što backend postane dostupan. U ovom slučaju, na dnu stranice se pojavljuje poruka o grešci jer aplikacija ne može da povuče podatke sa servera. Problem se rešava osvežavanjem stranice nakon što backend postane dostupan. Kada se projekat otvara putem konzole kako je navedeno u uputstvo, ovaj problem nije očekivan.  Edit: Problem je rešen dodavanjem endpointa. Sada će front-end pokušavati da se poveže sa serverom sve dok server ne postane aktivan. U konzoli imamo informacije o tome da li je konekcija uspostavljena, ukoliko nije, za jednu sekundu će pokušati ponovo

 




  
