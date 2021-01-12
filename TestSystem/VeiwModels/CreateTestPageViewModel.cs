using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using TestSystem.Models;

namespace TestSystem.VeiwModels
{
    public class CreateTestPageViewModel : BindableBase
    {
        public Test Test { get; set; }
        public CreateTestPageViewModel()
        {
            Test = new Test();
            Test.Name = "Новый тест";
            Test.Questions = new ObservableCollection<Question>()
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
                },
                new Question()
                {
                    Name = "Q2",
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
                },
                new Question()
                {
                    Name = "Q3",
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
            };
        }

        public ICommand SaveTestCommand => new DelegateCommand(() =>
        {

        });
        public ICommand DeleteTestCommand => new DelegateCommand(() =>
        {

        });
        public ICommand AddQuestionCommand => new DelegateCommand(() =>
        {
            var question = new Question
            {
                Name = "New Question"
            };
            Test.Questions.Add(question);
        });
        public ICommand DeleteQuestionCommand => new DelegateCommand<Guid>(id =>
        {
            var obj = Test.Questions.FirstOrDefault(x => x.Id == id);
            Test.Questions.Remove(obj);
        });

        public ICommand DeleteAnswerCommand => new DelegateCommand<Guid>(id =>
        {
            foreach (var question in Test.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.Id == id)
                    {
                        question.Answers.Remove(answer);
                        break;
                    }
                }
            } 
        });
        public ICommand AddAnswerCommand => new DelegateCommand<Guid>((questionId) =>
        {
            var question = Test.Questions.FirstOrDefault(x => x.Id == questionId);
            var answer = new Answer
            {
                Name = "New Answer"
            };
            question.Answers.Add(answer);
        });
    }
}
