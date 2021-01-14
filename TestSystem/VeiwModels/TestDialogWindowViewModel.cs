using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestSystem.Services;

namespace TestSystem.VeiwModels
{
    public class TestDialogWindowViewModel : BindableBase
    {
        private readonly TestState _testState;
        private int questionNum;
        public int QuestionNum
        {
            get => questionNum;
            set
            {
                if (value < 0)
                {
                    questionNum = 0;
                }
                else if (value > MaxQuestionNum)
                {
                    questionNum = MaxQuestionNum;
                }
                else
                {
                    questionNum = value;
                }
            }
        }
        public int MaxQuestionNum { get; set; }
        public bool MixQuestions { get; set; }
        public bool MixAnswers { get; set; }
        private int startIntervalNum;
        public int StartIntervalNum
        {
            get => startIntervalNum;
            set
            {
                if (value < 1 || value > MaxQuestionNum || value > EndIntervalNum)
                {
                    startIntervalNum = 1;
                }
                else
                {
                    startIntervalNum = value;
                }
            }
        }
        private int endIntervalNum;
        public int EndIntervalNum
        {
            get => endIntervalNum;
            set
            {
                if (value < 1 || value > MaxQuestionNum || value < StartIntervalNum)
                {
                    endIntervalNum = MaxQuestionNum;
                }
                else
                {
                    endIntervalNum = value;
                }
            }
        }

        public bool CanCheckAnswers { get; set; }
        public TestDialogWindowViewModel(TestState testState)
        {
            _testState = testState;
            StartIntervalNum = 1;
            QuestionNum = EndIntervalNum = MaxQuestionNum = _testState.CurrentTest.QuestionCount;
            MixQuestions = MixAnswers = CanCheckAnswers = true;
        }

        public ICommand ContinueCommand => new DelegateCommand<Window>((win) =>
        {
            _testState.ChangeTestParameters(StartIntervalNum, EndIntervalNum, QuestionNum, MixQuestions, MixAnswers);
            _testState.CanCheckAnswer = CanCheckAnswers;
            win.DialogResult = true;
        }, (win) => QuestionNum > 0);

        public ICommand SetMaxCommand => new DelegateCommand(() =>
        {
            QuestionNum = MaxQuestionNum;
        });

        public ICommand SetMaxIntervalCommand => new DelegateCommand(() =>
        {
            EndIntervalNum = MaxQuestionNum;
        });

        public ICommand SetMinCommand => new DelegateCommand(() =>
        {
            StartIntervalNum = 1;
        });
    }
}
