using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ujin
{
    internal class Program
    {
        static void Main()
        {
            ArtemWolf artemWolf = new ArtemWolf(2026);

            artemWolf.Work();
        }
    }

    class ArtemWolf
    {
        private int _currentDate;

        private List<Stew> _stews = new List<Stew>();

        public ArtemWolf(int currentDate)
        {
            _currentDate = currentDate;

            _stews.Add(new Stew("С рыбой", 2018, 2023));
            _stews.Add(new Stew("С говядиной", 2017, 2028));
            _stews.Add(new Stew("С курицей", 2020, 2024));
            _stews.Add(new Stew("Со свининой", 2018, 2030));
        }

        public void Work()
        {
            Console.WriteLine("Все тушенки:");
            ShowStews(_stews);
            Console.WriteLine("просрочки:");
            ShowStews(GetOverdueStews());
        }

        private List<Stew> GetOverdueStews()
        {
            var overdueStews = _stews.Where(stew => stew.BestBeforeDate < _currentDate).Select(stew => stew).ToList();

            return overdueStews;
        }

        private void ShowStews(List<Stew> stews)
        {
            Console.WriteLine();

            for (int i = 0; i < stews.Count; i++)
            {
                stews[i].ShowInfo();
            }

            Console.WriteLine();
        }
    }

    class Stew
    {
        private int _productionDate;
        private string _name;

        public Stew(string name, int productionDate, int bestBeforeDate)
        {
            _name = name;
            _productionDate = productionDate;
            BestBeforeDate = bestBeforeDate;
        }

        public int BestBeforeDate { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Тушенка: {_name}, дата производство: {_productionDate}, Срок годности до: {BestBeforeDate}");
        }
    }
}
