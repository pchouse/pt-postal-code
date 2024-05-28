using NHibernate;

namespace PChouse.PTPostalCode.Models.PostalCode;

public class PostalCodeMap : NHibernate.Mapping.ByCode.Conformist.ClassMapping<PostalCodeEntity>
{

    public PostalCodeMap() {

        Table("cpostal");

        ComposedId(map =>
        {
            map.Property(p => p.Pc4, p =>
            {
                p.Column("cp4");
                p.Type(NHibernateUtil.String);
            });

            map.Property(p => p.Pc3, p =>
            {
                p.Column("cp3");
                p.Type(NHibernateUtil.String);
            });

        });

        Property(p => p.Dd, p =>
        {
            p.Column("dd");
            p.Type(NHibernateUtil.String);
        });
        
        Property(p => p.Cc, p =>
        {
            p.Column("cc");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.Llll, p =>
        {
            p.Column("llll");
            p.Type(NHibernateUtil.String);
        });
        
        Property(p => p.City, p =>
        {
            p.Column("localidade");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.ArtCod, p =>
        {
            p.Column("art_cod");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.ArtType, p =>
        {
            p.Column("art_tipo");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.FirstPrep, p =>
        {
            p.Column("pri_prep");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.ArtTitle, p =>
        {
            p.Column("art_titulo");
            p.Type(NHibernateUtil.String);
        }); 
        
        Property(p => p.SecondPrep, p =>
        {
            p.Column("seg_prep");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.ArtDesig, p =>
        {
            p.Column("art_desig");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.ArtLocal, p =>
        {
            p.Column("art_local");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.Chunk, p =>
        {
            p.Column("troco");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.Dor, p =>
        {
            p.Column("porta");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.Client, p =>
        {
            p.Column("cliente");
            p.Type(NHibernateUtil.String);
        });

        Property(p => p.Cpalf, p =>
        {
            p.Column("cpalf");
            p.Type(NHibernateUtil.Int32);
        });

    }



}
