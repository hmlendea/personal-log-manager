using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NuciAPI.Middleware;
using PersonalLogManager.Configuration;

namespace PersonalLogManager
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration => configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddConfigurations(Configuration)
                .AddNuciApiScannerProtection()
                .AddCustomServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var dataStoreSettings = app.ApplicationServices.GetRequiredService<DataStoreSettings>();
            CreateStoreIfMissing(dataStoreSettings.LogStorePath);

            app.UseNuciApiRequestLogging();
            app.UseNuciApiExceptionHandling();
            app.UseNuciApiScannerProtection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static void CreateStoreIfMissing(string storePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(storePath);

            var storeDirectory = Path.GetDirectoryName(storePath);

            if (!string.IsNullOrWhiteSpace(storeDirectory) && !Directory.Exists(storeDirectory))
            {
                Directory.CreateDirectory(storeDirectory);
            }

            if (!File.Exists(storePath))
            {
                File.WriteAllText(storePath, "[]");
            }
        }
    }
}
