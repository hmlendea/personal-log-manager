using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;
using NuciLog;
using NuciLog.Configuration;
using NuciLog.Core;
using NuciText.Normalisation;
using NuciText.Obfuscation;
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

        public static IServiceCollection AddConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();
            securitySettings = new SecuritySettings();

            configuration.Bind(nameof(dataStoreSettings), dataStoreSettings);
            configuration.Bind(nameof(securitySettings), securitySettings);

            return services
                .AddSingleton(dataStoreSettings)
                .AddSingleton(securitySettings)
                .AddNuciLoggerSettings(configuration);
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<IFileRepository<PersonalLogEntity>>(x => new JsonRepository<PersonalLogEntity>(dataStoreSettings.LogStorePath))
            .AddSingleton<IPersonalLogTextBuilderFactory, PersonalLogTextBuilderFactory>()
            .AddSingleton<IPersonalLogService, PersonalLogService>()
            .AddSingleton<INuciTextNormaliser, NuciTextNormaliser>()
            .AddSingleton<INuciTextObfuscator, NuciTextObfuscator>()
            .AddScoped<ILogger, NuciLogger>();
    }
}
