using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataIvoPoC.Models;
using OdataIvoPoC.Services;

namespace OdataIvoPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicatiesController : ControllerBase
    {
        private readonly IPublicatieService _publicatieService;

        public PublicatiesController(IPublicatieService publicatieService)
        {
            this._publicatieService = publicatieService;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Publicatie> Get()
        {
            return _publicatieService.GetAll();
        }
    }
}
