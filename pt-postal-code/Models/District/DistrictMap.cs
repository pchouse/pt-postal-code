using NHibernate;

namespace PChouse.PTPostalCode.Models.District;

public class DistrictMap
    : NHibernate.Mapping.ByCode.Conformist.ClassMapping<DistrictEntity>
{

    public DistrictMap()
    {

        Table("distrito");

        Id(i => i.Dd);

        Property(p => p.Dd, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("dd");
        });

        Property(p => p.DistrictName, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("distrito");
        });
    }

}
