using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public class PublicatieService : IPublicatieService
    {
        private List<Publicatie> _publicatieList;

        public PublicatieService()
        {
            _publicatieList = new List<Publicatie>() 
            { 
                new Publicatie{ Id = "PUB-1", Soort = 1, Datum = new DateOnly(2022, 01, 11) },
                new Publicatie{ Id = "PUB-2", Soort = 1, Datum = new DateOnly(2022, 02, 01) },
                new Publicatie{ Id = "PUB-3", Soort = 1, Datum = new DateOnly(2022, 02, 14) },
                new Publicatie{ Id = "PUB-4", Soort = 1, Datum = new DateOnly(2022, 02, 21) },
                new Publicatie{ Id = "PUB-5", Soort = 1, Datum = new DateOnly(2022, 01, 15) }
            };
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Publicatie> GetAll()
        {
            return _publicatieList.AsQueryable();
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Publicatie> GetById(string id)
        {
            return _publicatieList.Where(p => p.Id == id).AsQueryable();
        }
    }
}
