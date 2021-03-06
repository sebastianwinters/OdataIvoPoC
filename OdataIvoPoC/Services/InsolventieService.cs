using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public class InsolventieService : IInsolventieService
    {
        private List<Insolventie> _insolventieList;

        public InsolventieService(IPublicatieService publicatieService)
        {
            _insolventieList = new List<Insolventie>()
            {
                new Insolventie{ 
                    Id = "INS-1", 
                    Soort = "A", 
                    EindDatum = new DateOnly(2022, 3, 10), 
                    Publicaties = new List<Publicatie>()
                    {
                        publicatieService.GetById("PUB-1").Single(),
                        publicatieService.GetById("PUB-2").Single()
                    },
                    Schuldenaar = new NatuurlijkPersoon{ Id = "np-1", Voornaam = "Sebastian"}
                },
                new Insolventie{
                    Id = "INS-2",
                    Soort = "A",
                    EindDatum = new DateOnly(2022, 4, 23),
                    Publicaties = new List<Publicatie>()
                    {
                        publicatieService.GetById("PUB-3").Single(),
                        publicatieService.GetById("PUB-4").Single()
                    },
                    Schuldenaar = new RechtsPersoon{ Id = "rp-1", KvKnummer = "KVK123"}
                }
            };
        }

        public IQueryable<Insolventie> GetAll()
        {
            return _insolventieList.AsQueryable();
        }

        public IQueryable<Insolventie> GetById(string id)
        {
            return _insolventieList.Where(i => i.Id == id).AsQueryable();
        }
    }
}
