using PChouse.PTPostalCode.database;
using DistrictModel = PChouse.PTPostalCode.Models.District.DistrictModel;
using AddressModel = PChouse.PTPostalCode.Models.Address.AddressModel;
using PChouse.PTPostalCode.Models.Address;
using log4net.Config;
using Microsoft.AspNetCore.Routing.Constraints;
using PChouse.PTPostalCode.Models.County;
using PChouse.PTPostalCode.Models.PostalCode;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Host.UseSystemd();

XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

builder.Logging.AddLog4Net();
builder.Services.AddHttpLogging(logging =>
{

    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNHibernate(builder.Configuration);
builder.Services.AddScoped<AddressModel, AddressModel>();
builder.Services.AddScoped<DistrictModel, DistrictModel>();
builder.Services.AddScoped<CountyModel, CountyModel>();
builder.Services.AddScoped<PostalCodeModel, PostalCodeModel>();

builder.Services.Configure<RouteOptions>(options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex"));

var API_MAX_ROWS = builder.Configuration.GetValue<int>("MaxRows");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

// District API

var districtApi = app.MapGroup("/district");

districtApi.MapGet("/", () =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return TypedResults.Ok(scope.ServiceProvider.GetService<DistrictModel>()?.All());
    });
})
.WithName("District all")
.WithDescription("Get all district")
.WithOpenApi();

districtApi.MapGet("/{dd:regex(^[0-9]{{2}}$)}", (string dd) =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return TypedResults.Ok(scope.ServiceProvider.GetService<DistrictModel>()?.GetDistrict(dd.Trim()));
    });
})
.WithName("District")
.WithDescription("Get the district with the id 'dd'")
.WithOpenApi();

// County API

var countyApi = app.MapGroup("/county");

countyApi.MapGet("/{dd:regex(^[0-9]{{2}}$)?}", (string? dd) =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return scope.ServiceProvider.GetService<CountyModel>()?.All(dd);
    });
})
.WithName("County all")
.WithDescription("Get all counties or all of district with id 'dd'")
.WithOpenApi();

countyApi.MapGet("/{dd:regex(^[0-9]{{2}}$)}/{cc:regex(^[0-9]{{2}}$)}", (string dd, string cc) =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return scope.ServiceProvider.GetService<CountyModel>()?.GetCounty(dd, cc);
    });
})
.WithName("County")
.WithDescription("Get the county with fo the id  district 'dd' and county 'cc'")
.WithOpenApi();


// Postal Code API

var postalCodeApi = app.MapGroup("/postalcode");

postalCodeApi.Map("/{pc4:regex(^[0-9]{{4}}$)}/{pc3:regex(^[0-9]{{3}}$)}", (string pc4, string pc3) =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return scope.ServiceProvider.GetService<PostalCodeModel>()?.GetPostalCode(pc4, pc3);
    });
})
.WithName("Postal code")
.WithDescription("Get information of the postal code with pc4 and pc3")
.WithOpenApi();

postalCodeApi.Map("/search/{pc4:regex(^[0-9]{{1,4}}|%$)}/{pc3:regex(^[0-9]{{1,3}}|%$)?}/{limit:int?}/{offset:int?}", (string pc4, string? pc3, int? limit, int? offset) =>
{
    return Task.Run(() =>
    {
        var scope = app.Services.CreateScope();
        return scope.ServiceProvider.GetService<PostalCodeModel>()?.All(
            pc4,
            string.IsNullOrEmpty(pc3?.Trim()) ? null : pc3.Trim(),
            (limit == null || limit <= 0 || limit > API_MAX_ROWS) ? API_MAX_ROWS : (int)limit,
            (offset == null || offset <= 0) ? 0 : (int)offset
        );
    });
})
.WithName("Postal code search")
.WithDescription("Get the postal code that start with pc4 and optional starts with pc3, can use wildcard '%'")
.WithOpenApi();

// Address API

var addressApi = app.MapGroup("/address");

addressApi.Map("/{type:regex(^(contains|start)$)}/{street}/{pc4:regex(^[0-9]{{2}}|%$)?}/{limit?}/{offset?}", (string type, string street, string? pc4, int? limit, int? offset) =>
{
    return Task.Run(() =>
    {
        using var scope = app.Services.CreateScope();
        return scope.ServiceProvider.GetService<AddressModel>()?.Search(
                Enum.Parse<AddressSearchType>(type.ToLower()),
                street.Trim(),
                (limit == null || limit <= 0 || limit > API_MAX_ROWS) ? API_MAX_ROWS : (int)limit,
                (offset == null || offset <= 0) ? 0 : (int)offset,
                pc4?.Trim()
        );
    });
})
.WithName("Address")
.WithDescription("Get the address that street strats or contains and optional belongs to poctal code with pc4, can use wildcard '%s'")
.WithOpenApi();


app.Run();

public partial class Program { }
