using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Хуильник
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Weapon
    {
        private const int MinBullets = 3;
        private const int MinDamage = 5;
        private const int BulletsToFire = 1;
        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            Damage = (damage < MinDamage) ? MinDamage : damage;
            _bullets = (bullets < MinBullets) ? BulletsToFire : bullets;
        }

        public int Damage { get; private set; }

        public void Fire(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException();
            }

            if (_bullets < BulletsToFire)
            {
                throw new InvalidOperationException();
            }

            player.TakeDamage(Damage);
            _bullets -= BulletsToFire;
        }
    }

    class Player
    {
        private const int MinHealth = 1;
        private int _health;

        public Player(int health)
        {
            _health = (health < MinHealth) ? MinHealth : health;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new InvalidOperationException();
            }

            int currentDamage = Math.Max(0, _health - damage);

            _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException();
            }

            _weapon.Fire(player);
        }
    }
}
