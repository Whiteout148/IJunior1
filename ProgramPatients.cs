using System;
using System.Collections.Generic;
using System.Threading;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            BattleField battleField = new BattleField();

            battleField.Work();
        }
    }

    class BattleField
    {
        private List<Fighter> _firstPlatoon = new List<Fighter>();
        private List<Fighter> _secondPlatoon = new List<Fighter>();

        public BattleField()
        {
            _firstPlatoon.Add(new Fighter("Леха", 100, 20, 30));
            _firstPlatoon.Add(new Sniper(2, "Максим", 90, 20, 50));
            _firstPlatoon.Add(new GrenadeLauncher(3, "Иван", 100, 10, 40));
            _firstPlatoon.Add(new MachineGunner(5, "Антон", 120, 20, 10));
            _firstPlatoon.Add(new Sniper(3, "Второй Максим", 70, 40, 50));

            _secondPlatoon.Add(new Fighter("Вова", 100, 30, 20));
            _secondPlatoon.Add(new Sniper(3, "Серега", 120, 10, 10));
            _secondPlatoon.Add(new GrenadeLauncher(3, "Андрей", 100, 20, 10));
            _secondPlatoon.Add(new MachineGunner(3, "Михаил", 100, 10, 30));
            _secondPlatoon.Add(new MachineGunner(5, "Второй Серега", 60, 20, 30));
        }

        public void Work()
        {
            ShowPlatoon(_firstPlatoon);
            Console.WriteLine();
            ShowPlatoon(_secondPlatoon);
            Console.WriteLine("Нажмите любую кнопку чтобы начать бой между взводами.");
            Console.ReadKey();
            Console.WriteLine();

            while (GetFightersHealth(_firstPlatoon) > 0 && GetFightersHealth(_secondPlatoon) > 0)
            {
                AttackPlatoon(_firstPlatoon, _secondPlatoon, "Атакует первый взвод.");
                Console.WriteLine();
                AttackPlatoon(_secondPlatoon, _firstPlatoon, "Атакует второй взвод.");

                Console.Clear();
            }

            ShowResults();
        }

        private int GetFightersHealth(List<Fighter> platoon)
        {
            int resultHealth = 0;

            for (int i = 0; i < platoon.Count; i++)
            {
                resultHealth += platoon[i].Health;
            }

            return resultHealth;
        }

        private void ShowResults()
        {
            if (GetFightersHealth(_firstPlatoon) > 0 && GetFightersHealth(_secondPlatoon) <= 0)
            {
                Console.WriteLine("Выиграл первый взвод.");
            }
            else if (GetFightersHealth(_secondPlatoon) > 0 && GetFightersHealth(_firstPlatoon) <= 0)
            {
                Console.WriteLine("Выиграл второй взвод");
            }
            else
            {
                Console.WriteLine("Ничья.");
            }
        }

        private void ShowPlatoon(List<Fighter> platoon)
        {
            for (int i = 0; i < platoon.Count; i++)
            {
                platoon[i].ShowInfo();
            }
        }

        private void AttackPlatoon(List<Fighter> platoon, List<Fighter> platoonToAttack, string message)
        {
            int halfSecondWithMs = 500;

            Console.WriteLine(message);

            for (int i = 0; i < platoon.Count; i++)
            {
                platoon[i].ShowAttackInfo();
                platoon[i].Attack(platoonToAttack);
                Thread.Sleep(halfSecondWithMs);
            }
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    class Fighter : IDamageable
    {
        protected string _name;
        protected int _damage;
        protected int _armor;

        public Fighter(string name, int health, int damage, int armor)
        {
            Health = health;
            _name = name;
            _damage = damage;
            _armor = armor;
        }

        public int Health { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {_name} Здоровье {Health} Урон {_damage} Броня {_armor}");
            Console.Write("Тип: ");
            ShowType();
        }

        public void TakeDamage(int damage)
        {
            Health -= (damage * UserUtils.MaxPercent / (UserUtils.MaxPercent + _armor));
        }

        public void ShowAttackInfo()
        {
            Console.WriteLine($"Боец {_name} атакует.");
        }

        public virtual void Attack(List<Fighter> target)
        {
            target[UserUtils.GetRandomNumber(0, target.Count - 1)].TakeDamage(_damage);
        }

        protected virtual void ShowType()
        {
            Console.WriteLine("Обычный.");
        }
    }

    class Sniper : Fighter
    {
        private int _damageCounter;

        public Sniper(int damageCounter, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _damageCounter = damageCounter;
        }

        public override void Attack(List<Fighter> target)
        {
            int resultDamage = _damage * _damageCounter;

            target[UserUtils.GetRandomNumber(0, target.Count - 1)].TakeDamage(resultDamage);
        }

        protected override void ShowType()
        {
            Console.WriteLine("Снайпер.");
            Console.WriteLine("Множитель урона: " + _damageCounter);
        }
    }

    class GrenadeLauncher : Fighter
    {
        private List<int> _AttackedTargetsIndex = new List<int>();
        private int _targetsCount;

        public GrenadeLauncher(int targetsCount, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targetsCount = targetsCount;
        }

        public override void Attack(List<Fighter> target)
        {
            for (int i = 0; i < _targetsCount; i++)
            {
                int targetIndex = UserUtils.GetRandomNumber(0, target.Count - 1);
                target[targetIndex].TakeDamage(_damage);
                _AttackedTargetsIndex.Add(targetIndex);
            }

            _AttackedTargetsIndex.Clear();
        }

        protected override void ShowType()
        {
            Console.WriteLine("Панзер гренадер.");
            Console.WriteLine("Количество врагов на атаку: " + _targetsCount);
        }
    }

    class MachineGunner : Fighter
    {
        private List<int> _AttackedTargetsIndex = new List<int>();
        private int _targetsCount;

        public MachineGunner(int targetsCount, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targetsCount = targetsCount;
        }

        public override void Attack(List<Fighter> target)
        {
            for (int i = 0; i < _targetsCount; i++)
            {
                target[UserUtils.GetRandomNumber(0, target.Count - 1)].TakeDamage(_damage);
            }

            _AttackedTargetsIndex.Clear();
        }

        protected override void ShowType()
        {
            Console.WriteLine("Пулеметчик.");
            Console.WriteLine("Количество врагов на атаку: " + _targetsCount);
        }
    }

    static class UserUtils
    {
        public const int MaxPercent = 100;
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max + 1);
        }
    }
}