namespace OdataIvoPoC.Models
{
    public class Handelsnaam
    {
        public string Id { get; set; }

        public string Naam { get; set; }

        public bool IsVervallen { get; set; }

        public List<Adres> Adressen { get; set; }
    }
}
