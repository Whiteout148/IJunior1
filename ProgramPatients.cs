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

            battleField.Fight();
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    class BattleField
    {
        private Platoon _firstPlatoon = new Platoon();
        private Platoon _secondPlatoon = new Platoon();

        public BattleField()
        {
            _firstPlatoon.AddFighter(new Fighter("Х1", 100, 40, 30));
            _firstPlatoon.AddFighter(new Sniper(2, "Х2", 90, 30, 60));
            _firstPlatoon.AddFighter(new GrenadeLauncher(5, "Х3", 110, 40, 30));
            _firstPlatoon.AddFighter(new MachineGunner(6, "Х4", 100, 50, 50));
            _firstPlatoon.AddFighter(new GrenadeLauncher(5, "Х5", 100, 30, 30));

            _secondPlatoon.AddFighter(new Fighter("Д1", 100, 20, 40));
            _secondPlatoon.AddFighter(new Sniper(2, "Д2", 90, 30, 50));
            _secondPlatoon.AddFighter(new GrenadeLauncher(2, "Д3", 120, 20, 50));
            _secondPlatoon.AddFighter(new MachineGunner(2, "Д4", 110, 30, 50));
            _secondPlatoon.AddFighter(new Sniper(2, "Д5", 90, 90, 60));
        }

        public void Fight()
        {
            _firstPlatoon.ShowFighters();
            Console.WriteLine();
            _secondPlatoon.ShowFighters();
            Console.WriteLine("Нажмите любую клавишу чтобы начать бой.");
            Console.ReadKey();

            while (_firstPlatoon.GetLivingFighters() > 0 && _secondPlatoon.GetLivingFighters() > 0)
            {
                _firstPlatoon.Attack(_secondPlatoon.FightersToDamage);
                _secondPlatoon.Attack(_firstPlatoon.FightersToDamage);
            }

            ShowResults();
        }

        private void ShowResults()
        {
            if (_firstPlatoon.GetLivingFighters() > 0 && _secondPlatoon.GetLivingFighters() <= 0)
            {
                Console.WriteLine("Первый взвод победил.");
            }
            else if (_secondPlatoon.GetLivingFighters() > 0 && _firstPlatoon.GetLivingFighters() <= 0)
            {
                Console.WriteLine("Второй взвод победил.");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }
    }

    class Platoon
    {
        private List<Fighter> _fighters = new List<Fighter>();
        public List<IDamageable> FightersToDamage => new List<IDamageable>(_fighters);

        public void Attack(List<IDamageable> targets)
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].Attack(targets);
            }
        }

        public void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].ShowInfo();
                Console.WriteLine();
            }
        }

        public void AddFighter(Fighter fighter)
        {
            _fighters.Add(fighter);
        }

        public int GetLivingFighters()
        {
            int fightersCount = 0;

            for (int i = 0; i < _fighters.Count; i++)
            {
                if (_fighters[i].Health > 0)
                {
                    fightersCount++;
                }
            }

            return fightersCount;
        }
    }

    class Fighter : IDamageable
    {
        protected int Damage;
        protected int Armor;

        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {Name} Здоровье {Health} Урон {Damage} Броня {Armor}");
            Console.WriteLine("Тип:");
            ShowType();
        }

        public virtual void TakeDamage(int damage)
        {
            int resultDamage = damage * UserUtils.MaxPercent / (UserUtils.MaxPercent + Armor);

            Health -= resultDamage;

            if (Health < 0)
                Health = 0;
        }

        public virtual void Attack(List<IDamageable> targets)
        {
            Console.WriteLine($"Боец {Name} атакует.");
            ShowType();

            targets[UserUtils.GetRandomNumber(0, targets.Count)].TakeDamage(Damage);
        }

        public virtual void ShowType()
        {
            Console.WriteLine("Обычный");
        }
    }

    class Sniper : Fighter
    {
        private int _damageFactor;

        public Sniper(int damageFactor, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _damageFactor = damageFactor;
        }

        public override void Attack(List<IDamageable> targets)
        {
            int resultDamage = Damage * _damageFactor;

            targets[UserUtils.GetRandomNumber(0, targets.Count)].TakeDamage(resultDamage);
        }

        public override void ShowType()
        {
            Console.WriteLine($"Снайпер, множитель урона: {_damageFactor}");
        }
    }

    class GrenadeLauncher : Fighter
    {
        private int _targets;

        public GrenadeLauncher(int targets, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targets = targets;
        }

        public override void Attack(List<IDamageable> targets)
        {
            List<IDamageable> untouchedTargets = new List<IDamageable>();

            untouchedTargets.AddRange(targets);

            for (int i = 0; i < _targets; i++)
            {
                int currentTargetIndex = UserUtils.GetRandomNumber(0, untouchedTargets.Count);

                untouchedTargets[currentTargetIndex].TakeDamage(currentTargetIndex);
                untouchedTargets.RemoveAt(currentTargetIndex);
            }
        }

        public override void ShowType()
        {
            Console.WriteLine("Гранатометчик");
            Console.WriteLine($"Количество противников на атаку: {_targets}");
        }
    }

    class MachineGunner : Fighter
    {
        private int _targets;

        public MachineGunner(int targets, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targets = targets;
        }

        public override void ShowType()
        {
            Console.WriteLine("Пулеметчик");
            Console.WriteLine($"Количество противников на атаку: {_targets}");
        }

        public override void Attack(List<IDamageable> targets)
        {
            for (int i = 0; i < _targets; i++)
            {
                targets[UserUtils.GetRandomNumber(0, targets.Count)].TakeDamage(Damage);
            }
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();
        private static int _maxPercent = 100;
        public static int MaxPercent => _maxPercent;

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}