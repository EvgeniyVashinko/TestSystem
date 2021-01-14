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
        private readonly TestState _testState;

        public StartPageViewModel(PageNavigationService navigationService, TestService testService, TestState testState)
        {
            _navigation = navigationService;
            _testService = testService;
            _testState = testState;
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
            _testState.CurrentTest = null;
            _navigation.Navigate(new CreateTestPage());
        });

        public ICommand ExitCommand => new DelegateCommand(() =>
        {
            System.Windows.Application.Current.Shutdown();
        });
    }
}
