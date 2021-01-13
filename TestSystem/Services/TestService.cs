using System;
using System.Collections.Generic;
using System.Text;
using TestSystem.Models;

namespace TestSystem.Services
{
    public class TestService
    {
        private readonly DialogService _dialogService;
        private readonly FileService _fileService;
        private readonly CustomFileService _customFileService;

        public TestService(DialogService dialogService, FileService fileService, CustomFileService customFileService)
        {
            _dialogService = dialogService;
            _fileService = fileService;
            _customFileService = customFileService;
        }

        public Test GetTest(Guid testId) 
        {
            var filepath = _fileService.GetFilePath(testId);
            return _fileService.ReadTestFromFile(filepath);
        }

        public void LoadTest() // загружаем тест по адресу filepath в tests/
        {
            if (_dialogService.OpenFileDialog(out string filepath))
            {
                LoadTestFromFile(filepath);
                _dialogService.ShowMessage("Тест загружен успешно!");
            }
            else
            {
                _dialogService.ShowMessage("Неверный путь к файлу");
            }
        }

        public void SaveTest(Test test)
        {
            _fileService.SaveTestFile(test);
        }

        public void DeleteTest(Guid testId)
        {
            _fileService.DeleteTestFile(testId);
        }

        private void LoadTestFromFile(string filepath)
        {
            var test = _customFileService.ReadTestFromFile(filepath);

            _fileService.SaveTestFile(test);
        }
    }
}
