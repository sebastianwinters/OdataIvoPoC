namespace OdataIvoPoC.Models
{
    public class EigenaarZonderRechtspersoon : NatuurlijkPersoon
    {
        public string KvKnummer { get; set; }

        public List<Handelsnaam> Handelsnamen { get; set; }
    }
}
