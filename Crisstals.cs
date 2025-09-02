using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    static void Main()
    {
        const int CommandAddDossier = 1;
        const int CommandPrintAllDossier = 2;
        const int CommandDeleteDossier = 3;
        const int CommandFindWithSurname = 4;
        const int CommandExit = 5;

        bool isWork = true;

        string[] fios =
        {
            "Иванов Сергей Папиков",
            "Гордон Иван Владимиров",
            "Галеев Алексей Павлович",
            "Галеев Артем Иванов"
        };

        string[] fiosJobs =
        {
            "Учитель",
            "Программист",
            "Инженер"
        };

        string dossier = "";
        string userInput;
        char divider = '*';

        while (isWork)
        {
            Console.WriteLine("** Кадровый Учет **\n");
            Console.WriteLine("Сотрудники:");
            Console.WriteLine();
            PrintArray(fios);
            Console.WriteLine();
            Console.WriteLine("Должности: ");
            Console.WriteLine();
            PrintArray(fiosJobs);
            Console.WriteLine();
            Console.WriteLine("Доступные команды:");
            Console.WriteLine($"Добавить досье: {CommandAddDossier}");
            Console.WriteLine($"Посмотреть все досье: {CommandPrintAllDossier}");
            Console.WriteLine($"Удалить досье: {CommandDeleteDossier}");
            Console.WriteLine($"Искать по фамилии: {CommandFindWithSurname}");
            Console.WriteLine($"Выход: {CommandExit}");
            Console.Write("Введите нужную команду: ");
            userInput = Console.ReadLine();

            int result;
            bool isTrueConvert = ReadInt(out result, userInput);

            switch (result)
            {
                case CommandAddDossier:
                    dossier = AddDossier(fios, fiosJobs, dossier, divider);
                    break;

                case CommandPrintAllDossier:
                    PrintAllDossiers(dossier, divider);
                    break;

                case CommandDeleteDossier:
                    dossier = DeleteDossier(dossier, divider);
                    break;

                case CommandFindWithSurname:
                    WriteWithSurname(fios, dossier, divider);
                    break;

                case CommandExit:
                    Console.WriteLine("Выход из программы");
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Нету такой команды");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    static string AddDossier(string[] fio, string[] fioJob, string dossier, char divider)
    {
        string userFullName;
        string userJob;

        Console.WriteLine("Введите Фамилию Имя и Отчество сотрудника для добавление в досье");
        userFullName = Console.ReadLine();
        bool isTrueFullName;
        int indexOfFullName = GetIndexAndElement(userFullName, fio, out isTrueFullName);

        if (isTrueFullName)
        {
            Console.WriteLine("Введите должность сотрудника");
            userJob = Console.ReadLine();
            bool isTrueJob;
            int indexOfJob = GetIndexAndElement(userJob, fioJob, out isTrueJob);

            if (isTrueJob)
            {
                if (dossier == "")
                {
                    dossier += "Полное имя: " + fio[indexOfFullName] + " Должность: " + fioJob[indexOfJob];
                }
                else
                {
                    dossier += divider + " - Полное имя: " + fio[indexOfFullName] + " Должность: " + fioJob[indexOfJob];
                }

                Console.WriteLine("Добавлено досье:");
                Console.WriteLine($"Ваш сотрудник: {fio[indexOfFullName]} по должности: {fioJob[indexOfJob]}");
            }
            else
            {
                Console.WriteLine("Нету такой должности");
            }
        }
        else
        {
            Console.WriteLine("Такого сотрудника нету");
        }

        return dossier;
    }

    static string DeleteDossier(string dossier, char divider)
    {
        string[] dossiers = DivideStrokeToArray(dossier, divider);

        Console.Write("Введите индекс досье которое хотите удалить: ");
        int userIndex = Convert.ToInt32(Console.ReadLine());
        userIndex = userIndex - 1;

        if (userIndex > dossiers.Length - 1 || userIndex < 0)
        {
            Console.WriteLine("Нету такого индекса досье");

            return dossier;
        }
        else
        {
            string newDossier = "";

            for (int i = 0; i < dossiers.Length; i++)
            {
                if (i == userIndex)
                    continue;

                if (string.IsNullOrWhiteSpace(dossiers[i]))
                    continue;

                if (i == 0)
                {
                    newDossier += dossiers[i];
                }
                else
                {
                    newDossier += divider + dossiers[i];
                }
            }

            return newDossier;
        }
    }

    static void PrintAllDossiers(string dossier, char divider)
    {
        string[] dossiers = DivideStrokeToArray(dossier, divider);

        Console.WriteLine("Все досье: \n");

        for (int i = 0; i < dossiers.Length; i++)
        {
            int dossierNumber = i + 1;

            Console.Write($"Досье Номер {dossierNumber} {dossiers[i]} ");
        }
    }

    static void WriteWithSurname(string[] fullName, string dossier, char divider)
    {
        string userInput;
        Console.WriteLine("Введите фамилию:");
        userInput = Console.ReadLine();

        bool isGetInfoOfSurname = false;

        string[] dividedDossier = DivideStrokeToArray(dossier, divider);

        for (int i = 0; i < dividedDossier.Length; i++)
        {
            string[] divideDossierToWords = DivideStrokeToArray(dividedDossier[i], ' ');

            bool isGetSurname = GetInfoOfUserElement(userInput, divideDossierToWords);

            if (isGetSurname)
            {
                isGetInfoOfSurname = true;

                Console.WriteLine($"Досье с фамилией {userInput}: {dividedDossier[i]}");
            }
        }


        if (isGetInfoOfSurname == false)
        {
            Console.WriteLine("Не сотрудников с такой фамилией");
        }
    }

    static int GetIndexAndElement(string userInfo, string[] array, out bool isTrueInfo)
    {
        int indexOfJob = FindIndexWithElement(userInfo, array);
        isTrueInfo = GetInfoOfUserElement(userInfo, array);

        return indexOfJob;
    }

    static int FindIndexWithElement(string Element, string[] array)
    {
        int index = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (Element == array[i])
            {
                index = i;
            }
        }

        return index;
    }

    static bool GetInfoOfUserElement(string userInfo, string[] Array)
    {
        for (int i = 0; i < Array.Length; i++)
        {
            if (userInfo == Array[i])
            {
                return true;
            }
        }

        return false;
    }

    static void PrintArray(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    static bool ReadInt(out int result, string userInput)
    {
        return int.TryParse(userInput, out result);
    }

    static string[] DivideStrokeToArray(string dossier, char divider)
    {
        string[] dividedDossier = dossier.Split(new[] { divider }, StringSplitOptions.RemoveEmptyEntries);

        return dividedDossier;
    }
}