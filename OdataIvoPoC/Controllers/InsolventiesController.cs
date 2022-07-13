using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataIvoPoC.Models;
using OdataIvoPoC.Services;

namespace OdataIvoPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsolventiesController : ControllerBase
    {
        private readonly IInsolventieService _insolventieService;
        public InsolventiesController(IInsolventieService insolventieService)
        {
            this._insolventieService = insolventieService;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Insolventie> Get()
        {
            return _insolventieService.GetAll();
        }

        [HttpGet("odata/Insolventies/{key}")]
        [EnableQuery]
        public IActionResult Get(string key)
        {
            return Ok(_insolventieService.GetById(key).Single());
        }

        [HttpGet("odata/Insolventies/{key}/Publicaties")]
        //[HttpGet("odata/Insolventies({key})/Publicaties")]
        [EnableQuery]
        public IActionResult GetPublicaties(string key)
        {
            return Ok(_insolventieService.GetById(key).Single().Publicaties.AsQueryable());
        }
    }
}
