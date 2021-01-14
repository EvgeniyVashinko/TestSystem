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
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class TestPageViewModel : BindableBase
    {
        private readonly TestState _testState;

        public Test Test { get; set; }
        public int QuestionNumber => CurrentQuestionNumber + 1;
        public Question CurrentQuestion { get; set; }
        public int CurrentQuestionNumber { get; set; } 
        public Answer CorrectAnswer { get; set; }
        public SolidColorBrush BorderColor { get; set; }
        public bool IsEnabled { get; set; }

        public TestPageViewModel(TestState testState)
        {
            _testState = testState;
            Test = _testState.CurrentTest;
            CurrentQuestionNumber = 0;
            CurrentQuestion = CopyQuestion(Test.Questions[CurrentQuestionNumber]);
            BorderColor = new SolidColorBrush(Colors.Black);
            IsEnabled = true;
        }

        public ICommand CheckAnswerCommand => new DelegateCommand(() =>
        {
            Check();
        });

        public ICommand NextQuestionCommand => new DelegateCommand(() =>
        {
            
            IsEnabled = true;
            BorderColor = new SolidColorBrush(Colors.Black);
            CorrectAnswer = null;
            CurrentQuestionNumber++;
            CurrentQuestion = CopyQuestion(Test.Questions[CurrentQuestionNumber]);
        }, () => CurrentQuestionNumber < Test.QuestionCount - 1);

        public ICommand ShowResultsCommand => new DelegateCommand(() =>
        {
            
        }, () => CurrentQuestionNumber >= Test.QuestionCount - 1 && !IsEnabled);

        public ICommand LeaveTestCommand => new DelegateCommand(() =>
        {
            
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

        private void Check()
        {
            if (CorrectAnswer != null)
            {
                return;
            }

            IsEnabled = false;

            var answers = CurrentQuestion.Answers;

            Question question;

            for (int i = 0; i < CurrentQuestion.Answers.Count; i++)
            {
                question = Test.Questions[CurrentQuestionNumber];

                if (question.Answers[i].IsTrue)
                {
                    CorrectAnswer = question.Answers[i];
                }

                if (answers[i].IsTrue != question.Answers[i].IsTrue)
                {
                    BorderColor = new SolidColorBrush(Colors.Red);
                    _testState.WrongAnswers.Add(question);
                    break;
                }
                else
                {
                    BorderColor = new SolidColorBrush(Colors.Green);
                    _testState.RightAnswers.Add(question);
                }
            }
        }
    }
}
