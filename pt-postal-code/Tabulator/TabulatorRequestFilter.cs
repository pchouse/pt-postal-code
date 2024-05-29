using System.Text.Json.Serialization;

namespace PChouse.PTPostalCode.Tabulator;

public class TabulatorRequestFilter
{
    [JsonPropertyName("field")]
    public string Field { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}
