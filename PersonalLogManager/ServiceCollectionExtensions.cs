using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;
using NuciLog;
using NuciLog.Core;
using NuciText.Normalisation;
using NuciText.Obfuscation;
using PersonalLogManager.Configuration;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            DataStoreSettings dataStoreSettings = new();
            SecuritySettings securitySettings = new();

            configuration.Bind(nameof(dataStoreSettings), dataStoreSettings);
            configuration.Bind(nameof(securitySettings), securitySettings);

            return services
                .AddSingleton(dataStoreSettings)
                .AddSingleton(securitySettings)
                .AddNuciLoggerSettings(configuration);
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<IFileRepository<PersonalLogEntity>>(sp =>
                new JsonRepository<PersonalLogEntity>(
                    sp.GetRequiredService<DataStoreSettings>().LogStorePath))
            .AddSingleton<IPersonalLogTextBuilderFactory, PersonalLogTextBuilderFactory>()
            .AddSingleton<IPersonalLogService, PersonalLogService>()
            .AddSingleton<INuciTextNormaliser, NuciTextNormaliser>()
            .AddSingleton<INuciTextObfuscator, NuciTextObfuscator>()
            .AddScoped<ILogger, NuciLogger>();
    }
}
