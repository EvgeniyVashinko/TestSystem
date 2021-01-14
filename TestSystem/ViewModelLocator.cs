using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.VeiwModels;

namespace TestSystem
{
    public class ViewModelLocator
    {
        public MainViewModel mainViewModel => Dependency.Resolve<MainViewModel>();
        public StartPageViewModel startPageViewModel => Dependency.Resolve<StartPageViewModel>();
        public TestListPageViewModel testListPageViewModel => Dependency.Resolve<TestListPageViewModel>();
        public CreateTestPageViewModel createTestPageViewModel => Dependency.Resolve<CreateTestPageViewModel>();
        public TestPageViewModel testPageViewModel => Dependency.Resolve<TestPageViewModel>();
        public TestDialogWindowViewModel testDialogWindowViewModel => Dependency.Resolve<TestDialogWindowViewModel>();
        public StatisticsPageViewModel statisticsPageViewModel => Dependency.Resolve<StatisticsPageViewModel>();
    }
}
