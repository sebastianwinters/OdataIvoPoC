namespace OdataIvoPoC.Models
{
    public class NatuurlijkPersoon : Schuldenaar
    {
        public string Voornaam { get; set; }

        public List<Adres> Adressen { get; set; }
    }
}
