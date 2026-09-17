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

        public int Damage { get; private set; }

        public Weapon(int damage, int bullets)
        {
            Damage = (damage < MinDamage) ? MinDamage : damage;
            _bullets = (bullets < MinBullets) ? BulletsToFire : bullets;
        }

        public void Fire(Player player)
        {
            if (player != null)
            {
                player.TakeDamage(Damage);
                _bullets -= BulletsToFire;
            }
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
            int currentDamage = Math.Max(0, _health - damage);

            _health -= currentDamage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            if (player != null)
            {
                _weapon.Fire(player);
            }
        }
    }
}
