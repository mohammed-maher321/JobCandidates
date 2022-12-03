using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Bootstrapper
{
    public interface IApplicationInitializer
    {
        void ConfigurePersistence(IServiceCollection services, IConfiguration configuration, string connectionStringName);
        void ConfigureMediatR(IServiceCollection services, IConfiguration configuration);
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
