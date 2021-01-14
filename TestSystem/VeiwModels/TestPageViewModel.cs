using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestSystem.Models;
using TestSystem.Pages;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class TestPageViewModel : BindableBase
    {
        private readonly TestState _testState;
        private readonly PageNavigationService _navigationService;
        private readonly DialogService _dialogService;

        public Test Test { get; set; }
        public int QuestionNumber => CurrentQuestionNumber + 1;
        public Question CurrentQuestion { get; set; }
        public int CurrentQuestionNumber { get; set; } 
        public Answer CorrectAnswer { get; set; }
        public SolidColorBrush BorderColor { get; set; }
        public bool IsEnabled { get; set; }

        public TestPageViewModel(TestState testState, PageNavigationService navigationService, DialogService dialogService)
        {
            _testState = testState;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Test = _testState.CurrentTest;
            CurrentQuestionNumber = 0;
            CurrentQuestion = CopyQuestion(Test.Questions[CurrentQuestionNumber]);
            BorderColor = new SolidColorBrush(Colors.Black);
            IsEnabled = true;
        }

        public ICommand CheckAnswerCommand => new DelegateCommand(() =>
        {
            IsEnabled = false;

            if (Check())
            {
                BorderColor = new SolidColorBrush(Colors.Green);
            }
            else
            {
                BorderColor = new SolidColorBrush(Colors.Red);
            }

            CorrectAnswer = Test.Questions[CurrentQuestionNumber].GetRightAnswer();

        }, ()=>_testState.CanCheckAnswer && IsEnabled);

        public ICommand NextQuestionCommand => new DelegateCommand(() =>
        {
            Check();
            IsEnabled = true;
            BorderColor = new SolidColorBrush(Colors.Black);
            CorrectAnswer = null;
            CurrentQuestionNumber++;
            if (CurrentQuestionNumber < Test.QuestionCount)
            {
                CurrentQuestion = CopyQuestion(Test.Questions[CurrentQuestionNumber]);
            }
            else
            {
                IsEnabled = false;
                _dialogService.ShowMessage("Тест завершен, посмотрите результаты!");
            }

        }, () => CurrentQuestionNumber < Test.QuestionCount);

        public ICommand ShowResultsCommand => new DelegateCommand(() =>
        {
            _navigationService.Navigate(new StatisticsPage());
        }, () => CurrentQuestionNumber >= Test.QuestionCount - 1 && !IsEnabled);

        public ICommand LeaveTestCommand => new DelegateCommand(() =>
        {
            _navigationService.GoBack();
        }, () => CurrentQuestionNumber >= Test.QuestionCount - 1 && !IsEnabled);

        private Question CopyQuestion(Question q)
        {
            var res = new Question
            {
                Id = q.Id,
                Name = q.Name,
                Answers = new ObservableCollection<Answer>()
            };

            foreach (var item in q.Answers)
            {
                res.Answers.Add(new Answer
                {
                    Name = item.Name,
                    IsTrue = false,
                });
            }

            return res;
        }

        private bool Check()
        {
            Question question = Test.Questions[CurrentQuestionNumber];

            var isCorrect = question.CheckAnswer(CurrentQuestion);

            if (CorrectAnswer != null)
            {
                return isCorrect;
            }

            if (isCorrect)
            {
                _testState.RightAnswers.Add(question);
            }
            else
            {
                _testState.WrongAnswers.Add(question);
            }

            return isCorrect;
        }
    }
}
