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

    var model = builder.GetEdmModel();

    //var insovlentiesSet = (EdmEntitySet)model.FindDeclaredEntitySet("Insolventies");
    //var publicatiesSet = (EdmEntitySet)model.FindDeclaredEntitySet("Publicaties");
    //var insovlentieType = (EdmEntityType)model.FindDeclaredType("OdataIvoPoC.Models.Insolventie");
    //var publicatieType = (EdmEntityType)model.FindDeclaredType("OdataIvoPoC.Models.Publicatie");

    ////
    //// Add the Customer@odata.navigationLink property
    ////
    //var publicatieProperty = new EdmNavigationPropertyInfo();
    //publicatieProperty.TargetMultiplicity = EdmMultiplicity.Many;
    //publicatieProperty.Target = publicatieType;
    //publicatieProperty.ContainsTarget = false;
    //publicatieProperty.OnDelete = EdmOnDeleteAction.None;
    //publicatieProperty.Name = "Publicaties";

    //var navigationProperty = insovlentieType.AddUnidirectionalNavigation(publicatieProperty);
    //insovlentiesSet.AddNavigationTarget(navigationProperty, publicatiesSet);

    ////
    //// Enable Odata endpoint
    ////
    //var linkBuilder = EdmModelLinkBuilderExtensions.GetNavigationSourceLinkBuilder(model, insovlentiesSet);

    //linkBuilder.AddNavigationPropertyLinkBuilder(navigationProperty,
    //    new NavigationLinkBuilder((context, property) =>
    //        context.GenerateNavigationPropertyLink(property, false), false));

    return model;
}