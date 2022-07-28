using System.ComponentModel.DataAnnotations;

namespace OdataIvoPoC.Models
{
    public class Insolventie
    {
        public string Id { get; set; }
        
        public string Soort { get; set; }

        public DateOnly EindDatum { get; set; }

        public List<Publicatie> Publicaties { get; set; }

        public Schuldenaar Schuldenaar { get; set; }
    }
}
