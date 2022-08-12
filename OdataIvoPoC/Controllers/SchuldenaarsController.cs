using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using OdataIvoPoC.Models;
using OdataIvoPoC.Services;
using System.Linq.Expressions;
using System.Xml;

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

        [HttpPost("Persoon")]
        [EnableQuery]
        public IQueryable<NatuurlijkPersoon> ZoekPersoon(ODataActionParameters parameters)
        {
            string? voorvoegsel = null;
            string? achternaam = null;
            DateOnly? geboorteDatum = null;
            string? postcode = null;
            int? huisnummer = null;

            object value;
            if (parameters.TryGetValue("voorvoegsel", out value))
                voorvoegsel = value as string;
            if (parameters.TryGetValue("achternaam", out value))
                achternaam = value as string;
            if (parameters.TryGetValue("geboorteDatum", out value))
                geboorteDatum = DateOnly.FromDateTime((DateTime)(Microsoft.OData.Edm.Date)value);
            if (parameters.TryGetValue("postcode", out value))
                postcode = value as string;
            if (parameters.TryGetValue("huisnummer", out value))
                huisnummer = value as int?;

            Expression<Func<NatuurlijkPersoon, bool>> IsMatch = null;

            //Voorvoegsel, achternaam, geboortedatum
            if (IsMatch == null && !string.IsNullOrWhiteSpace(achternaam) && geboorteDatum.HasValue)
            {
                IsMatch = np =>
                    np.Achternaam.Equals(achternaam, StringComparison.InvariantCultureIgnoreCase) &&
                    np.Voorvoegsel.Equals(voorvoegsel, StringComparison.InvariantCultureIgnoreCase) &&
                    np.GeboorteDatum.Equals(geboorteDatum.Value);
            }

            //Voorvoegsel, achternaam, postcode, huisnummer
            if (IsMatch == null && !string.IsNullOrWhiteSpace(achternaam) && !string.IsNullOrWhiteSpace(postcode) && huisnummer.HasValue)
            {
                IsMatch = np =>
                    np.Achternaam.Equals(achternaam, StringComparison.InvariantCultureIgnoreCase) &&
                    np.Voorvoegsel.Equals(voorvoegsel, StringComparison.InvariantCultureIgnoreCase) &&
                    np.Adressen.Any(a => a.Postcode.Equals(postcode, StringComparison.InvariantCultureIgnoreCase) && a.Huisnummer == huisnummer.Value);
            }

            //Geboortdatum, postcode, huisnummer
            if (IsMatch == null && geboorteDatum.HasValue && !string.IsNullOrWhiteSpace(postcode) && huisnummer.HasValue)
            {
                IsMatch = np =>
                    np.GeboorteDatum.Equals(geboorteDatum.Value) &&
                    np.Adressen.Any(a => a.Postcode.Equals(postcode, StringComparison.InvariantCultureIgnoreCase) && a.Huisnummer == huisnummer.Value);
            }

            if (IsMatch == null)
                return (new List<NatuurlijkPersoon>()).AsQueryable();

            return _schuldenaarService.GetAll()
                .Where(s => s is NatuurlijkPersoon)
                .Cast<NatuurlijkPersoon>()
                .Where(IsMatch);
        }

        [HttpPost("Organisatie")]
        [EnableQuery]
        public IQueryable<Organisatie> ZoekOrganisatie(ODataActionParameters parameters)
        {
            string? organisatieNaam = null;
            string? handelsNaam = null;
            string? kvkNummer = null;
            string? postcode = null;
            int? huisnummer = null;

            object value;
            if (parameters.TryGetValue("organisatieNaam", out value))
                organisatieNaam = value as string;
            if (parameters.TryGetValue("handelsNaam", out value))
                handelsNaam = value as string;
            if (parameters.TryGetValue("kvkNummer", out value))
                kvkNummer = value as string;
            if (parameters.TryGetValue("kvkNumpostcodemer", out value))
                postcode = value as string;
            if (parameters.TryGetValue("huisnummer", out value))
                huisnummer = value as int?;

            Expression<Func<Organisatie, bool>> IsMatch = null;

            //Naam rechtspersoon of handelsnaam
            if (IsMatch == null && (!string.IsNullOrWhiteSpace(organisatieNaam) || !string.IsNullOrWhiteSpace(handelsNaam)))
            {
                IsMatch = rp =>
                    rp.Handelsnamen.Any(hn => hn.Naam.Equals(handelsNaam, StringComparison.InvariantCultureIgnoreCase)) ||
                    rp.Naam.Equals(organisatieNaam, StringComparison.InvariantCultureIgnoreCase);
            }

            //kvkNummer
            if (IsMatch == null && !string.IsNullOrWhiteSpace(kvkNummer))
            {
                IsMatch = rp =>
                    rp.KvKnummer.Equals(kvkNummer, StringComparison.InvariantCultureIgnoreCase);
            }

            //Postcode en huisnummer
            if (IsMatch == null && !string.IsNullOrWhiteSpace(postcode) && huisnummer.HasValue)
            {
                IsMatch = rp =>
                    rp.Adressen.Any(a => a.Postcode.Equals(postcode, StringComparison.InvariantCultureIgnoreCase) && a.Huisnummer == huisnummer.Value);
            }

            if (IsMatch == null)
                return (new List<Organisatie>()).AsQueryable();

            return _schuldenaarService.GetAll()
                .Where(s => s is Organisatie)
                .Cast<Organisatie>()
                .Where(IsMatch);
        }
    }
}
