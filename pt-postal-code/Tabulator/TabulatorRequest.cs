namespace PChouse.PTPostalCode.Tabulator;

public class TabulatorRequest
{

    public int Page { get; set; }

    public int Size { get; set; }

    public List<TabulatorRequestSort> Sort { get; set; } = [];

    public List<TabulatorRequestFilter> Filter { get; set; } = [];

}
