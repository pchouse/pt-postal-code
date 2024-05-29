using Microsoft.AspNetCore.Mvc.Testing;
using PChouse.PTPostalCode.Models.Address;
using PChouse.PTPostalCode.Tabulator;
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

    [TestMethod]
    public async Task TestTablulatorData()
    {
        var response = await this._httpClient.PostAsJsonAsync<TabulatorRequest>(
            "/address/tabulator/scroll", new TabulatorRequest { Page  = 1, Size = 99}
        );

        Assert.IsTrue(response?.IsSuccessStatusCode);

        var tabulatorResponse = await response!.Content.ReadFromJsonAsync<TabulatorResponse<AddressEntity>>();

        Assert.IsTrue(tabulatorResponse?.Data.Count == 99);
    }

    [TestMethod]
    public async Task TestTablulatorFilterEqual()
    {
        var response = await this._httpClient.PostAsJsonAsync<TabulatorRequest>(
            "/address/tabulator/scroll", new TabulatorRequest { 
                Page = 1, 
                Size = 99,
                Filter = { 
                    new() { Field = "Pc4", Type = "=", Value = "1100"},
                    new() { Field = "Pc3", Type = "=", Value = "003"}
                }
            }
        );

        Assert.IsTrue(response?.IsSuccessStatusCode);

        var tabulatorResponse = await response!.Content.ReadFromJsonAsync<TabulatorResponse<AddressEntity>>();

        Assert.IsTrue(tabulatorResponse?.Data.Count == 1);

        Assert.IsTrue(tabulatorResponse?.Data[0].Pc4 == "1100");
        Assert.IsTrue(tabulatorResponse?.Data[0].Pc3 == "003");
    }

    [TestMethod]
    public async Task TestTablulatorFilterSortAsc()
    {
        var response = await this._httpClient.PostAsJsonAsync<TabulatorRequest>(
            "/address/tabulator/scroll", new TabulatorRequest
            {
                Page = 1,
                Size = 99,
                Sort = {
                    new() { Field = "Pc4", Dir = "asc"}
                }
            }
        );

        Assert.IsTrue(response?.IsSuccessStatusCode);

        var tabulatorResponse = await response!.Content.ReadFromJsonAsync<TabulatorResponse<AddressEntity>>();

        Assert.IsTrue(tabulatorResponse?.Data.Count == 99);

        Assert.IsTrue(tabulatorResponse?.Data[0].Pc4.StartsWith('1'));
    }

    [TestMethod]
    public async Task TestTablulatorFilterSortDesc()
    {
        var response = await this._httpClient.PostAsJsonAsync<TabulatorRequest>(
            "/address/tabulator/scroll", new TabulatorRequest
            {
                Page = 1,
                Size = 99,
                Sort = {
                    new() { Field = "Pc4", Dir = "desc"}
                }
            }
        );

        Assert.IsTrue(response?.IsSuccessStatusCode);

        var tabulatorResponse = await response!.Content.ReadFromJsonAsync<TabulatorResponse<AddressEntity>>();

        Assert.IsTrue(tabulatorResponse?.Data.Count == 99);

        Assert.IsTrue(tabulatorResponse?.Data[0].Pc4.StartsWith('9'));
    }

}
