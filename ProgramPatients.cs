using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            MilitaryLeadership militaryLeadership = new MilitaryLeadership();

            militaryLeadership.Work();
        }
    }

    class MilitaryLeadership
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public MilitaryLeadership()
        {
            _soldiers.Add(new Soldier("Иван", "Лейтенант", 7, "АК-47"));
            _soldiers.Add(new Soldier("Сергей", "Сержант", 12, "АК-74"));
            _soldiers.Add(new Soldier("Дмитрий", "Рядовой", 8, "АК-12"));
            _soldiers.Add(new Soldier("Владлен", "Майор", 9, "АК-15"));
            _soldiers.Add(new Soldier("Александр", "Майор", 2, "Пистолет Макарова"));
        }

        public void Work()
        {
            ShowSoldiers();
            Console.WriteLine("Нажмите на любую кнопку чтобы получить только имя и звание");
            Console.ReadLine();
            FilterSoldiers();
        }

        private void FilterSoldiers()
        {
            var filteredSoldiers = _soldiers.Select(soldier => new { Name = soldier.Name, Rank = soldier.Rank }).ToList();

            for (int i = 0; i < filteredSoldiers.Count; i++)
            {
                Console.WriteLine($"Имя: {filteredSoldiers[i].Name} Звание: {filteredSoldiers[i].Rank}");
            }
        }

        private void ShowSoldiers()
        {
            Console.WriteLine();

            for (int i = 0; i < _soldiers.Count; i++)
            {
                _soldiers[i].ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Soldier
    {
        private int _serviceLive;
        private string _gun;

        public Soldier(string name, string rank, int serviceLife, string gun)
        {
            Name = name;
            _gun = gun;
            _serviceLive = serviceLife;
            Rank = rank;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {Name} звание: {Rank} срок службы: {_serviceLive} вооружение: {_gun}");
        }
    }
}