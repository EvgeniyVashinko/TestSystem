using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;

namespace TestSystem.Services
{
    public class TestState
    {
        private Test currentTest;
        public Test CurrentTest
        {
            get => currentTest;
            set
            {
                currentTest = value;
                ClearState();
            }
        }
        private int questionNum;
        public int QuestionNum
        {
            get => questionNum;
            set
            {
                questionNum = value > CurrentTest.QuestionCount ? CurrentTest.QuestionCount : value;
            }
        }
        public ObservableCollection<Question> RightAnswers { get; set; }
        public ObservableCollection<Question> WrongAnswers { get; set; }
        public bool ShowRightAnswers { get; set; }
        public int TestResult => RightAnswers.Count / CurrentTest.QuestionCount;

        private void ClearState()
        {
            RightAnswers = new ObservableCollection<Question>();
            WrongAnswers = new ObservableCollection<Question>();
        }

    }
}
