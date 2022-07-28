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
                    Schuldenaar = new NatuurlijkPersoon
                    { 
                        Id = "np-1", 
                        Voornaam = "Sebastian",
                        Adressen = new List<Adres>()
                        {
                            new Adres(){ Straat = "mijnstraat", Huisnummer = 98, Postcode = "2211AB", Plaats = "Utrecht" }
                        }
                    }
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
                    Schuldenaar = new Organisatie{ 
                        Id = "rp-1", 
                        KvKnummer = "KVK123",
                        Adressen = new List<Adres>()
                        {
                            new Adres(){ Straat = "orgstraat", Huisnummer = 11, Postcode = "3322CD", Plaats = "Utrecht" },
                            new Adres(){ Straat = "orgstraat", Huisnummer = 3, Postcode = "4411CD", Plaats = "Utrecht" }
                        },
                        Handelsnamen = new List<Handelsnaam>()
                        { 
                            new Handelsnaam()
                            { 
                                Id = "han-1",
                                Naam = "handelsnaam 1", 
                                IsVervallen = true,
                                Adressen = new List<Adres>()
                                {
                                    new Adres(){ Straat = "handelstraatX", Huisnummer = 43, Postcode = "7654CD", Plaats = "Utrecht" },
                                    new Adres(){ Straat = "handelstraatY", Huisnummer = 65, Postcode = "4567CD", Plaats = "Amsterdam" }
                                }
                            },
                            new Handelsnaam()
                            {
                                Id = "han-2",
                                Naam = "handelsnaam 2",
                                IsVervallen = true,
                                Adressen = new List<Adres>()
                                {
                                    new Adres(){ Straat = "handelstraatA", Huisnummer = 21, Postcode = "4343WE", Plaats = "Groninigen" },
                                    new Adres(){ Straat = "handelstraatB", Huisnummer = 909, Postcode = "2121GF", Plaats = "Rotterdam" }
                                }
                            }
                        } 
                    }
                },
                new Insolventie{
                    Id = "INS-3",
                    Soort = "A",
                    EindDatum = new DateOnly(2022, 2, 23),
                    Publicaties = new List<Publicatie>()
                    {
                        publicatieService.GetById("PUB-5").Single()
                    },
                    Schuldenaar = new EigenaarZonderRechtspersoon
                    {
                        Id = "zp-1",                        
                        Voornaam = "Jan-Willem",
                        Adressen = new List<Adres>()
                        {
                            new Adres(){ Straat = "zijnstraat", Huisnummer = 92, Postcode = "6743BC", Plaats = "Zeist" }
                        },
                        KvKnummer = "KVK999",
                        Handelsnamen = new List<Handelsnaam>()
                        {
                            new Handelsnaam()
                            {
                                Id = "han-3",
                                Naam = "handelsnaam 3",
                                IsVervallen = true,
                                Adressen = new List<Adres>()
                                {
                                    new Adres(){ Straat = "zijnstraat", Huisnummer = 92, Postcode = "6743BC", Plaats = "Zeist" }
                                }
                            }
                        }
                    }
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
