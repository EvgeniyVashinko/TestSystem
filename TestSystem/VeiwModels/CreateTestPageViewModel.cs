using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using TestSystem.Models;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class CreateTestPageViewModel : BindableBase
    {
        private readonly TestService _testService;

        public Test Test { get; set; }
        public CreateTestPageViewModel(TestService testService)
        {
            Test = new Test();
            Test.Name = "Новый тест";
            Test.Questions = new ObservableCollection<Question>();
            _testService = testService;
        }

        public ICommand SaveTestCommand => new DelegateCommand(() =>
        {
            _testService.SaveTest(Test);
        });
        public ICommand DeleteTestCommand => new DelegateCommand(() =>
        {
            _testService.DeleteTest(Test.Id);
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
