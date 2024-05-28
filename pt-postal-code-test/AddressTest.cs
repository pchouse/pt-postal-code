using Microsoft.AspNetCore.Mvc.Testing;
using PChouse.PTPostalCode.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PTPostalCodeTest;

[TestClass]
public class AddressTest
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
    public async Task TestAddressAllStart()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<AddressEntity>>(
            "/address/start/rua"
        );

        Assert.AreEqual(200, (response?.Count ?? 0));

        response!.ForEach(a => Assert.IsTrue(a.Street.StartsWith("rua", StringComparison.CurrentCultureIgnoreCase)));

        var first = response[0];
        var second = response[1];

        response = await this._httpClient.GetFromJsonAsync<List<AddressEntity>>(
            "/address/Start/rua/%/2"
        );

        Assert.AreEqual(2, (response?.Count ?? 0));

        response!.ForEach(a => Assert.IsTrue(a.Street.StartsWith("rua", StringComparison.CurrentCultureIgnoreCase)));

        Assert.AreEqual(first, response![0]);

        response = await this._httpClient.GetFromJsonAsync<List<AddressEntity>>(
            "/address/Start/rua/%/19/1"
        );

        Assert.AreEqual(19, (response?.Count ?? 0));

        response!.ForEach(a => Assert.IsTrue(a.Street.StartsWith("rua", StringComparison.CurrentCultureIgnoreCase)));

        Assert.AreEqual(second, response![0]);

        response = await this._httpClient.GetFromJsonAsync<List<AddressEntity>>(
            "/address/Start/escadinhas/1100"
        );

        response!.ForEach(a => Assert.IsTrue(
            a.Street.StartsWith("escadinhas", StringComparison.CurrentCultureIgnoreCase) && a.Pc4 == "1100"
        ));

    }

    [TestMethod]
    public async Task TestAddressContains()
    {
        var response = await this._httpClient.GetFromJsonAsync<List<AddressEntity>>(
            "/address/contains/marques"
        );

        Assert.IsTrue((response?.Count ?? 0) > 0);

        response!.ForEach(a => Assert.IsTrue(a.Street.Contains("Marquês") || a.Street.Contains("Marques")));
    }

}
