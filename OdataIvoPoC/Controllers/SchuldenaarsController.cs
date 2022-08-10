using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using OdataIvoPoC.Models;
using OdataIvoPoC.Services;

namespace OdataIvoPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchuldenaarsController : ControllerBase
    {
        private readonly ISchuldenaarService _schuldenaarService;

        public SchuldenaarsController(ISchuldenaarService schuldenaarService)
        {
            _schuldenaarService = schuldenaarService;
        }

        //[HttpGet]
        //[EnableQuery]
        //public IQueryable<Schuldenaar> Get()
        //{
        //    return _schuldenaarService.GetAll();
        //}

        //public async Task<IHttpActionResult> Rate([FromODataUri] int key, ODataActionParameters parameters)

        [HttpPost]
        public IQueryable<Schuldenaar> ZoekPersoon(ODataActionParameters parameters)
        {
            object value;
            if (!parameters.TryGetValue("voorvoegsel", out value))
                return (new List<Schuldenaar>()).AsQueryable();
            string? voorvoegsel = value as string;

            if (!parameters.TryGetValue("achternaam", out value))
                return (new List<Schuldenaar>()).AsQueryable();
            string? achternaam = value as string;

            if (!parameters.TryGetValue("geboorteDatum", out value))
                return (new List<Schuldenaar>()).AsQueryable();
            DateOnly? geboorteDatum = (DateOnly)value;

            return _schuldenaarService.GetAll()
                .Where(s => s is NatuurlijkPersoon)
                .Cast<NatuurlijkPersoon>()
                .Where(s => s.Voorvoegsel == voorvoegsel && s.Achternaam == achternaam && s.GeboorteDatum == geboorteDatum);
        }
    }
}
