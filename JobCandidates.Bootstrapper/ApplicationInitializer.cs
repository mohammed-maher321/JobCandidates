using JobCandidates.Application.Common;
using JobCandidates.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Bootstrapper
{
    public class ApplicationInitializer : IApplicationInitializer
    {
        
        public void ConfigureMediatR(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public void ConfigurePersistence(IServiceCollection services, IConfiguration configuration, string connectionStringName)
        {
            services.AddDbContext<JobCandidatesContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(connectionStringName)));

            services.AddScoped<IJobCandidatesContext>(provider => provider.GetService<JobCandidatesContext>());

        }
        
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<IAttachmentService, AttachmentService>();
        }

       
    }

}
