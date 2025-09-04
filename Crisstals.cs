using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string CommandAddDossier = "1";
        const string CommandPrintAllDossier = "2";
        const string CommandDeleteDossier = "3";
        const string CommandFindWithSurname = "4";
        const string CommandExit = "5";

        string[] fullNames =
        {
            "Галеев Артем Иванов",
            "Гордон Иван Владимиров",
        };

        string[] jobs =
        {
            "Учитель",
            "Программист",
        };

        bool isWork = true;
        string userInput;

        while (isWork)
        {
            Console.WriteLine("** Кадровый Учет **\n");
            Console.WriteLine();
            Console.WriteLine("Доступные команды:");
            Console.WriteLine($"Добавить досье: {CommandAddDossier}");
            Console.WriteLine($"Посмотреть все досье: {CommandPrintAllDossier}");
            Console.WriteLine($"Удалить досье: {CommandDeleteDossier}");
            Console.WriteLine($"Искать по фамилии: {CommandFindWithSurname}");
            Console.WriteLine($"Выход: {CommandExit}");
            Console.Write("Введите нужную команду: ");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandAddDossier:
                    AddDossier(ref jobs, ref fullNames);
                    break;

                case CommandPrintAllDossier:
                    PrintAllDossiers(fullNames, jobs);
                    break;

                case CommandDeleteDossier:
                    DeleteDossier(ref fullNames, ref jobs);
                    break;

                case CommandFindWithSurname:
                    FindDossierWithSurname(fullNames, jobs);
                    break;

                case CommandExit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Нету Такой Команды");
                    break;

            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    static void AddDossier(ref string[] jobs, ref string[] fullNames)
    {
        bool isAddDossier = AddFullNameAndJob(ref jobs, ref fullNames);

        Console.WriteLine(isAddDossier ? "Досье добавлено" : "Не удалось добавить досье");
    }

    static bool AddFullNameAndJob(ref string[] jobs, ref string[] fullName)
    {
        Console.Write("Введите фио (3 слова 'Имя Фамилия Отчество' не больше не меньше): ");
        string userFullName = Console.ReadLine();
        Console.Write("Введите должность: ");
        string userJob = Console.ReadLine();

        string[] dividedFullName = userFullName.Split(' ');

        int quantityOfWords = 3;

        if (dividedFullName.Length == quantityOfWords)
        {
            fullName = AddElement(fullName, userFullName);
            jobs = AddElement(jobs, userJob);

            return true;
        }
        else
        {
            return false;
        }
    }

    static string[] AddElement(string[] array, string element)
    {
        string[] tempArray = new string[array.Length + 1];

        for (int i = 0; i < array.Length; i++)
        {
            tempArray[i] = array[i];
        }

        tempArray[tempArray.Length - 1] = element;

        return tempArray;
    }

    static void DeleteDossier(ref string[] fullNames, ref string[] jobs)
    {
        bool isHaveDossier = GetInformationAboutAvability(fullNames);

        if (isHaveDossier)
        {
            Console.Write("Введите индекс с нужным досье: ");
            string userIndex = Console.ReadLine();
            int resultIndex;
            bool isConvert = int.TryParse(userIndex, out resultIndex);
            resultIndex = resultIndex - 1;

            if (isConvert)
            {
                if (resultIndex > fullNames.Length - 1 || resultIndex < 0)
                {
                    Console.WriteLine("Нету досье с таким индексом!");
                }
                else
                {
                    fullNames = DeleteElement(fullNames, resultIndex);
                    jobs = DeleteElement(jobs, resultIndex);
                }
            }
        }
        else
        {
            Console.WriteLine("У вас нету досье");
        }
    }

    static string[] DeleteElement(string[] array, int index)
    {
        string[] newArray = new string[array.Length - 1];

        int newIndex = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (i == index)
                continue;

            newArray[newIndex++] = array[i];
        }

        return newArray;
    }

    static void PrintDossier(string[] fullNames, string[] jobs, int index, char divider = ' ')
    {
        Console.Write($"Полное имя: {fullNames[index]} Должность: {jobs[index]} {divider} ");
    }

    static bool GetInformationAboutAvability(string[] array)
    {
        return array.Length > 0;
    }

    static void PrintAllDossiers(string[] fullNames, string[] jobs)
    {
        Console.WriteLine("Все досье: \n");

        bool isHaveDossier = GetInformationAboutAvability(fullNames);

        if (isHaveDossier)
        {
            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.Write("Досье номер: " + (i + 1) + " ");
                char divider = (i == fullNames.Length - 1) ? ' ' : '-';
                PrintDossier(fullNames, jobs, i, divider);
            }
        }
        else
        {
            Console.WriteLine("У вас нету в наличии досье");
        }
    }

    static void FindDossierWithSurname(string[] fullName, string[] jobs)
    {
        string userInput;
        Console.WriteLine("Введите фамилию:");
        userInput = Console.ReadLine();

        bool isGetSurname = false;

        for (int i = 0; i < fullName.Length; i++)
        {
            string[] dividedFullNames = fullName[i].Split(' ');

            if (userInput.ToLower() == dividedFullNames[0].ToLower())
            {
                isGetSurname = true;
                Console.Write("Досье с фамилией: " + dividedFullNames[0] + " ");
                PrintDossier(fullName, jobs, i);
            }
        }

        if (isGetSurname == false)
        {
            Console.WriteLine("Нету сотрудников с такой фамилией");
        }
    }
}