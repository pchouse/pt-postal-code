namespace PChouse.PTPostalCode.Models.PostalCode;

public class PostalCodeModel
{
    private NHibernate.IStatelessSession _session;

    public PostalCodeModel(NHibernate.IStatelessSession session) => _session = session;

    public PostalCodeEntity? GetPostalCode(string pc4, string pc3) => _session.Query<PostalCodeEntity>()
                                        .Where(p => p.Pc4 == pc4 && p.Pc3 == pc3)
                                        .FirstOrDefault();
    
    public List<PostalCodeEntity> All(string pc4, string? pc3, int limit, int offset)
    {
        var query = _session.Query<PostalCodeEntity>();
        query = query.Where(p => p.Pc4.StartsWith(pc4));
        if(pc3 != null)
        {
            query = query.Where(p => p.Pc3.StartsWith(pc3));
        }

        return query.Take(limit).Skip(offset).ToList();
    }
    
}
