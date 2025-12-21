using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Ujin
{
    internal class Program
    {
        static void Main()
        {
            MilitaryLeadership militaryLeadership = new MilitaryLeadership();

            militaryLeadership.Work();
        }
    }

    class MilitaryLeadership
    {
        private const string FirstLetterInName = "Б";

        private List<Soldier> _firstSquad = new List<Soldier>();
        private List<Soldier> _secondSquad = new List<Soldier>();

        public MilitaryLeadership()
        {
            _firstSquad.Add(new Soldier("Иван"));
            _firstSquad.Add(new Soldier("Сергей"));
            _firstSquad.Add(new Soldier("Борис"));
            _firstSquad.Add(new Soldier("Берат"));
        }

        public void Work()
        {
            Console.WriteLine("Солдаты до перевода:");
            ShowInfo();
            Console.WriteLine("Солдаты после перевода:");
            TransferFighters();
            ShowInfo();
        }

        private void TransferFighters()
        {
            var soldiersToTransfer = _firstSquad.Where(soldier => soldier.Name.ToUpper().StartsWith(FirstLetterInName)).ToList();
            _secondSquad.AddRange(soldiersToTransfer);

            for (int i = 0; i < soldiersToTransfer.Count; i++)
            {
                int index = GetIndexWithSoldier(_firstSquad, soldiersToTransfer[i]);

                _firstSquad.RemoveAt(index);
            }
        }

        private void ShowInfo()
        {
            Console.WriteLine("1 Отряд:");
            ShowSquad(_firstSquad);
            Console.WriteLine();
            Console.WriteLine("2 Отряд:");
            ShowSquad(_secondSquad);
            Console.WriteLine();
        }

        private void ShowSquad(List<Soldier> squad)
        {
            Console.WriteLine();

            for (int i = 0; i < squad.Count; i++)
            {
                squad[i].ShowInfo();
                Console.WriteLine();
            }
        }

        private int GetIndexWithSoldier(List<Soldier> squad, Soldier soldier)
        {
            for (int i = 0; i < squad.Count; i++)
            {
                if (squad[i] == soldier)
                {
                    return i;
                }
            }

            return -1;
        }
    }

    class Soldier
    {
        public Soldier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine("Имя:" + Name);
        }

        public Soldier GetClone()
        {
            return new Soldier(Name);
        }
    }
}
