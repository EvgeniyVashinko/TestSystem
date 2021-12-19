using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;

namespace TestSystem.Services
{
    public class CustomFileService : FileService
    {
        private readonly DialogService _dialogService;

        public CustomFileService(DialogService dialogService)
        {
            _dialogService = dialogService;
        }
        //public override Test ReadTestFromFile(string filepath)
        //{
        //    Test test = new Test
        //    {
        //        Name = "Тест по АВС",
        //    };

        //    using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))
        //    {
        //        string name, answers, rightAnswer;
        //        name = answers = rightAnswer = string.Empty;
                
        //        while ((name = sr.ReadLine()) != null)
        //        {
        //            Question question = new Question { Name = name };

        //            answers = sr.ReadLine();
        //            rightAnswer = sr.ReadLine();
        //            sr.ReadLine();

        //            string[] answs = answers.Split(';', StringSplitOptions.RemoveEmptyEntries);

        //            var items = answs.Select(str => str.Trim()[3..]);

        //            foreach (var item in items)
        //            {
        //                question.Answers.Add(new Answer
        //                {
        //                    Name = item
        //                });
        //            }

        //            var rightAnswerIndex = Convert.ToInt32(rightAnswer) - 1;

        //            question.Answers[rightAnswerIndex].IsTrue = true;

        //            test.Questions.Add(question);
        //        }
        //    }

        //    return test;
        //}
    }
}
