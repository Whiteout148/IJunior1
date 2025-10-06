using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Remoting.Messaging;
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
            _fighters.Add(new SwordsMan("Чел с мечом", 120, 10, 40));
            _fighters.Add(new Wicked("Злой", 150, 5, 50, 30));
            _fighters.Add(new SpellCaster("Ведьмак", 90, 5, 60, 25, 25));
            _fighters.Add(new Dexterous("Ловкий чел", 85, 15, 40, 25));
        }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("** Колизей **");
                Console.WriteLine("Типы бойцов");
                Console.WriteLine();
                ShowAllFighters();
                Console.WriteLine();

                Fighter firstFighter;
                Fighter secondFighter;

                string firstNumber = GetUserMessage("Введите номер первого бойца:");

                if (TryToGetFighterNumber(firstNumber, out int resultFirstNumber))
                {
                    firstFighter = _fighters[resultFirstNumber - 1].GetInfo();

                    string secondNumber = GetUserMessage("Введите номер второго бойца:");

                    if (TryToGetFighterNumber(secondNumber, out int resultSecondNumber))
                    {
                        secondFighter = _fighters[resultSecondNumber - 1].GetInfo();
                    }
                }
            }
        }

        private bool TryToGetFighterNumber(string userNumber, out int resultNumber)
        {
            if (int.TryParse(userNumber, out resultNumber))
            {
                if (resultNumber > _fighters.Count || resultNumber < 0)
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

        private void ShowAllFighters()
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
        void Attack(Fighter fighter);
        void ShowAbilityInBattle();
    }

    abstract class Fighter
    {
        protected string _name;
        protected int _damage;
        protected int _armor;
        protected int _health;

        public Fighter(string name, int health, int damage, int armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
        }

        public int Health { get { return _health; } }

        public virtual void TakeDamage(int damage)
        {
            _health *= _armor -= damage;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя бойца: {_name} здоровье: {_health} наносимый урон: {_damage} броня: {_armor}");
            Console.WriteLine("Особенность:");
            ShowAbility();
            Console.WriteLine();
        }

        protected void ShowAttackInfo()
        {
            Console.WriteLine("Боец атаковал обычной атакой.");
        }

        public abstract Fighter GetInfo();

        public abstract void ShowAbility();
    }

    class Viking : Fighter
    {
        private int _chance;

        public Viking(string name, int health, int damage, int armor, int chance) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _chance = chance;

            if (chance > UserUtils.MaxChanceWithPercent)
            {
                chance = UserUtils.MaxChanceWithPercent;
            }
        }

        public void Attack(Fighter fighter)
        {
            int doubleDamage = _damage + _damage;

            if ((UserUtils.GetRandomNumber(0, UserUtils.MaxChanceWithPercent) <= _chance))
            {
                fighter.TakeDamage(doubleDamage);
                ShowAbilityInBattle();
            }
            else
            {
                fighter.TakeDamage(_damage);
                ShowAttackInfo();
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Есть шанс нанести удвоенный урон.");
            Console.WriteLine($"Шанс {_chance} процентов");
        }

        public void ShowAbilityInBattle()
        {
            Console.WriteLine($"Боец {_name} применил двойной удар.");
        }

        public override Fighter GetInfo()
        {
            return new Viking(_name, _health, _damage, _armor, _chance);
        }
    }

    class SwordsMan : Fighter, IDamageable
    {
        private static int s_attacksCountToUserAbility = 3;
        private int _currentAttacksCount;

        public SwordsMan(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;

            _currentAttacksCount = 0;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public void Attack(Fighter fighter)
        {
            int doubleDamage = _damage + _damage;

            if (_currentAttacksCount % s_attacksCountToUserAbility == 0)
            {
                fighter.TakeDamage(doubleDamage);
                ShowAbilityInBattle();
            }
            else
            {
                fighter.TakeDamage(_damage);
                ShowAttackInfo();
            }

            _currentAttacksCount++;
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Каждую третью атаку наносит удвоенный урон.");
            Console.WriteLine($"Количество атак: {_currentAttacksCount}");
        }

        public void ShowAbilityInBattle()
        {
            Console.WriteLine($"Боец {_name} применил двойной удар.");
            Console.WriteLine($"Количество атак: {_currentAttacksCount}");
        }

        public override Fighter GetInfo()
        {
            return new SwordsMan(_name, _health, _damage, _armor);
        }
    }

    class Wicked : Fighter, IDamageable
    {
        private int _currentRagesCount;
        private int _maxHealth;
        private int _RagesCountToHeals;
        private int _healthToAdd;

        public Wicked(string name, int health, int damage, int armor, int healthToAdd) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _healthToAdd = healthToAdd;

            _maxHealth = _health;
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

        public void Attack(Fighter fighter)
        {
            fighter.TakeDamage(_damage);
            ShowAttackInfo();
        }

        private void Heals()
        {
            _health += _healthToAdd;

            if (_healthToAdd > _health)
            {
                _health = _maxHealth;
            }
        }

        public void ShowAbilityInBattle()
        {
            Console.WriteLine($"Боец {_name} применил лечение.");
        }

        public override Fighter GetInfo()
        {
            return new Wicked(_name, _health, _damage, _armor, _healthToAdd);
        }
    }

    class SpellCaster : Fighter, IDamageable
    {
        private static int s_priceToMana = 5;
        private int _mana;
        private int _damageWithFireball;

        public SpellCaster(string name, int health, int damage, int armor, int mana, int damageWithFireball) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _mana = mana;
            _damageWithFireball = damageWithFireball;

            if (mana < s_priceToMana)
            {
                mana = s_priceToMana;
            }
        }

        public void Attack(Fighter fighter)
        {
            if (_mana >= s_priceToMana)
            {
                fighter.TakeDamage(_damageWithFireball);
                ShowAbilityInBattle();
            }
            else
            {
                fighter.TakeDamage(_damage);
                ShowAttackInfo();
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Может применять заклинание с огненным шаром пока мана находится в нужном количестве.");
            Console.WriteLine($"Мана: {_mana}");
            Console.WriteLine($"Минимальное количество маны для использование заклинание: {s_priceToMana}");
            Console.WriteLine($"Урон при огненном шаре: {_damageWithFireball}");
        }

        public void ShowAbilityInBattle()
        {
            Console.WriteLine($"Боец {_name} применил огненный шар.");
            Console.WriteLine($"Мана: {_mana}");
        }

        public override Fighter GetInfo()
        {
            return new SpellCaster(_name, _health, _damage, _armor, _mana, _damageWithFireball);
        }
    }

    class Dexterous : Fighter, IDamageable
    {
        private int _chanceToEvade;

        public Dexterous(string name, int health, int damage, int armor, int chanceToEvade) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _chanceToEvade = chanceToEvade;

            if (chanceToEvade > UserUtils.MaxChanceWithPercent)
            {
                chanceToEvade = UserUtils.MaxChanceWithPercent;
            }
        }

        public override void TakeDamage(int damage)
        {
            if (UserUtils.GetRandomNumber(0, UserUtils.MaxChanceWithPercent) <= _chanceToEvade)
            {
                ShowAbilityInBattle();
                return;
            }
            else
                base.TakeDamage(damage);
        }

        public void Attack(Fighter fighter)
        {
            fighter.TakeDamage(_damage);
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Возможность уклонится от удара.");
            Console.WriteLine($"Шанс на уклон: {_chanceToEvade} процентов");
        }

        public void ShowAbilityInBattle()
        {
            Console.WriteLine($"Боец {_name} уклонился.");
        }

        public override Fighter GetInfo()
        {
            return new Dexterous(_name, _health, _damage, _armor, _chanceToEvade);
        }
    }

    class UserUtils
    {
        private static int _maxChance = 100;
        private static Random s_random = new Random();
        public static int MaxChanceWithPercent => _maxChance;

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}