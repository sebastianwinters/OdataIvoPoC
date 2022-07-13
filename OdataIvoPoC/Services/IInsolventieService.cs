using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public interface IInsolventieService
    {
        IQueryable<Insolventie> GetAll();

        IQueryable<Insolventie> GetById(string id);
    }
}
