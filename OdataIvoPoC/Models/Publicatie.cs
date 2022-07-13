using System.ComponentModel.DataAnnotations;

namespace OdataIvoPoC.Models
{
    public class Publicatie
    {
        public string Id { get; set; }

        public int Soort { get; set; }

        public DateOnly Datum { get; set; }
    }
}
