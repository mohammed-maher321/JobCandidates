using JobCandidates.Bootstrapper;
using System.Reflection;
using System.Runtime.Loader;

namespace JobCandidates.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(configuration.GetValue<string>("AllowOrigins"))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true));
            });
        }

        public static void InitializeApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var initializerType = configuration.GetValue<string>("InitializerType");
            var rootDirectory = GetAssemblyFolder();



            // Load Bootstapper Assembly
            string[] bootstrapperAssembly = initializerType.Split(',');
            var initializerAssembly = bootstrapperAssembly[0];
            var initializerName = bootstrapperAssembly[1];
            var assemblyBootstapper = LoadAssembly($@"{rootDirectory}\{initializerAssembly}");

            var type = assemblyBootstapper.GetType(initializerName);

            if (!(Activator.CreateInstance(type) is IApplicationInitializer initializer))
            {
                throw new InvalidOperationException("Could not load/create initializer");
            }

            // Start initialization
            initializer.ConfigurePersistence(services, configuration, "JobCandidatesConnection");
            initializer.ConfigureMediatR(services, configuration);
            initializer.ConfigureServices(services, configuration);
        }

        private static string GetAssemblyFolder()
        {
            var currentAssemblyPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            var splitterIndex = currentAssemblyPath.LastIndexOf('\\');
            return currentAssemblyPath.Substring(0, splitterIndex);
        }

        private static Assembly LoadAssembly(string assemblyPath) => AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
     


    }
}
