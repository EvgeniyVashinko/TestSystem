using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Models;

namespace TestSystem.Services
{
    public class TestService
    {
        private readonly DialogService _dialogService;
        private readonly CustomFileService _customFileService;
        private readonly PageNavigationService _navigationService;
        private readonly Repository _repository;

        public TestService(DialogService dialogService, CustomFileService customFileService, PageNavigationService navigationService, Repository repository)
        {
            _dialogService = dialogService;
            _customFileService = customFileService;
            _navigationService = navigationService;
            _repository = repository;
        }

        public Test GetTest(Guid testId) 
        {
            return _repository.FindAll<Test>().FirstOrDefault(x => x.Id == testId);
        }

        public void LoadTest()
        {
            if (_dialogService.OpenFileDialog(out string filepath))
            {
                try
                {
                    LoadTestFromFile(filepath);
                    _dialogService.ShowMessage("Тест загружен успешно!");
                }
                catch(Exception e)
                {
                    _dialogService.ShowMessage(e.Message);
                }

            }
            else
            {
                _dialogService.ShowMessage("Неверный путь к файлу");
            }
        }

        public void SaveTest(Test test)
        {
            test.CreationDate = DateTime.Now;
            _repository.Save(test);
            _dialogService.ShowMessage("Тест сохранен!");
            _navigationService.GoBack();
        }

        public void SaveTestToFile(Test test)
        {
            if (_dialogService.SaveFileDialog(out string filepath))
            {
                try
                {
                    _customFileService.WriteTestToFile(filepath, test);
                    _dialogService.ShowMessage("Тест сохранен успешно!");
                }
                catch (Exception e)
                {
                    _dialogService.ShowMessage(e.Message);
                }
            }
            else
            {
                _dialogService.ShowMessage("Неверный путь к файлу");
            }
        }

        public void DeleteTest(Guid testId)
        {
            _repository.Delete<Test>(testId);
            _dialogService.ShowMessage("Тест удален!");
            _navigationService.GoBack();
        }

        private void LoadTestFromFile(string filepath)
        {
            var test = _customFileService.ReadTestFromFile(filepath);

            if (_repository.FindAll<Test>().Any(x => x.Id == test.Id))
            {
                test.Id = Guid.NewGuid();
                test.CreationDate = DateTime.Now;
            }

            _repository.Save(test);
        }
    }
}
