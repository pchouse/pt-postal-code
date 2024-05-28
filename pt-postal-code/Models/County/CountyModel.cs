
namespace PChouse.PTPostalCode.Models.County;

public class CountyModel(NHibernate.IStatelessSession session)
{
    private readonly NHibernate.IStatelessSession _session = session;

    public List<CountyEntity> All(string? dd)
    {
        var query = _session.Query<CountyEntity>();
        if(dd != null)
        {
            query = query.Where(c => c.Dd == dd);
        }
        return [.. query];
    }

    public CountyEntity? GetCounty(string dd, string cc) => _session.Query<CountyEntity>()
                            .Where(c => c.Dd == dd && c.Cc == cc)
                            .FirstOrDefault();

}
