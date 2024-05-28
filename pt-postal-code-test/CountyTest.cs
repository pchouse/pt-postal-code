using Microsoft.AspNetCore.Mvc.Testing;
using PChouse.PTPostalCode.Models.County;
using System.Net.Http.Json;

namespace PTPostalCodeTest;

[TestClass]
public class CountyTest
{
    #pragma warning disable CS8618
    private WebApplicationFactory<Program> _factory;
    private HttpClient _httpClient;
    #pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
        this._factory = new WebApplicationFactory<Program>();
        this._httpClient = this._factory.CreateClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
        this._httpClient.Dispose();
        this._factory.Dispose();
    }

    [TestMethod]
    public async Task TestCountyAll()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<CountyEntity>> ("/county");

        Assert.IsTrue((response?.Count ?? 0) > 0);
    }

    [TestMethod]
    public async Task TestCountyOfDistrict()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<CountyEntity>>("/county/09");

        Assert.IsNotNull(response);

        response?.ForEach(c => Assert.AreEqual("09", c.Dd));
    }
    
    [TestMethod]
    public async Task TestCounty()
    {
        var response = await this._httpClient.GetFromJsonAsync<CountyEntity>("/county/09/01");

        Assert.IsNotNull(response);

        Assert.AreEqual("09", response.Dd);
        Assert.AreEqual("01", response.Cc);
    }

}
