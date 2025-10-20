using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;

using PersonalLogManager.Configuration;
using PersonalLogManager.DataAccess;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service;

namespace PersonalLogManager
{
    public static class ServiceCollectionExtensions
    {
        static DataStoreSettings dataStoreSettings;

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();

            configuration.Bind(nameof(DataStoreSettings), dataStoreSettings);

            services.AddSingleton(dataStoreSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<IFileRepository<PersonalLogEntity>>(x => new JsonRepository<PersonalLogEntity>(dataStoreSettings.TextLogStorePath))
            .AddSingleton<IPersonalLogTextBuilder, PersonalLogTextBuilder>()
            .AddSingleton<IPersonalLogService, PersonalLogService>()
            .AddAutoMapper(typeof(DataAccessMappingProfile))
            .AddAutoMapper(typeof(ServiceMappingProfile));
    }
}
