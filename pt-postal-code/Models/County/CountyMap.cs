using NHibernate;

namespace PChouse.PTPostalCode.Models.County;

public class CountyMap : NHibernate.Mapping.ByCode.Conformist.ClassMapping<CountyEntity>
{

    public CountyMap() {
        
        Table("concelho");

        ComposedId(map =>
        {
            map.Property(p => p.Dd, m =>
            {
                m.Column("dd");
                m.Type(NHibernateUtil.String);
            });

            map.Property(p => p.Cc, m =>
            {
                m.Column("cc");
                m.Type(NHibernateUtil.String);
            });
        });

        Property(p => p.CountyName, m =>
        {
            m.Column("concelho");
            m.Type(NHibernateUtil.String);
        });

    }

}
