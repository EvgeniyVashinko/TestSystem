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
        public int CurrentQuestionNumber { get; set; }
        public Answer CorrectAnswer { get; set; }
        public SolidColorBrush BorderColor { get; set; }
        public bool IsEnabled { get; set; }
        public bool CanCheckAnswer { get; set; }
        public bool ShowPreviousButton { get; set; }
        private List<Question> _questions;
        private bool _testCompleted = false;

        public Question CurrentQuestion
        {
            get
            {
                if (CurrentQuestionNumber >= _questions.Count())
                {
                    return _questions.Last();
                }

                return _questions[CurrentQuestionNumber];
            }
        }

        public TestPageViewModel(TestState testState, PageNavigationService navigationService, DialogService dialogService)
        {
            _testState = testState;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Test = _testState.CurrentTest;
            CurrentQuestionNumber = 0;
            BorderColor = new SolidColorBrush(Colors.Black);
            IsEnabled = true;
            CanCheckAnswer = _testState.CanCheckAnswer;
            ShowPreviousButton = !_testState.CanCheckAnswer;
            _questions = new List<Question>(Test.Questions.Select(x => CopyQuestion(x)));
        }

        public ICommand CheckAnswerCommand => new DelegateCommand(() =>
        {
            IsEnabled = false;
            var question = Test.Questions.First(x => x.Id == CurrentQuestion.Id);

            if (Check())
            {
                BorderColor = new SolidColorBrush(Colors.Green);
            }
            else
            {
                BorderColor = new SolidColorBrush(Colors.Red);
            }

            CorrectAnswer = question.GetRightAnswer();

        }, () => CanCheckAnswer && IsEnabled && IsAnswerSelected());

        public ICommand NextQuestionCommand => new DelegateCommand(() =>
        {
            IsEnabled = true;
            BorderColor = new SolidColorBrush(Colors.Black);
            CorrectAnswer = null;

            if (CurrentQuestionNumber >= Test.QuestionCount - 1 && !_testCompleted)
            {
                if (CanCheckAnswer)
                {
                    IsEnabled = false;
                }

                _testCompleted = true;
                _dialogService.ShowMessage("Тест завершен, посмотрите результаты!");
            }

            if (CurrentQuestionNumber < Test.QuestionCount - 1)
            {
                CurrentQuestionNumber++;
            }

        }, () => IsAnswerSelected() && !(_testCompleted && CurrentQuestionNumber >= Test.QuestionCount - 1));

        public ICommand PreviousQuestionCommand => new DelegateCommand(() =>
        {
            IsEnabled = true;
            CorrectAnswer = null;
            CurrentQuestionNumber--;
        }, () => CurrentQuestionNumber > 0);

        public ICommand ShowResultsCommand => new DelegateCommand(() =>
        {
            CheckResult();
            _navigationService.Navigate(new StatisticsPage());
        }, () => _testCompleted );

        public ICommand LeaveTestCommand => new DelegateCommand(() =>
        {
            _navigationService.GoBack();
        }, () => _testCompleted);

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
                    Id = item.Id,
                    Name = item.Name,
                    IsTrue = false,
                });
            }

            return res;
        }

        private bool Check(Question q)
        {
            var question = Test.Questions.First(x => x.Id == q.Id);

            var isCorrect = question.CheckAnswer(q);

            return isCorrect;
        }

        private bool Check() => Check(CurrentQuestion);

        private bool IsAnswerSelected()
        {
            foreach (var item in CurrentQuestion.Answers)
            {
                if (item.IsTrue == true)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckResult()
        {
            _testState.RightAnswers.Clear();
            _testState.WrongAnswers.Clear();
            foreach (var question in _questions)
            {
                if (Check(question))
                {
                    _testState.RightAnswers.Add(question);
                }
                else
                {
                    _testState.WrongAnswers.Add(question);
                }
            }
        }
    }
}
