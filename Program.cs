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
        private int _damage;
        private int _dullets;

        public void Fire(Player player)
        {
            player.TakeDamage(_damage);
            _dullets -= 1;
        }
    }

    class Player
    {
        private int _health;

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
