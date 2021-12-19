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
        private readonly Repository _repository;
        private readonly TestState _testState;

        public ObservableCollection<Test> Tests { get; set; }
        private Test selectedTest;
        public Test SelectedTest
        {
            get => selectedTest;
            set
            {
                selectedTest = value;
                _testState.CurrentTest = SelectedTest;
                ShowTestDialog();
            }
        }
        public TestListPageViewModel(PageNavigationService navigationService, Repository repository, TestState testState)
        {
            _navigation = navigationService;
            _repository = repository;
            _testState = testState;

            Tests = new ObservableCollection<Test>(_repository.FindAll<Test>().OrderByDescending(x => x.CreationDate));
            //Tests = new ObservableCollection<Test>()
            //{
            //    new Test
            //    {
            //        Name = "Test1",
            //        Questions = new ObservableCollection<Question>()
            //        {
            //            new Question()
            //            {
            //                Name = "Q1",
            //                Answers = new ObservableCollection<Answer>()
            //                {
            //                    new Answer()
            //                    {
            //                        Name = "A1",
            //                        IsTrue = true,
            //                    },
            //                    new Answer()
            //                    {
            //                        Name = "A2",
            //                        IsTrue = false,
            //                    },
            //                    new Answer()
            //                    {
            //                        Name = "A2",
            //                        IsTrue = false,
            //                    },
            //                }
            //            }
            //        }
            //    },
            //    new Test
            //    {
            //        Name = "Test2",
            //        Questions = new ObservableCollection<Question>()
            //        {
            //            new Question()
            //            {
            //                Name = "Q1",
            //                Answers = new ObservableCollection<Answer>()
            //                {
            //                    new Answer()
            //                    {
            //                        Name = "A1",
            //                        IsTrue = true,
            //                    },
            //                    new Answer()
            //                    {
            //                        Name = "A2",
            //                        IsTrue = false,
            //                    },
            //                }
            //            }
            //        }
            //    }
            //};
        }

        public ICommand OpenTestCommand => new DelegateCommand<Guid>((id) =>
        {
            _testState.CurrentTest = Tests.FirstOrDefault(x => x.Id == id);
            ShowTestDialog();      
        });

        public ICommand EditTestCommand => new DelegateCommand<Guid>((id) =>
        {
            _testState.CurrentTest = Tests.FirstOrDefault(x => x.Id == id);
            _navigation.Navigate(new CreateTestPage());
        });

        public ICommand DeleteTestCommand => new DelegateCommand<Guid>((id) =>
        {
            _repository.Delete<Test>(id);
            Tests.Remove(Tests.FirstOrDefault(x => x.Id == id));
        });

        private void ShowTestDialog()
        {
            Views.TestDialogWindow dialogWindow = new Views.TestDialogWindow();
            if (dialogWindow.ShowDialog() == true)
            {
                _navigation.Navigate(new TestPage());
            }
        }
    }
}
