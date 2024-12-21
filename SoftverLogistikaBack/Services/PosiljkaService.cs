using DeljeniPodaci;
namespace SoftverLogistikaBack.Services
{
    public class PosiljkaService
    {
        private readonly List<Posiljka> _posiljke;
        
        public PosiljkaService()
        {
            // Mock lista za pošiljke (In-memory podaci)
            _posiljke = new List<Posiljka> {
        new Posiljka { Id = Guid.NewGuid(), Naziv = "Pošiljka 1", Status = StatusPosiljke.NaPutu, DatumKreiranja = DateTime.Now },
        new Posiljka { Id = Guid.NewGuid(), Naziv = "Pošiljka 2", Status = StatusPosiljke.USkladistu, DatumKreiranja = DateTime.Now },
   
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
