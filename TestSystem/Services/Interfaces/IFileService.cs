using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.Models;

namespace TestSystem.Services.Interfaces
{
    interface IFileService
    {
        public void SaveTestFile(Test test);
        public Test ReadTestFromFile(string filepath);
        public void DeleteTestFile(Guid testId);
    }
}
