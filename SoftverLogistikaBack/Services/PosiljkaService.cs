using DeljeniPodaci;
namespace SoftverLogistikaBack.Services
{
    public class PosiljkaService
    {
        private readonly List<Posiljka> _posiljke;
        
        public PosiljkaService()
        {
            // Mock lista za pošiljke 
            _posiljke = new List<Posiljka> {
          new Posiljka { Id = Guid.NewGuid(), Naziv = "HP Laptop", Status = StatusPosiljke.NaPutu, DatumKreiranja = DateTime.Now.AddDays(-3), Posiljalac = "Milan Kaurin", Primalac="Dejan Petrovic" },
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Canon Štampač", Status = StatusPosiljke.USkladistu, DatumKreiranja = DateTime.Now.AddDays(-10), Posiljalac = "Marko Jovanović", Primalac="Ana Petrović"},
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Samsung Telefon", Status = StatusPosiljke.Isporuceno, DatumKreiranja = DateTime.Now.AddDays(-7), DatumIsporuke = DateTime.Now.AddDays(-1), Posiljalac = "Nikola Stojanović", Primalac="Milica Nikolić" },
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Dell Monitor", Status = StatusPosiljke.NaPutu, DatumKreiranja = DateTime.Now.AddDays(-1),Posiljalac = "Ivan Đorđević", Primalac="Jelena Ilić" },
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Kingston SSD", Status = StatusPosiljke.Isporuceno, DatumKreiranja = DateTime.Now.AddDays(-5), DatumIsporuke = DateTime.Now.AddDays(-2) , Posiljalac = "Stefan Savić", Primalac="Marija Milenković"},
    new Posiljka { Id = Guid.NewGuid(), Naziv = "TP-Link Ruter", Status = StatusPosiljke.USkladistu, DatumKreiranja = DateTime.Now.AddDays(-15) , Posiljalac = "Luka Kovačević", Primalac="Dragana Marković"},
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Xiaomi Narukvica", Status = StatusPosiljke.NaPutu, DatumKreiranja = DateTime.Now.AddDays(-2), Posiljalac = "Aleksandar Ristić", Primalac="Ivana Lukić" },
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Logitech Tastatura", Status = StatusPosiljke.Isporuceno, DatumKreiranja = DateTime.Now.AddDays(-20), DatumIsporuke = DateTime.Now.AddDays(-3), Posiljalac = "Filip Đukić", Primalac="Katarina Popović" },
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Lenovo Laptop", Status = StatusPosiljke.USkladistu, DatumKreiranja = DateTime.Now.AddDays(-8) , Posiljalac = "Bojan Milošević", Primalac="Tijana Radović"},
    new Posiljka { Id = Guid.NewGuid(), Naziv = "Asus Monitor", Status = StatusPosiljke.NaPutu, DatumKreiranja = DateTime.Now.AddDays(-6), Posiljalac = "Nenad Živanović", Primalac="Sara Vuković" }

            };
        }

        public IEnumerable<Posiljka> GetAll()
        {
            return _posiljke;
        }



        public Posiljka? GetById(Guid id)
        {
            return _posiljke.FirstOrDefault(p => p.Id == id);
        }

        public Posiljka Create(Posiljka novaPosiljka)
        {
            novaPosiljka.Id = Guid.NewGuid();
            _posiljke.Add(novaPosiljka);
            return novaPosiljka;
        }

        public bool Update(Guid id, Posiljka izmenjenaPosiljka)
        {
            var posiljka = GetById(id);
            if (posiljka == null) return false;

            posiljka.Naziv = izmenjenaPosiljka.Naziv;
            posiljka.Status = izmenjenaPosiljka.Status;
            posiljka.DatumIsporuke = izmenjenaPosiljka.DatumIsporuke;
            posiljka.Posiljalac = izmenjenaPosiljka.Posiljalac;
            posiljka.Primalac = izmenjenaPosiljka.Primalac;
            return true;
        }

        public bool Delete(Guid id)
        {
            var posiljka = GetById(id);
            if (posiljka == null) return false;

            _posiljke.Remove(posiljka);
            return true;
        }


    }
}
