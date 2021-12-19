# TestSystem
### Программа для прохождения тестов.  
***
Для того, чтобы загрузить тест из вашего файла переопределите метод 
**ReadTestFromFile(string filepath)** в **CustomFileService**, который преобразует содержимое вашего файла в объект типа **Test**.  
В БАЗОВОЙ реализации, метод считывает **JSON-файл** в объект типа **Test**.
***
Для считывания тестов в формате ниже, расскомментируйте метод **ReadTestFromFile(string filepath)** в **CustomFileService**:  
```
  Вопрос
  1) Ответ1; 2) Ответ2; 3) Ответ3; 4) Ответ4; 5) Ответ5;
  Правильный вариант ответа
  [Пустая строка]
```
***
Скачать тест по АВС в формате .TXT можно [здесь](https://drive.google.com/file/d/1CIjA2186x9k-OWAo9Sv5pIMt2bnhY9R-/view?usp=sharing).  
Скачать тест по АВС в формате .JSON можно [здесь](https://drive.google.com/file/d/1nzqYZWv5gVMtemRKUDg1tnb7upAAsW-K/view?usp=sharing).

Далее его необходимо загрузить, нажав на соответствующую кнопку в меню программы.