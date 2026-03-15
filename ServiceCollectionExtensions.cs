using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;
using NuciLog;
using NuciLog.Configuration;
using NuciLog.Core;
using PersonalLogManager.Configuration;
using PersonalLogManager.DataAccess;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager
{
    public static class ServiceCollectionExtensions
    {
        static DataStoreSettings dataStoreSettings;
        static SecuritySettings securitySettings;
        static NuciLoggerSettings logSettings;

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();
            securitySettings = new SecuritySettings();
            logSettings = new NuciLoggerSettings();

            configuration.Bind(nameof(DataStoreSettings), dataStoreSettings);
            configuration.Bind(nameof(securitySettings), securitySettings);
            configuration.Bind(nameof(NuciLoggerSettings), logSettings);

            services.AddSingleton(dataStoreSettings);
            services.AddSingleton(securitySettings);
            services.AddSingleton(logSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<IFileRepository<PersonalLogEntity>>(x => new JsonRepository<PersonalLogEntity>(dataStoreSettings.LogStorePath))
            .AddSingleton<IPersonalLogTextBuilderFactory, PersonalLogTextBuilderFactory>()
            .AddSingleton<IPersonalLogService, PersonalLogService>()
            .AddScoped<ILogger, NuciLogger>();
    }
}
