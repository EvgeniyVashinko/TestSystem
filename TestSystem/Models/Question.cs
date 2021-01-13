using DevExpress.Mvvm;
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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        public Question()
        {
            Id = Guid.NewGuid();
            Answers = new ObservableCollection<Answer>();
        }
    }
}
