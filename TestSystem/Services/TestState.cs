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
                MixQuestions();
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

        private void MixQuestions()
        {
            Random rnd = new Random();
            int count = CurrentTest.QuestionCount;

            for (int i = 0; i < count; i++)
            {
                Swap(CurrentTest.Questions, rnd.Next(count), rnd.Next(count));
            }
        }

        private void MixAnswers()
        {
            Random rnd = new Random();
            int count;

            foreach (var question in CurrentTest.Questions)
            {
                count = question.Answers.Count;

                for (int i = 0; i < count; i++)
                {
                    Swap(question.Answers, rnd.Next(count), rnd.Next(count));
                }
            }     
        }

        private void Swap<T>(ObservableCollection<T> col ,int i1, int i2) where T : class
        {
            T temp = col[i1];
            col[i1] = col[i2];
            col[i2] = temp;
        }
    }
}
