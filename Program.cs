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
        private const int BulletsToFire = 1;

        public int Damage { get; private set; }

        private int _bullets;
         
        public Weapon(int damage, int bullets)
        {
            Damage = damage;
            _bullets = bullets;
        }

        public void Fire(Player player)
        {
            player.TakeDamage(Damage);
            _bullets -= BulletsToFire;
        }
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                if (_health - damage <= 0)
                {
                    _health = 0;
                }
                else
                {
                    _health -= damage;
                }
            }
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }
}
