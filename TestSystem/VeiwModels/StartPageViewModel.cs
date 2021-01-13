using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystem.Services;
using TestSystem.Pages;

namespace TestSystem.VeiwModels
{
    public class StartPageViewModel : BindableBase
    {
        private readonly PageNavigationService _navigation;
        private readonly TestService _testService;

        public StartPageViewModel(PageNavigationService navigationService, TestService testService)
        {
            _navigation = navigationService;
            _testService = testService;
        }

        public ICommand OpenTestsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new TestListPage());
        });

        public ICommand LoadTestCommand => new DelegateCommand(() =>
        {
            _testService.LoadTest();
        });

        public ICommand CreateTestCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new CreateTestPage());
        });

        public ICommand ExitCommand => new DelegateCommand(() =>
        {
            System.Windows.Application.Current.Shutdown();
        });
    }
}
