﻿using Microsoft.AspNetCore.OData;
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

    var action = builder.EntityType<Schuldenaar>()
        .Collection
        .Action("ZoekPersoon")
        .ReturnsCollectionViaEntitySetPath<Schuldenaar>("bindingParameter");
    action.Parameter<string>("voorvoegsel");
    action.Parameter<string>("achternaam");
    var p1 = action.Parameter<DateOnly>("geboorteDatum");
    p1.Optional();

    var model = builder.GetEdmModel();

    return model;
}