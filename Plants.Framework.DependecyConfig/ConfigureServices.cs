using Microsoft.Extensions.DependencyInjection;
using Plants.Domain.IRepository;
using Plants.Domain.IService;
using Plants.Domain.Service;
using Plants.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Framework.DependecyConfig
{
   public  class ConfigureServices
    {
        public ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPlantsService, PlantsService>();
            services.AddTransient<IPlantsRepository, PlantsRepository>();
            
        }
    }
}
