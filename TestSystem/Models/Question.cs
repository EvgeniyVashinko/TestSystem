using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Models
{
    public class Question : BindableBase
    {
        public string Name { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        public Question()
        {
            Answers = new ObservableCollection<Answer>();
        }
    }
}
