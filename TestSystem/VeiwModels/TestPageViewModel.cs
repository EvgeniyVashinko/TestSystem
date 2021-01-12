using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;

namespace TestSystem.VeiwModels
{
    public class TestPageViewModel : BindableBase
    {
        private readonly TestListPageViewModel _testListPageViewModel;
        public Test Test { get; set; }

        public TestPageViewModel(TestListPageViewModel testListPageViewModel)
        {
            _testListPageViewModel = testListPageViewModel;
            Test = _testListPageViewModel.SelectedTest;
        }
    }
}
