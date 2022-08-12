namespace OdataIvoPoC.Models
{
    public class Organisatie : Schuldenaar
    {
        public string Naam { get; set; }

        public string KvKnummer { get; set; }

        public List<Adres> Adressen { get; set; }

        public List<Handelsnaam> Handelsnamen { get; set; }
    }
}
