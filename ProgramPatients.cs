using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            PsychHospital investigativeCommittee = new PsychHospital();

            investigativeCommittee.Work();
        }
    }

    class PsychHospital
    {
        private List<Patients> _patients = new List<Patients>();

        public PsychHospital()
        {
            _patients.Add(new Patients("Петров Антон Борисов", 12, "Шиза"));
            _patients.Add(new Patients("Темеев Дмитрий Калугин", 28, "Депрессия"));
            _patients.Add(new Patients("Буянов Владимир Иванов", 9, "Аутизм"));
            _patients.Add(new Patients("Иванов Николай Захаров", 50, "ПТСР"));
            _patients.Add(new Patients("Захаров Иван Антонов", 40, "ПТСР"));
            _patients.Add(new Patients("Ибрагимов Руслан Дудаев", 21, "Шиза"));
            _patients.Add(new Patients("Могильников Алексей Абрамов", 35, "Депрессия"));
            _patients.Add(new Patients("Сокарев Евгений Владимиров", 30, "ОКР"));
            _patients.Add(new Patients("Иванов Андропов Михайлов", 47, "ОКР"));
            _patients.Add(new Patients("Дударин Иван Николаев", 7, "Аутизм"));
        }

        public void Work()
        {
            const string SortByNameCommand = "1";
            const string SortByAgeCommand = "2";
            const string SearchByDiseaseCommand = "3";
            const string ExitCommand = "4";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("** Психушка **");
                Console.WriteLine("Пациенты: ");

                UserUtils.ShowCollection(_patients);

                Console.WriteLine($"Команда для сортировки пациентов по ФИО: {SortByNameCommand}");
                Console.WriteLine($"Команда для сортировки по возрасту: {SortByAgeCommand}");
                Console.WriteLine($"Команда для поиска по заболеванию: {SearchByDiseaseCommand}");
                Console.WriteLine($"Команда для выхода: {ExitCommand}");

                switch (Console.ReadLine())
                {
                    case SortByNameCommand:
                        SortByName();
                        break;

                    case SortByAgeCommand:
                        SortByAge();
                        break;

                    case SearchByDiseaseCommand:
                        SearchByDisease();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нету.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void SortByName()
        {
            var orderedPatients = _patients.OrderBy(patient => patient.FullName).ToList();

            UserUtils.ShowCollection(orderedPatients);
        }

        private void SortByAge()
        {
            var orderedPatients = _patients.OrderBy(patient => patient.Age).ToList();

            UserUtils.ShowCollection(orderedPatients);
        }

        private void SearchByDisease()
        {
            Console.WriteLine("Введите заболевание:");
            string userInput = Console.ReadLine();

            var filteredPatients = _patients.Where(patient => patient.Disease == userInput).ToList();

            if (filteredPatients.Count <= 0)
            {
                Console.WriteLine("Пациентов с таким заболеванием нету.");
            }
            else
            {
                UserUtils.ShowCollection(filteredPatients);
            }
        }
    }

    class Patients
    {
        public Patients(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }

        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Фио: {FullName} Возраст: {Age} Заболевание: {Disease}");
        }
    }

    static class UserUtils
    {
        public static void ShowCollection(List<Patients> collection)
        {
            Console.WriteLine();

            for (int i = 0; i < collection.Count; i++)
            {
                collection[i].ShowInfo();
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}