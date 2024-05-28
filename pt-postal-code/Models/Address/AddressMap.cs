using NHibernate;

namespace PChouse.PTPostalCode.Models.Address;

public class AddressMap : NHibernate.Mapping.ByCode.Conformist.ClassMapping<AddressEntity>
{

    public AddressMap()
    {
        Table("address");

        ComposedId(map =>
        {
            map.Property(p => p.Pc4, m =>
            {
                m.Column("cp4");
                m.Type(NHibernateUtil.String);
            });
            map.Property(p => p.Pc3, m => {
                m.Column("cp3");
                m.Type(NHibernateUtil.String);
            });
        });

        Property(p => p.Dd, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("dd");
        });

        Property(p => p.Cc, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("cc");
        });

        Property(p => p.Llll, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("llll");
        });

        Property(p => p.City, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("localidade");
        });

        Property(p => p.ArtCod, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("art_cod");
        });

        Property(p => p.Street, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("street");
        });

        Property(p => p.ArtLocal, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("art_local");
        });

        Property(p => p.Chunk, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("troco");
        });

        Property(p => p.Dor, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("porta");
        });

        Property(p => p.Client, p =>
        {
            p.Type(NHibernateUtil.String);
            p.Column("cliente");
        });

        Property(p => p.Cpalf, p =>
        {
            p.Type(NHibernateUtil.Int32);
            p.Column("cpalf");
        });

    }

}
