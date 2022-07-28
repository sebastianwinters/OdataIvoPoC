using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Edm;
using Microsoft.AspNetCore.OData.Formatter;
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

    var model = builder.GetEdmModel();

    return model;
}