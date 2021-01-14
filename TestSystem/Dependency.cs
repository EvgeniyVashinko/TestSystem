using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.VeiwModels;
using TestSystem.Services;
using LiteDB;

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
            services.AddTransient<TestListPageViewModel>();
            services.AddTransient<CreateTestPageViewModel>();
            services.AddTransient<TestPageViewModel>();
            services.AddTransient<TestDialogWindowViewModel>();
            services.AddTransient<StatisticsPageViewModel>();

            services.AddSingleton<PageNavigationService>();
            services.AddSingleton<DialogService>();
            services.AddSingleton<CustomFileService>();
            services.AddSingleton<TestService>();
            services.AddSingleton(new LiteDatabase(@"MyData.db"));
            services.AddTransient<Repository>();
            services.AddSingleton<TestState>();

            _provider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
