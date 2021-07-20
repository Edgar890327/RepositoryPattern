using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.MongoRepository;
using Repository.SqlRepository;

namespace Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string MONGO_SETTINGS_NAME = "MongoDbSettings";
        public static void ConfigureSqlRepository<TContext>(this IServiceCollection services, TContext context) where TContext : DbContext
        {
            services.AddScoped<ISqlRepository<TContext>>(implementation=> new SqlRepository<TContext>(context));
        }

        public static void ConfigureMongoRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = new MongoDbSettings();
            configuration.GetSection(MONGO_SETTINGS_NAME).Bind(mongoSettings);
            services.Configure<MongoDbSettings>(settings => configuration.GetSection(MONGO_SETTINGS_NAME));

            services.AddSingleton<IMongoDbSettings>(mongoSettings);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        }
    }
}
