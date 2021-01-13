using DevExpress.Mvvm;
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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        public int QuestionCount => Questions.Count;
        public Test()
        {
            Id = Guid.NewGuid();
            Questions = new ObservableCollection<Question>();
        }
    }
}
