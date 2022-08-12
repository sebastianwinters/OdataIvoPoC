using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OdataIvoPoC.Models;
using OdataIvoPoC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//OData security guidance: https://docs.microsoft.com/en-us/odata/webapi/odata-security

builder.Services.AddControllers().AddOData(options => options
    .AddRouteComponents("odata", GetConventionModel())
    .Select().Expand().Filter());

builder.Services.AddTransient<IInsolventieService, InsolventieService>();
builder.Services.AddTransient<IPublicatieService, PublicatieService>();
builder.Services.AddTransient<ISchuldenaarService, SchuldenaarService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetConventionModel()
{
    var builder = new ODataConventionModelBuilder();
    var insolventies = builder.EntitySet<Insolventie>("Insolventies");
    var publicaties = builder.EntitySet<Publicatie>("Publicaties");
    var schuldenaars = builder.EntitySet<Schuldenaar>("Schuldenaars");
    var handelsnamen = builder.EntitySet<Handelsnaam>("Handelsnamen");

    var zoekPersoonAction = builder.EntityType<NatuurlijkPersoon>()
        .Collection
        .Action("ZoekPersoon")
        .ReturnsCollectionViaEntitySetPath<NatuurlijkPersoon>("bindingParameter");
    zoekPersoonAction.Parameter<string>("voorvoegsel").Optional();
    zoekPersoonAction.Parameter<string>("achternaam").Optional();
    zoekPersoonAction.Parameter<DateOnly>("geboorteDatum").Optional();
    zoekPersoonAction.Parameter<string>("postcode").Optional();
    zoekPersoonAction.Parameter<int>("huisnummer").Optional();

    var zoekOrganisatieAction = builder.EntityType<Organisatie>()
        .Collection
        .Action("ZoekOrganisatie")
        .ReturnsCollectionViaEntitySetPath<Organisatie>("bindingParameter");
    zoekOrganisatieAction.Parameter<string>("organisatieNaam").Optional();
    zoekOrganisatieAction.Parameter<string>("handelsNaam").Optional();
    zoekOrganisatieAction.Parameter<string>("kvkNummer").Optional();
    zoekOrganisatieAction.Parameter<string>("postcode").Optional();
    zoekOrganisatieAction.Parameter<string>("huisnummer").Optional();

    var model = builder.GetEdmModel();

    return model;
}