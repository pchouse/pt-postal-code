using System.Data.Entity;

namespace PChouse.PTPostalCode.Models.Address;

public class AddressModel
{
    private NHibernate.IStatelessSession _session;

    public AddressModel(NHibernate.IStatelessSession session) => _session = session;

    public List<AddressEntity> Search(AddressSearchType searchType, string street, int limit, int offset, string? pc4)
    {
        var query = _session.Query<AddressEntity>();

        query = searchType == AddressSearchType.contains ?
                query.Where(a => a.Street.Contains(street)) :
                query.Where(a => a.Street.StartsWith(street));

        if (!string.IsNullOrEmpty(pc4))
        {
            query = query.Where(a => a.Pc4.StartsWith(pc4));
        }

        return query.Take(limit).Skip(offset).ToList();
    }
}
