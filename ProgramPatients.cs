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
            InvestigativeCommittee investigativeCommittee = new InvestigativeCommittee("Антиправительственное");

            investigativeCommittee.Work();
        }
    }

    class InvestigativeCommittee
    {
        private List<Criminal> _criminals = new List<Criminal>();
        private string _crimeToLiberation;

        public InvestigativeCommittee(string crimeToLiberation)
        {
            _criminals.Add(new Criminal("Хлебов Антон Глебов", "Антиправительственное"));
            _criminals.Add(new Criminal("Темеев Дмитрий Калугин", "Антиправительственное"));
            _criminals.Add(new Criminal("Буянов Владимир Иванов", "Воровство"));
            _criminals.Add(new Criminal("Иванов Николай Захаров", "Убийство"));

            _crimeToLiberation = crimeToLiberation;
        }

        public void Work()
        {
            Console.WriteLine("До амнистии");
            Console.WriteLine();

            UserUtils.ShowCollection(_criminals);

            Console.WriteLine();
            Console.WriteLine("После амнистии");
            Console.WriteLine();

            Amnesty();

            UserUtils.ShowCollection(_criminals);
        }

        private void Amnesty()
        {
            var filteredCriminals = _criminals.Where(criminal => criminal.Crime != _crimeToLiberation).Select(criminal => criminal).ToList();

            _criminals = filteredCriminals;
        }
    }

    class Criminal
    {
        private string _fullName;

        public Criminal(string fullName, string crime)
        {
            _fullName = fullName;
            Crime = crime;
        }

        public string Crime { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Фио: {_fullName} Преступление: {Crime}");
        }
    }

    static class UserUtils
    {
        public static void ShowCollection(List<Criminal> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                collection[i].ShowInfo();
            }
        }
    }
}