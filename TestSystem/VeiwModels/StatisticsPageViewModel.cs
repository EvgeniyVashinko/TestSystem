using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystem.Models;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class StatisticsPageViewModel : BindableBase
    {
        private readonly TestState _testState;
        private readonly PageNavigationService _navigationService;

        public string Title { get; set; }
        public int RightAnswersNum { get; set; }
        public int WrongAnswersNum { get; set; }
        public int Result { get; set; }
        public ObservableCollection<Question> Questions { get; set; }


        public StatisticsPageViewModel(TestState testState, PageNavigationService navigationService)
        {
            _testState = testState;
            _navigationService = navigationService;
            Questions = new ObservableCollection<Question>();
            RightAnswersNum = _testState.RightAnswers.Count;
            WrongAnswersNum = _testState.WrongAnswers.Count;
            Result = _testState.TestResult;
        }

        public ICommand ShowRightQuestions => new DelegateCommand(() =>
        {
            Title = "Правильные ответы";
            Questions = _testState.RightAnswers;
        });

        public ICommand ShowWrongQuestions => new DelegateCommand(() =>
        {
            Title = "Неправильные ответы";
            Questions = _testState.WrongAnswers;
        });

        public ICommand ToStartPage => new DelegateCommand(() =>
        {
            _navigationService.ToStartPage();
        });
    }
}
