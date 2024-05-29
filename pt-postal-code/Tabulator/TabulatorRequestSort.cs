using System.Text.Json.Serialization;

namespace PChouse.PTPostalCode.Tabulator;

public class TabulatorRequestSort
{

    [JsonPropertyName("field")]
    public string Field { get; set; } = string.Empty;

    [JsonPropertyName("dir")]
    public string? Dir { get; set; }  = string.Empty;
}
