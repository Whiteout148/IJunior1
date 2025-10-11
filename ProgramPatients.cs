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
            ShowAllFighters();
            Console.WriteLine();

            Fighter firstFighter;
            Fighter secondFighter;

            string firstNumber = GetUserMessage("Введите номер первого бойца:");

            if (TryToGetFighterNumber(firstNumber, out int resultFirstNumber))
            {
                firstFighter = _fighters[resultFirstNumber - 1].GetClone();

                string secondNumber = GetUserMessage("Введите номер второго бойца:");

                if (TryToGetFighterNumber(secondNumber, out int resultSecondNumber))
                {
                    secondFighter = (resultFirstNumber == resultSecondNumber) ? firstFighter.GetClone(): _fighters[resultSecondNumber - 1].GetClone();

                    StartFight(firstFighter, secondFighter);
                }
            }
        }

        private void StartFight(Fighter firstFighter, Fighter secondFighter)
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
        void TakeDamage(int damage);
    }

    abstract class Fighter : IDamageable
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

        public string Name { get { return _name; } }
        public int Health { get { return _health; } }

        public virtual void TakeDamage(int damage)
        {
            _health -= (damage * UserUtils.MaxPercent) / (UserUtils.MaxPercent + _armor);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя бойца: {_name} здоровье: {_health} наносимый урон: {_damage} броня: {_armor}");
            Console.WriteLine("Особенность:");
            ShowAbility();
            Console.WriteLine();
        }

        public void ShowHealth()
        {
            Console.WriteLine($"{_name} Здоровье: {_health}");
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
            Console.WriteLine($"Боец {_name} атаковал обычной атакой.");
        }
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
        }

        public override void Attack(IDamageable target)
        {
            if ((UserUtils.IsGetChance(_chance)))
            {
                target.TakeDamage(_damage);
                target.TakeDamage(_damage);
                ShowAbilityInBattle();
            }
            else
            {
                target.TakeDamage(_damage);
                ShowAttackInfo();
            }
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Есть шанс нанести удвоенный урон.");
            Console.WriteLine($"Шанс {_chance} процентов");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();    
            Console.WriteLine($"Боец {_name} применил двойной удар.");
        }

        public override Fighter GetClone()
        {
            return new Viking(_name, _health, _damage, _armor, _chance);
        }
    }

    class SwordsMan : Fighter
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

        public override void Attack(IDamageable target)
        {
            if (_currentAttacksCount % s_attacksCountToUserAbility == 0)
            {
                target.TakeDamage(_damage);
                target.TakeDamage(_damage);
                ShowAbilityInBattle();
            }
            else
            {
                target.TakeDamage(_damage);
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
            Console.WriteLine($"Боец {_name} применил двойной удар.");
        }

        public override Fighter GetClone()
        {
            return new SwordsMan(_name, _health, _damage, _armor);
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
            target.TakeDamage(_damage);
            ShowAttackInfo();
        }

        private void Heals()
        {
            _health += _healthToAdd;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {_name} применил лечение.");
        }

        public override Fighter GetClone()
        {
            return new Wicked(_name, _health, _damage, _armor, _healthToAdd);
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

        public override void Attack(IDamageable target)
        {
            if (_mana >= s_priceToMana)
            {
                target.TakeDamage(_damageWithFireball);
                ShowAbilityInBattle();
                _mana -= s_priceToMana;
            }
            else
            {
                target.TakeDamage(_damage);
                ShowAttackInfo();
            }

            Console.WriteLine($"Мана: {_mana}");
        }

        public override void ShowAbility()
        {
            Console.WriteLine("Может применять заклинание с огненным шаром пока мана находится в нужном количестве.");
            Console.WriteLine($"Мана: {_mana}");
            Console.WriteLine($"Минимальное количество маны для использование заклинание: {s_priceToMana}");
            Console.WriteLine($"Урон при огненном шаре: {_damageWithFireball}");
        }

        public override void ShowAbilityInBattle()
        {
            base.ShowAbilityInBattle();
            Console.WriteLine($"Боец {_name} применил огненный шар.");
        }

        public override Fighter GetClone()
        {
            return new SpellCaster(_name, _health, _damage, _armor, _mana, _damageWithFireball);
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
            target.TakeDamage(_damage);
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
            Console.WriteLine($"Боец {_name} уклонился.");
        }

        public override Fighter GetClone()
        {
            return new Dexterous(_name, _health, _damage, _armor, _chanceToEvade);
        }
    }

    static class UserUtils
    {
        private static int _maxPercent = 100;
        private static Random s_random = new Random();
        public static int MaxPercent => _maxPercent;

        public static bool IsGetChance(int chance)
        {
            return (GetRandomNumber(0, _maxPercent) <= chance) ? true: false;
        }

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}