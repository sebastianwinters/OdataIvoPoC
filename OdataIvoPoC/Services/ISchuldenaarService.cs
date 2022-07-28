using OdataIvoPoC.Models;

namespace OdataIvoPoC.Services
{
    public interface ISchuldenaarService
    {
        IQueryable<Schuldenaar> GetAll();

        IQueryable<Schuldenaar> GetById(string id);

    }
}
