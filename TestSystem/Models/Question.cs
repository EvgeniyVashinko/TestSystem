using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Models
{
    public class Question
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        public Question()
        {
            Id = Guid.NewGuid();
            Answers = new ObservableCollection<Answer>();
        }

        public bool CheckAnswer(Question question)
        {            
            for (int i = 0; i < Answers.Count; i++)
            {
                if (question.Answers[i].IsTrue != Answers[i].IsTrue)
                {
                    return false;
                }
            }

            return true;
        }

        public Answer GetRightAnswer()
        {
            Answer answer = null;

            foreach (var item in Answers)
            {
                if (item.IsTrue)
                {
                    answer = item;
                    break;
                }
            }

            return answer ?? new Answer();
        }

        public bool IsCorrectQuestion()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (Answers.Count == 0)
            {
                return false;
            }

            foreach (var item in Answers)
            {
                if (!item.IsCorrectAnswer())
                {
                    return false;
                }

                if (item.IsTrue == true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
