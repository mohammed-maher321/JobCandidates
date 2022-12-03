using JobCandidates.Application.Common;
using JobCandidates.Application.UserProfileUseCases.Commands;
using JobCandidates.Application.UserProfileUseCases.Queries;
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
            services.AddMediatR(typeof(AddUserProfileModel).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserProfileModel).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllUserProfilesModel).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserProfileModel).GetTypeInfo().Assembly);
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
