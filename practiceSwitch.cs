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

        string[] dossiers = new string[0];

        int userInput;

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
            userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case CommandAddDossier:
                    dossiers = AddDossier(fios, fiosJobs, dossiers);
                    break;

                case CommandPrintAllDossier:
                    PrintAllDossiers(dossiers);
                    break;

                case CommandDeleteDossier:
                    dossiers = DeleteDossier(dossiers);
                    break;

                case CommandFindWithSurname:
                    FindWithSurname(dossiers);
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

    static string[] AddDossier(string[] fio, string[] fioJob, string[] dossier)
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
                string[] tempDossier = new string[dossier.Length + 1];

                for (int i = 0; i < dossier.Length; i++)
                {
                    tempDossier[i] = dossier[i];
                }

                tempDossier[tempDossier.Length - 1] = "Полное имя: " + fio[indexOfFullName] + " Должность: " + fioJob[indexOfJob];

                dossier = tempDossier;

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

    static string[] DeleteDossier(string[] dossier)
    {
        Console.Write("Введите индекс досье которое хотите удалить: ");
        int userIndex = Convert.ToInt32(Console.ReadLine());
        userIndex = userIndex - 1;

        if (userIndex > dossier.Length - 1 || userIndex < 0)
        {
            Console.WriteLine("Индекс находится за границей массива");

            return dossier;
        }
        else
        {
            string tempElement = dossier[dossier.Length - 1];
            dossier[dossier.Length - 1] = dossier[userIndex];
            dossier[userIndex] = tempElement;

            string[] tempDossier = new string[dossier.Length - 1];

            for (int i = 0; i < tempDossier.Length; i++)
            {
                tempDossier[i] = dossier[i];
            }

            return tempDossier;
        }
    }

    static void PrintAllDossiers(string[] dossier)
    {
        Console.WriteLine("Все досье: \n");

        for (int i = 0; i < dossier.Length; i++)
        {
            int dossierNumber = i + 1;

            Console.Write($"Досье Номер {dossierNumber} {dossier[i]} - ");
        }
    }

    static void FindWithSurname(string[] fullNames)
    {
        string userInput;
        Console.WriteLine("Введите фамилию:");
        userInput = Console.ReadLine();

        bool isGetSurname = false;
        bool isGetInfoOfGetSurname = false;

        for (int i = 0; i < fullNames.Length; i++)
        {
            string[] words = fullNames[i].Split(' ');

            isGetSurname = CheckTrueElement(userInput, words);
           
            if (isGetSurname)
            {
                isGetInfoOfGetSurname = true;
                Console.WriteLine(fullNames[i]);
            }
        }

        if (isGetInfoOfGetSurname == false)
        {
            Console.WriteLine("Не сотрудников с такой фамилией");
        }
    }

    static int GetIndexAndElement(string userInfo, string[] array, out bool isTrueInfo)
    {
        int indexOfJob = FindIndexWithElement(userInfo, array);
        isTrueInfo = CheckTrueElement(userInfo, array);

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

    static bool CheckTrueElement(string userInfo, string[] Array)
    {
        bool isTrueInfo = false;

        for (int i = 0; i < Array.Length; i++)
        {
            if (userInfo == Array[i])
            {
                isTrueInfo = true;
            }
        }

        if (isTrueInfo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static void PrintArray(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }
}