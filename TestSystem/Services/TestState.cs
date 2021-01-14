using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;
using TestSystem;

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
        public int TestResult => (int)((double)RightAnswers.Count / CurrentTest.QuestionCount * 100);

        public bool CanCheckAnswer { get; set; }

        private void ClearState()
        {
            RightAnswers = new ObservableCollection<Question>();
            WrongAnswers = new ObservableCollection<Question>();
        }

        public void ChangeTestParameters(int start, int end, int qNum, bool mixQ, bool mixA)
        {
            SelectQuestionInterval(start, end);

            if (mixQ)
            {
                CurrentTest.Questions.Shuffle();
            }

            if (mixA)
            {
                MixAnswers();
            }

            SelectQuestions(qNum);
        }

        private void SelectQuestionInterval(int start, int end)
        {
            CurrentTest.Questions = new ObservableCollection<Question>(CurrentTest.Questions.Skip(start - 1).Take(end - start + 1));
        }

        private void SelectQuestions(int num)
        {
            CurrentTest.Questions = new ObservableCollection<Question>(CurrentTest.Questions.Take(num));
        }

        private void MixAnswers()
        {
            foreach (var question in CurrentTest.Questions)
            {
                question.Answers.Shuffle();
            }
        }
    }
}
