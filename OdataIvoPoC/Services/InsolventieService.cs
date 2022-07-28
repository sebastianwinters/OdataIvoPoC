using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public class InsolventieService : IInsolventieService
    {
        private List<Insolventie> _insolventieList;

        public InsolventieService(IPublicatieService publicatieService, ISchuldenaarService schuldenaarService)
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
                    Schuldenaar = schuldenaarService.GetById("np-1").Single()
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
                    Schuldenaar = schuldenaarService.GetById("rp-1").Single()
                },
                new Insolventie{
                    Id = "INS-3",
                    Soort = "A",
                    EindDatum = new DateOnly(2022, 2, 23),
                    Publicaties = new List<Publicatie>()
                    {
                        publicatieService.GetById("PUB-5").Single()
                    },
                    Schuldenaar = schuldenaarService.GetById("zp-1").Single()
                },
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
