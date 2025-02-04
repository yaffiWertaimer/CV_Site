using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class Extention
    {
        public static void AddGitHubIntegration(this IServiceCollection services ,Action<GitHubIntegrationOptions> configurationOptions)
        {
            services.Configure(configurationOptions);
            services.AddScoped<IGitHubService,GitHubService>();
        }
    }
}
