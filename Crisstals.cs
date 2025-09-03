using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    static void Main()
    {
        const string CommandAddDossier = "1";
        const string CommandPrintAllDossier = "2";
        const string CommandDeleteDossier = "3";
        const string CommandFindWithSurname = "4";
        const string CommandExit = "5";

        bool isWork = true;

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

        string dossier = "";
        string userInput;
        char divider = '*';

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
                    fullNames = AddDossier(ref jobs, fullNames);
                    break;

                case CommandPrintAllDossier:
                    PrintAllDossiers(fullNames, jobs);
                    break;

                case CommandDeleteDossier:
                    fullNames = DeleteDossier(fullNames, ref jobs);
                    break;

                case CommandFindWithSurname:
                    WriteWithSurname(fullNames, jobs);
                    break;

                case CommandExit:
                    Console.WriteLine("Выход из программы");
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

    static string[] AddDossier(ref string[] jobs, string[] fullName)
    {
        Console.Write("Введите фио: ");
        string userFullName = Console.ReadLine();
        Console.Write("Введите работу: ");
        string userJob = Console.ReadLine();

        fullName = AddElement(fullName, userFullName);
        jobs = AddElement(jobs, userJob);

        return fullName;
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

    static string[] DeleteDossier(string[] fullNames, ref string[] jobs)
    {
        Console.Write("Введите индекс с нужным досье: ");
        string userIndex = Console.ReadLine();
        int resultIndex;
        bool isConvert = ReadInt(out resultIndex, userIndex);
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
        else
        {
            Console.WriteLine("Ошибка! нужно ввести целое число");
        }

        return fullNames;
    }

    static string[] DeleteElement(string[] array, int index)
    {
        string tempElement = array[index];
        array[index] = array[array.Length - 1];
        array[array.Length - 1] = tempElement;

        string[] tempArray = new string[array.Length - 1];

        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = array[i];
        }

        return tempArray;
    }

    static void PrintDossier(string[] fullNames, string[] jobs, int index, int number = 0, char divider = ' ')
    {
        Console.Write($"Полное имя: {fullNames[index]} Должность: {jobs[index]} {divider} ");
    }

    static void PrintAllDossiers(string[] fullNames, string[] jobs)
    {
        Console.WriteLine("Все досье: \n");

        int dossierNumber = 1;

        for (int i = 0; i < fullNames.Length - 1; i++)
        {

            Console.Write("Досье номер: " + dossierNumber + " ");
            PrintDossier(fullNames, jobs, i, dossierNumber, '-');

            dossierNumber++;
        }

        Console.Write("Досье номер: " + dossierNumber + " ");

        PrintDossier(fullNames, jobs, fullNames.Length - 1, dossierNumber += 1);
    }

    static void WriteWithSurname(string[] fullName, string[] jobs)
    {
        string userInput;
        Console.WriteLine("Введите фамилию:");
        userInput = Console.ReadLine();

        bool isGetSurname = false;

        for (int i = 0; i < fullName.Length; i++)
        {
            string[] dividedFullNames = fullName[i].Split(' ');

            if (userInput == dividedFullNames[0])
            {
                isGetSurname = true;
                Console.Write("Досье с фамилией: " + dividedFullNames[0] + " ");
                PrintDossier(fullName, jobs, i);
            }
        }

        if (isGetSurname == false)
        {
            Console.WriteLine("Не сотрудников с такой фамилией");
        }
    }

    static bool ReadInt(out int result, string userInput)
    {
        return int.TryParse(userInput, out result);
    }
}