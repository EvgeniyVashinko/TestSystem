using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;
using TestSystem.Services.Interfaces;

namespace TestSystem.Services
{
    public class FileService : IFileService
    {
        private readonly string _path = @$"C:\Users\{Environment.UserName}\Documents";
        private readonly string _subpath = @"TestSystem\tests";

        public string GetFilePath(Guid testId) => @$"C:\Users\{Environment.UserName}\Documents\testSystem\tests\{testId}.json";

        public void SaveTestFile(Test test)
        {
            DirectoryInfo dir = new DirectoryInfo(_path + "\\" + _subpath);

            if (!dir.Exists)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(_path);

                dirInfo.CreateSubdirectory(_subpath);
            }

            var filepath = GetFilePath(test.Id);

            File.WriteAllText(filepath, JsonConvert.SerializeObject(test));
        }

        public virtual Test ReadTestFromFile(string filepath)
        {
            var test = new Test();

            if (File.Exists(filepath))
            {
                test = JsonConvert.DeserializeObject<Test>(filepath);
            }

            return test;
        }

        public void DeleteTestFile(Guid testId)
        {
            var filepath = GetFilePath(testId);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
