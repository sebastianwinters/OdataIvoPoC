using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public class SchuldenaarService : ISchuldenaarService
    {
        private List<Schuldenaar> _schuldenaars;

        public SchuldenaarService()
        {
            _schuldenaars = new List<Schuldenaar>()
            {
                new NatuurlijkPersoon
                {
                    Id = "np-1",
                    Voorvoegsel = "de",
                    Achternaam = "Winter",
                    GeboorteDatum = new DateOnly(1982, 11, 11),
                    Adressen = new List<Adres>()
                    {
                        new Adres(){ Straat = "mijnstraat", Huisnummer = 98, Postcode = "2211AB", Plaats = "Utrecht" }
                    }
                },
                new Organisatie
                {
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
                },
                new EigenaarZonderRechtspersoon
                {
                    Id = "zp-1",
                    Voorvoegsel = string.Empty,
                    Achternaam = "Jansen",
                    GeboorteDatum = new DateOnly(1976, 2, 19),
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
            };
        }

        public IQueryable<Schuldenaar> GetAll()
        {
            return _schuldenaars.AsQueryable();
        }

        public IQueryable<Schuldenaar> GetById(string id)
        {
            return _schuldenaars.Where(p => p.Id == id).AsQueryable();
        }
    }
}
