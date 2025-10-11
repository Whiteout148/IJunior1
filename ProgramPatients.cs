using System;
using System.Collections.Generic;
using System.Threading;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Coliseum coliseum = new Coliseum();

            coliseum.Work();
        }
    }

    class Coliseum
    {
        private List<Fighter> _fighters = new List<Fighter>();

        public Coliseum()
        {
            _fighters.Add(new Viking("Викинг", 100, 15, 50, 30));
            _fighters.Add(new SwordsMan("Чел с мечом", 120, 10, 30));
            _fighters.Add(new Wicked("Злой", 150, 5, 50, 30));
            _fighters.Add(new SpellCaster("Ведьмак", 90, 5, 60, 15, 25));
            _fighters.Add(new Dexterous("Ловкий чел", 85, 15, 40, 30));
        }

        public void Work()
        {
            Console.WriteLine("** Колизей **");
            Console.WriteLine("Типы бойцов");
            Console.WriteLine();
            ShowFighters();
            Console.WriteLine();

            Fighter firstFighter = GetFighter("Введите номер первого бойца:");
            Fighter secondFighter = GetFighter("Введите номер второго бойца:");

            if (firstFighter != null && secondFighter != null)
            {
                Fight(firstFighter, secondFighter);
            }
        }

        private Fighter GetFighter(string message)
        {
            Fighter fighter = null;

            while (fighter == null)
            {
                string userNumber = GetUserMessage(message);

                if (TryToGetFighterNumber(userNumber, out int resultNumber))
                {
                    fighter = _fighters[resultNumber - 1].GetClone();
                }
            }

            return fighter;
        }

        private void Fight(Fighter firstFighter, Fighter secondFighter)
        {
            firstFighter.ShowInfo();
            secondFighter.ShowInfo();

            int threeSecondWithMs = 3000;

            Thread.Sleep(threeSecondWithMs);

            while (firstFighter.Health > 0 && secondFighter.Health > 0)
            {
                firstFighter.Attack(secondFighter);
                secondFighter.Attack(firstFighter);
                Console.WriteLine();
                firstFighter.ShowHealth();
                secondFighter.ShowHealth();

                Thread.Sleep(threeSecondWithMs);
                Console.Clear();
            }

            ShowFightResult(firstFighter, secondFighter);
        }

        private void ShowFightResult(Fighter firstFighter, Fighter secondFighter)
        {
            if (firstFighter.Health > secondFighter.Health)
            {
                Console.WriteLine($"Победитель: {firstFighter.Name}");
            }
            else if (secondFighter.Health > firstFighter.Health)
            {
                Console.WriteLine($"Победитель: {secondFighter.Name}");
            }
            else
            {
                Console.WriteLine("Ничья.");
            }
        }

        private bool TryToGetFighterNumber(string userNumber, out int resultNumber)
        {
            if (int.TryParse(userNumber, out resultNumber))
            {
                if (resultNumber > _fighters.Count || resultNumber < 1)
                {
                    Console.WriteLine("Нету бойца с таким номером!");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Введите число.");
            }

            return false;
        }

        private void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].ShowInfo();
            }
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    abstract class Fighter : IDamageable
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
        public int Health {  get; protected set; }

        public virtual void TakeDamage(int damage)
        {
            Health -= (damage * UserUtils.MaxPercent) / (UserUtils.MaxPercent + Armor);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя бойца: {Name} здоровье: {Health} наносимый урон: {Damage} броня: {Armor}");
            Console.WriteLine("Особенность:");
            ShowAbility();
            Console.WriteLine();
        }

        public void ShowHealth()
        {
            Console.WriteLine($"{Name} Здоровье: {Health}");
        }

        public abstract void Attack(IDamageable target);

        public abstract Fighter GetClone();

        public abstract void ShowAbility();

        public virtual void ShowAbilityInBattle()
        {
            Console.WriteLine("Боец применил способность.");
        }

        protected void ShowAttackInfo()
        {
            Console.WriteLine($"Боец {Name} атаковал обычной атакой.");
        }
    }

    class Viking : Fighter
    {
        private int _chanceToDoubleDamage;

        public Viking(string name, int health, int damage, int armor, int chanceToDoubleDamage) : base(name, health, damage, armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
            _chanceToDoubleDamage = chanceToDoubleDamage;
        }

        public override void Attack(IDamageable target)
        {
            if ((UserUtils.IsGetChance(_chanceToDoubleDamage)))
            {
                int doubleDamage = Damage + Damage;
                target.TakeDamage(Damage);
                ShowAbilityInBattle();
            }
            else
            {
                target.TakeDamage(Damage);
                ShowAttackInfo();
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Есть шанс нанести удвоенный урон.");
            Console.WriteLine($"Шанс {_chanceToDoubleDamage} процентов");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();    
            Console.WriteLine($"Боец {Name} применил двойной удар.");
        }

        public override Fighter GetClone()
        {
            return new Viking(Name, Health, Damage, Armor, _chanceToDoubleDamage);
        }
    }

    class SwordsMan : Fighter
    {
        private const int _attacksCountToUserAbility = 3;
        private int _currentAttacksCount;

        public SwordsMan(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;

            _currentAttacksCount = 0;
        }

        public override void Attack(IDamageable target)
        {
            if (_currentAttacksCount % _attacksCountToUserAbility == 0)
            {
                target.TakeDamage(Damage);
                target.TakeDamage(Damage);
                ShowAbilityInBattle();
            }
            else
            {
                target.TakeDamage(Damage);
                ShowAttackInfo();
            }

            Console.WriteLine($"Количество атак: {_currentAttacksCount}");

            _currentAttacksCount++;
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Каждую третью атаку наносит удвоенный урон.");
            Console.WriteLine($"Количество атак: {_currentAttacksCount}");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {Name} применил двойной удар.");
        }

        public override Fighter GetClone()
        {
            return new SwordsMan(Name, Health, Damage, Armor);
        }
    }

    class Wicked : Fighter
    {
        private int _currentRagesCount;
        private int _maxHealth;
        private int _RagesCountToHeals;
        private int _healthToAdd;

        public Wicked(string name, int health, int damage, int armor, int healthToAdd) : base(name, health, damage, armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
            _healthToAdd = healthToAdd;

            _maxHealth = Health;
            _RagesCountToHeals = 5;
            _currentRagesCount = 0;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _currentRagesCount++;
            Console.WriteLine($"Ярость: {_currentRagesCount}");

            if (_currentRagesCount >= _RagesCountToHeals)
            {
                Heals();
                ShowAbilityInBattle();
                _currentRagesCount = 0;
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("После накопление ярости восстанавливает здоровье.");
            Console.WriteLine($"Ярость: {_currentRagesCount} количество ярости для лечение: {_RagesCountToHeals}");
        }

        public override void Attack(IDamageable target)
        {
            target.TakeDamage(Damage);
            ShowAttackInfo();
        }

        private void Heals()
        {
            Health += _healthToAdd;

            if (Health > _maxHealth)
            {
                Health = _maxHealth;
            }
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {Name} применил лечение.");
        }

        public override Fighter GetClone()
        {
            return new Wicked(Name, Health, Damage, Armor, _healthToAdd);
        }
    }

    class SpellCaster : Fighter
    {
        private const int _priceToMana = 5;
        private int _mana;
        private int _damageWithFireball;

        public SpellCaster(string name, int health, int damage, int armor, int mana, int damageWithFireball) : base(name, health, damage, armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
            _mana = mana;
            _damageWithFireball = damageWithFireball;

            if (mana < _priceToMana)
            {
                mana = _priceToMana;
            }
        }

        public override void Attack(IDamageable target)
        {
            if (_mana >= _priceToMana)
            {
                target.TakeDamage(_damageWithFireball);
                ShowAbilityInBattle();
                _mana -= _priceToMana;
            }
            else
            {
                target.TakeDamage(Damage);
                ShowAttackInfo();
            }

            Console.WriteLine($"Мана: {_mana}");
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Может применять заклинание с огненным шаром пока мана находится в нужном количестве.");
            Console.WriteLine($"Мана: {_mana}");
            Console.WriteLine($"Минимальное количество маны для использование заклинание: {_priceToMana}");
            Console.WriteLine($"Урон при огненном шаре: {_damageWithFireball}");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {Name} применил огненный шар.");
        }

        public override Fighter GetClone()
        {
            return new SpellCaster(Name, Health, Damage, Armor, _mana, _damageWithFireball);
        }
    }

    class Dexterous : Fighter
    {
        private int _chanceToEvade;

        public Dexterous(string name, int health, int damage, int armor, int chanceToEvade) : base(name, health, damage, armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
            _chanceToEvade = chanceToEvade;
        }

        public override void TakeDamage(int damage)
        {
            if (UserUtils.IsGetChance(_chanceToEvade))
            {
                ShowAbilityInBattle();
                ShowHealth();
                return;
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        public override void Attack(IDamageable target)
        {
            target.TakeDamage(Damage);
            ShowAttackInfo();
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Возможность уклонится от удара.");
            Console.WriteLine($"Шанс на уклон: {_chanceToEvade} процентов");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {Name} уклонился.");
        }

        public override Fighter GetClone()
        {
            return new Dexterous(Name, Health, Damage, Armor, _chanceToEvade);
        }
    }

    static class UserUtils
    {
        private static int _maxPercent = 100;
        private static Random s_random = new Random();
        public static int MaxPercent => _maxPercent;

        public static bool IsGetChance(int chance)
        {
            return GetRandomNumber(0, _maxPercent) <= chance;
        }

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}