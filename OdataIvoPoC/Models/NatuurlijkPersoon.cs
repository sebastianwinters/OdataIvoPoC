namespace OdataIvoPoC.Models
{
    public class NatuurlijkPersoon : Schuldenaar
    {
        public string Voorvoegsel { get; set; }

        public string Achternaam { get; set; }

        public DateOnly GeboorteDatum { get; set; }

        public List<Adres> Adressen { get; set; }
    }
}
