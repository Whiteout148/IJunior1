using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            InvestigativeCommittee investigativeCommittee = new InvestigativeCommittee();

            investigativeCommittee.Work();
        }
    }

    class InvestigativeCommittee
    {
        private List<Criminal> _criminals = new List<Criminal>();
        
        public InvestigativeCommittee()
        {
            _criminals.Add(new Criminal("Ороспу Евлады", "Турок", 170, 100, true));
            _criminals.Add(new Criminal("Яррак кафа", "Турок", 170, 100, false));
            _criminals.Add(new Criminal("Ахмед Амыноглу", "Турок", 180, 80, false));
            _criminals.Add(new Criminal("Иван Сундуков", "Русский", 165, 70, true));
            _criminals.Add(new Criminal("Владимир Буянов", "Русский", 175, 90, false));
            _criminals.Add(new Criminal("Алексей Смирнов", "Русский", 175, 90, false));
        }

        public void Work()
        {
            Console.WriteLine("** Следственный комитет **");
            Console.WriteLine("Напишите рост вес и национальность для поиска преступника.");
            Console.WriteLine("Напишите рост:");
            int userHeight = UserUtils.ReadInt();
            Console.WriteLine("Напишите вес:");
            int userWeight = UserUtils.ReadInt();
            Console.WriteLine("Напишите национальность:");
            string userNationality = Console.ReadLine();

            ShowCriminalsWithIndications(userNationality, userHeight, userWeight);
        }

        private void ShowCriminalsWithIndications(string nationality, int height, int weight)
        {
            var filteredCriminals = _criminals.Where(criminal => criminal.Nationality == nationality && criminal.Height == height && criminal.Weight == weight && criminal.IsInPrison == false).Select(Criminal => Criminal);

            int criminalsCount = 0;

            if (criminalsCount < 1)
            {
                Console.WriteLine("Не нашлось преступников с такими данными.");
            }
            else
            {
                Console.WriteLine("Все преступники с такими данными которые на свободе:");

                foreach (var filteredCriminal in filteredCriminals)
                {
                    filteredCriminal.ShowInfo();

                    criminalsCount++;
                }
            }
        }
    }

    class Criminal
    {
        public Criminal(string fullName, string nationality, int height, int weight, bool isInPrison)
        {
            FullName = fullName;
            Nationality = nationality;
            Height = height;
            Weight = weight;
            IsInPrison = isInPrison;
        }

        public string FullName { get; private set; }
        public string Nationality { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public bool IsInPrison { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {FullName} Национальность: {Nationality} Рост: {Height} Вес: {Weight}");
        }
    }

    static class UserUtils
    {
        public static int ReadInt()
        {
            string userInput = " ";
            int result = 0;

            while (int.TryParse(userInput, out result) == false)
            {
                userInput = Console.ReadLine();
            }

            return result;
        }
    }
}