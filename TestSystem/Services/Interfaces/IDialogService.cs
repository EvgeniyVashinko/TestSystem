using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.Services.Interfaces
{
    interface IDialogService
    {
        void ShowMessage(string message);
        bool OpenFileDialog(out string path);
        bool SaveFileDialog(out string path);
    }
}
