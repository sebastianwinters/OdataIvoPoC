using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public interface IPublicatieService
    {
        IQueryable<Publicatie> GetAll();

        IQueryable<Publicatie> GetById(string id);
    }
}
