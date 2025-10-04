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
            ShowAllFighters();   
        }

        private void ShowAllFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].ShowInfo();
            }
        }
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

        public virtual int Damage => _damage;

        public virtual void TakeDamage(int damage)
        {
            _health -= damage * _armor;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Имя бойца: {_name} здоровье: {_health} наносимый урон: {_damage} броня: {_armor}");
            Console.WriteLine("Особенность:");
            ShowAbility();
            Console.WriteLine();
        }

        public abstract void ShowAbility();
    }

    class Viking : Fighter
    {
        private int _chance;
        private int _doubleDamage;

        public Viking(string name, int health, int damage, int armor, int chance) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _chance = chance;

            _doubleDamage = damage + damage;

            if (chance > UserUtils.MaxChanceWithPercent)
            {
                chance = UserUtils.MaxChanceWithPercent;
            }
        }

        public override int Damage => (UserUtils.GetRandomNumber(0, UserUtils.MaxChanceWithPercent) <= _chance) ? _doubleDamage : _damage;

        public override void ShowAbility()
        {
            Console.WriteLine("Есть шанс нанести удвоенный урон.");
            Console.WriteLine($"Шанс {_chance} процентов");
        }
    }

    class SwordsMan : Fighter
    {
        private static int s_attacksCountToUserAbility = 3;
        private int _doubleDamage;
        private int _currentAttacksCount;

        public SwordsMan(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;

            _doubleDamage = damage + damage;

            _currentAttacksCount = 0;
        }

        public override int Damage => (_currentAttacksCount % s_attacksCountToUserAbility == 0) ? _doubleDamage : _damage;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _currentAttacksCount++;
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Каждую третью атаку наносит удвоенный урон.");
            Console.WriteLine($"Количество атак: {_currentAttacksCount}");
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

            if (_currentRagesCount >= _RagesCountToHeals)
            {
                Heals();
                _currentRagesCount = 0;
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("После накопление ярости восстанавливает здоровье.");
            Console.WriteLine($"Ярость: {_currentRagesCount} количество ярости для лечение: {_RagesCountToHeals}");
        }

        private void Heals()
        {
            _health += _healthToAdd;

            if (_healthToAdd > _health)
            {
                _health = _maxHealth;
            }
        }
    }

    class SpellCaster : Fighter
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

        public override int Damage => (_mana >= s_priceToMana) ? _damageWithFireball : _damage;

        public override void ShowAbility()
        {
            Console.WriteLine("Может применять заклинание с огненным шаром пока мана находится в нужном количестве.");
            Console.WriteLine($"Мана: {_mana}");
            Console.WriteLine($"Минимальное количество маны для использование заклинание: {s_priceToMana}");
            Console.WriteLine($"Урон при огненном шаре: {_damageWithFireball}");
        }
    }

    class Dexterous : Fighter
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
                return;
            else
                base.TakeDamage(damage);
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Возможность уклонится от удара.");
            Console.WriteLine($"Шанс на уклон: {_chanceToEvade} процентов");
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