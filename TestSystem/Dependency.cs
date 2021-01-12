using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.VeiwModels;
using TestSystem.Services;

namespace TestSystem
{
    public static class Dependency
    {
        private static readonly ServiceProvider _provider;

        static Dependency()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();

            services.AddSingleton<StartPageViewModel>();

            services.AddSingleton<TestListPageViewModel>();

            services.AddTransient<CreateTestPageViewModel>();

            services.AddTransient<TestPageViewModel>();


            services.AddSingleton<PageNavigationService>();

            _provider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
