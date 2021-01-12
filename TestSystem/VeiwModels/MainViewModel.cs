using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TestSystem.Pages;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class MainViewModel : BindableBase
    {
        private readonly PageNavigationService _navigation;

        public Page CurrentPage { get; set; }

        public MainViewModel(PageNavigationService navigationService)
        {
            navigationService.OnPageChanged += page => CurrentPage = page;
            navigationService.Navigate(new StartPage());
            _navigation = navigationService;
        }

        public ICommand GoBack => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        }, () => _navigation.CanGoBack);
    }
}
