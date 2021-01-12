using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystem.Models;
using TestSystem.Pages;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class TestListPageViewModel : BindableBase
    {
        private readonly PageNavigationService _navigation;
        
        public ObservableCollection<Test> Tests { get; set; }
        private Test selectedTest;
        public Test SelectedTest
        {
            get => selectedTest;
            set
            {
                selectedTest = value;
                _navigation.Navigate(new TestPage());
                selectedTest = null;
            }
        }
        public TestListPageViewModel(PageNavigationService navigationService)
        {
            _navigation = navigationService;

            Tests = new ObservableCollection<Test>() 
            {
                new Test
                {
                    Name = "Test1",
                    Questions = new ObservableCollection<Question>()
                    {
                        new Question()
                        {
                            Name = "Q1",
                            Answers = new ObservableCollection<Answer>()
                            {
                                new Answer()
                                {
                                    Name = "A1",
                                    IsTrue = true,
                                },
                                new Answer()
                                {
                                    Name = "A2",
                                    IsTrue = false,
                                },
                            }
                        }
                    }
                },
                new Test
                {
                    Name = "Test2",
                    Questions = new ObservableCollection<Question>()
                    {
                        new Question()
                        {
                            Name = "Q1",
                            Answers = new ObservableCollection<Answer>()
                            {
                                new Answer()
                                {
                                    Name = "A1",
                                    IsTrue = true,
                                },
                                new Answer()
                                {
                                    Name = "A2",
                                    IsTrue = false,
                                },
                            }
                        }
                    }
                }
            };
        }

        public ICommand OpenTestCommand => new DelegateCommand(() =>
        {
            //_navigation.Navigate(new )
        });
    }
}
