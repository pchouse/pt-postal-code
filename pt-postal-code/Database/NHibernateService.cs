using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using PChouse.PTPostalCode.Models.District;
using PChouse.PTPostalCode.Models.Address;
using PChouse.PTPostalCode.Models.PostalCode;
using PChouse.PTPostalCode.Models.County;

namespace PChouse.PTPostalCode.database;

public static class NHibernateService
{
    public static IServiceCollection AddNHibernate(
        this IServiceCollection services,
        ConfigurationManager configurationManager
    )
    {
        try
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = configurationManager.GetConnectionString("zip-code-pt") ?? "";
                db.Dialect<NHibernate.Dialect.SQLiteDialect>();
            });

            configuration.SetProperty(
                "show_sql", configurationManager.GetValue<bool>("ShowSql") ? "true" : "false"
            );
            
            configuration.SetProperty(
                "format_sql", configurationManager.GetValue<bool>("FormatSql") ? "true" : "false"
             );

            configuration.SetProperty(
                "generate_statistics", configurationManager.GetValue<bool>("GenerateStatistics") ? "true" : "false"
            );

            var mapper = new ModelMapper();
            mapper.AddMapping<DistrictMap>();
            mapper.AddMapping<CountyMap>();
            mapper.AddMapping<AddressMap>();
            mapper.AddMapping<PostalCodeMap>();
            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            
            var sessionFactory = configuration.BuildSessionFactory();
            
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenStatelessSession());
        }
        catch (Exception ex)
        {
            // Log the exception (you can use any logging framework or service)
            Console.WriteLine($"NHibernate configuration failed: {ex.Message}");
            throw;
        }

        return services;
    }
}
