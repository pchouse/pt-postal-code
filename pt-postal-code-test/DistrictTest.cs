using Microsoft.AspNetCore.Mvc.Testing;
using PChouse.PTPostalCode.Models.District;
using System.Net.Http.Json;

namespace PTPostalCodeTest;

[TestClass]
public class DistrictTest
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
    public async Task TestDistrictAll()
    {
       
        var response = await this._httpClient.GetFromJsonAsync<List<DistrictEntity>>("/district");

        Assert.IsTrue((response?.Count ?? 0) > 0);

    }

    [TestMethod]
    public async Task TestDistrict()
    {
        var response = await this._httpClient.GetFromJsonAsync<DistrictEntity>("/district/09");

        Assert.AreEqual("Guarda", response?.DistrictName);

    }


}