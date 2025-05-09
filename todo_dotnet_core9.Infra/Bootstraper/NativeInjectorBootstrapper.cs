using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_dotnet_core9.Applications.Interfaces;
using todo_dotnet_core9.Applications.Services;
using todo_dotnet_core9.Domain.Repositories;
using todo_dotnet_core9.Infra.Repositories;

namespace todo_dotnet_core9.Infra.Bootstraper
{
    public class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITaskAppService, TaskAppService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
