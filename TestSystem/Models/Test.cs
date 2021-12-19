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
    public class Test
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        [JsonIgnore]
        public int QuestionCount => Questions.Count;
        public Test()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            Questions = new ObservableCollection<Question>();
        }

        public bool IsCorrectTest()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (QuestionCount == 0)
            {
                return false;
            }

            foreach (var item in Questions)
            {
                if (!item.IsCorrectQuestion())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
