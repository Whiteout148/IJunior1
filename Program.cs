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
        private Squad _firstSquad;
        private Squad _secondSquad;

        public MilitaryLeadership()
        {
            List<Soldier> soldiers = new List<Soldier>();

            soldiers.Add(new Soldier("Иван"));
            soldiers.Add(new Soldier("Сергей"));
            soldiers.Add(new Soldier("Борис"));
            soldiers.Add(new Soldier("Берат"));

            _firstSquad = new Squad(soldiers);
            _secondSquad = new Squad(new List<Soldier>());
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
            var soldiersToTransfer = _firstSquad.Soldiers.Where(soldier => soldier.Name.ToUpper().StartsWith("Б")).ToList();

            _secondSquad.Soldiers.AddRange(soldiersToTransfer);

            for (int i = 0; i < soldiersToTransfer.Count; i++)
            {
                int index = UserUtils.GetIndex(_firstSquad.Soldiers, soldiersToTransfer[i]);

                _firstSquad.Soldiers.RemoveAt(index);
            }
        }

        private void ShowInfo()
        {
            Console.WriteLine("1 Отряд:");
            _firstSquad.ShowSoldiers();
            Console.WriteLine();
            Console.WriteLine("2 Отряд:");
            _secondSquad.ShowSoldiers();
            Console.WriteLine();
        }
    }

    class Squad
    {
        private List<Soldier> _soldiers = new List<Soldier>();
        private List<Soldier> _clonedSoldiers = new List<Soldier>();
                    
        public Squad(List<Soldier> soldiers)
        {
            _soldiers = soldiers;

            for (int i = 0; i < _soldiers.Count; i++)
            {
                _clonedSoldiers.Add(_soldiers[i].GetClone());
            }
        }

        public List<Soldier> Soldiers => _clonedSoldiers;

        public void ShowSoldiers()
        {
            Console.WriteLine();

            for (int i = 0; i < _clonedSoldiers.Count; i++)
            {
                _clonedSoldiers[i].ShowInfo();
                Console.WriteLine();
            }
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

    static class UserUtils
    {
        public static int GetIndex(List<Soldier> soldiers, Soldier soldier)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                if (soldiers[i] == soldier)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
