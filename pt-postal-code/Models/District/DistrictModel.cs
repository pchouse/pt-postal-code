namespace PChouse.PTPostalCode.Models.District;

public class DistrictModel
{
    private NHibernate.IStatelessSession _session;

    public DistrictModel(NHibernate.IStatelessSession session) => _session = session;

    public List<DistrictEntity> All() => _session.Query<DistrictEntity>().ToList();

    public DistrictEntity? GetDistrict(string dd) => _session.Query<DistrictEntity>()
                                .Where(d => d.Dd == dd)
                                .FirstOrDefault();

}
