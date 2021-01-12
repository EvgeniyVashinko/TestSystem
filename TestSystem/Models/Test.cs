using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Models
{
    public class Test : BindableBase
    {
        public string Name { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        public int QuestionCount => Questions.Count;
        public Test()
        {
            Questions = new ObservableCollection<Question>();
        }
    }
}
