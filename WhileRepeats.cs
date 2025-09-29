using System;
using System.Collections.Generic;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Personal libraryPersonal = new Personal();

            libraryPersonal.Work();
        }
    }

    class Library
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(string name, string writerName, int age)
        {
            _books.Add(new Book(name, writerName, age));
            Console.WriteLine("Книга добавлена.");
        }

        public void ShowBooks()
        {
            for (int i = 0; i < _books.Count; i++)
            {
                _books[i].ShowInfo();
            }
        }

        public void RemoveBookWithId(int id)
        {
            _books.RemoveAt(id - 1);
        }

        public bool TryToGetBookName(string userBookName)
        {
            bool isContainsName = false;

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Name == userBookName)
                {
                    _books[i].ShowInfo();
                    isContainsName = true;
                }
            }

            return isContainsName;
        }

        public bool TryToGetWriter(string userWriter)
        {
            bool isContainsWriter = false;

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].WriterName == userWriter)
                {
                    _books[i].ShowInfo();
                    isContainsWriter = true;
                }
            }

            return isContainsWriter;
        }

        public bool TryToGetAge(int userAge)
        {
            bool isContainsAge = false;

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Age == userAge)
                {
                    _books[i].ShowInfo();
                    isContainsAge = true;
                }
            }

            return isContainsAge;
        }

        public bool TryToGetId(int userId)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (userId == _books[i].Id)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class Personal
    {
        private Library _library = new Library();

        public void Work()
        {
            const string AddBookCommand = "1";
            const string RemoveBookCommand = "2";
            const string FindWithNameCommand = "3";
            const string FindWithWriterCommand = "4";
            const string FindWithAgeCommand = "5";
            const string ExitCommand = "6";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("** Библиотека **");
                Console.WriteLine("Книги:");
                Console.WriteLine();
                _library.ShowBooks();
                Console.WriteLine();
                Console.WriteLine("Доступные команды:");
                Console.WriteLine($"Добавить книгу: {AddBookCommand}");
                Console.WriteLine($"Удалить книгу: {RemoveBookCommand}");
                Console.WriteLine($"Искать книгу по названию: {FindWithNameCommand}");
                Console.WriteLine($"Искать по автору: {FindWithWriterCommand}");
                Console.WriteLine($"Искать по дате выпуска: {FindWithAgeCommand}");
                Console.WriteLine($"Выход из библиотеки: {ExitCommand}");
                string userInput = GetUserMessage("Введите команду:");

                switch (userInput)
                {
                    case AddBookCommand:
                        AddBookToLibrary();
                        break;

                    case RemoveBookCommand:
                        DeleteBook();
                        break;

                    case FindWithNameCommand:
                        FindWithBookName();
                        break;

                    case FindWithWriterCommand:
                        FindWithBookWriter();
                        break;

                    case FindWithAgeCommand:
                        FindWithBookAge();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddBookToLibrary()
        {
            string userName = GetUserMessage("Введите книгу:");
            string userWriter = GetUserMessage("Введите писателя:");
            string userAge = GetUserMessage("Введите год выпуска книги:");

            if (TryToConvert(out int resultAge, userAge))
            {
                _library.AddBook(userName, userWriter, resultAge);
            }
        }

        private void DeleteBook()
        {
            string userId = GetUserMessage("Введите номер книги:");

            if (TryToConvert(out int resultId, userId))
            {
                if (_library.TryToGetId(resultId))
                {
                    _library.RemoveBookWithId(resultId);
                }
                else
                {
                    Console.WriteLine("Неверный номер книги!");
                }
            }
        }

        private void FindWithBookName()
        {
            string userBook = GetUserMessage("Введите название книги:");
            bool isContainsBook = _library.TryToGetBookName(userBook);

            if (isContainsBook == false)
            {
                Console.WriteLine("Не удалось найти книгу по названию.");
            }
        }

        private void FindWithBookWriter()
        {
            string userWriter = GetUserMessage("Введите имя автора:");
            bool isContainsWriter = _library.TryToGetWriter(userWriter);

            if (isContainsWriter == false)
            {
                Console.WriteLine("Не удалось найти книгу по автору.");
            }
        }

        private void FindWithBookAge()
        {
            string userAge = GetUserMessage("Введите дату выхода книги:");

            if (TryToConvert(out int resultAge, userAge))
            {
                bool isContainsBookAge = _library.TryToGetAge(resultAge);

                if (isContainsBookAge == false)
                {
                    Console.WriteLine("Не удалось найти книгу по возрасту.");
                }
            }
        }

        private bool TryToConvert(out int result, string userMessage)
        {
            if (int.TryParse(userMessage, out result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Введите число!");
            }

            return false;
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }

    class Book
    {
        private static int _idCounter = 1;

        public Book(string name, string writerName, int age)
        {
            Id = _idCounter++;
            Name = name;
            WriterName = writerName;
            Age = age;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string WriterName { get; private set; }
        public int Age { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Книга: {Name} Писатель: {WriterName} Год выпуска: {Age} Номер книги: {Id}");
        }
    }
}
