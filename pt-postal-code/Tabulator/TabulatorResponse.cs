using PChouse.PTPostalCode.Models.Address;
using System.Text.Json.Serialization;

namespace PChouse.PTPostalCode.Tabulator;

public class TabulatorResponse<T>
{
    [JsonPropertyName("last_page")]
    public int LstaPage { get; set; } = 0;

    [JsonPropertyName("data")]
    public IList<T> Data { get; set; } = [];
}
