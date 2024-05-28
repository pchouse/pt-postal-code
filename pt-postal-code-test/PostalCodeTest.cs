using Microsoft.AspNetCore.Mvc.Testing;
using PChouse.PTPostalCode.Models.PostalCode;
using System.Net.Http.Json;

namespace PTPostalCodeTest;

[TestClass]
public class PostalCodeTest
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
    public async Task TestPostalCode()
    {
        var response = await this._httpClient.GetFromJsonAsync<PostalCodeEntity>("/postalcode/1100/003");
        Assert.IsNotNull(response);
        Assert.AreEqual("1100", response.Pc4);
        Assert.AreEqual("003", response.Pc3);
    }

    [TestMethod]
    public async Task TestSearchLimitAndOffset()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<PostalCodeEntity>>(
            "/postalcode/search/2"
        );

        Assert.IsTrue((response?.Count ?? 0) == 200);

        response?.ForEach(p => Assert.IsTrue(p.Pc4.StartsWith('2')));


        var second = response![1];

        response = await this._httpClient.GetFromJsonAsync<List<PostalCodeEntity>>(
            "/postalcode/search/2/%/99/1"
        );

        Assert.IsTrue((response?.Count ?? 0) == 99);

        response?.ForEach(p => Assert.IsTrue(p.Pc4.StartsWith('2')));

        Assert.AreEqual(second.Pc4, response![0].Pc4);
        Assert.AreEqual(second.Pc3, response![0].Pc3);

    }

    [TestMethod]
    public async Task TestSearchLimitAndOffWithWildCard()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<PostalCodeEntity>>(
            "/postalcode/search/%/30"
        );

        Assert.IsTrue((response?.Count ?? 0) > 0);

        response?.ForEach(p => Assert.IsTrue(p.Pc3.StartsWith("30")));

        response = await this._httpClient.GetFromJsonAsync<List<PostalCodeEntity>>(
            "/postalcode/search/30/%/5"
        );

        Assert.IsTrue((response?.Count ?? 0) > 0 && (response?.Count ?? 0) <= 5);

        response?.ForEach(p => Assert.IsTrue(p.Pc4.StartsWith("30")));

    }

}
