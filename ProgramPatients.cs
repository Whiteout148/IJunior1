using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace XDproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order(42, 3);

            List<IPaymentSystem> systems = new List<IPaymentSystem>();

            systems.Add(new NormalSystem());
            systems.Add(new AmountSystem());
            systems.Add(new KeySystem());

            for (int i = 0; i < systems.Count; i++)
            {
                Console.WriteLine(systems[i].GetPayingLink(order));
            }
        }
    }

    class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    interface IPaymentSystem
    {
        string GetPayingLink(Order order);
    }

    class NormalSystem : IPaymentSystem
    {
        private const string BaseLink = "pay.system1.ru/order?amount=";

        public string GetPayingLink(Order order)
        {
            string id = order.Id.ToString();

            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(id);
                byte[] hashBytes = md5.ComputeHash(bytes);

                string hash = BitConverter.ToString(hashBytes).ToLower();

                return $"{BaseLink}{order.Amount}RUB&hash={hash}";
            }
        }
    }

    class AmountSystem : IPaymentSystem
    {
        private const string BaseLink = "order.system2.ru/pay?hash=";

        public string GetPayingLink(Order order)
        {
            string data = order.Id.ToString() + order.Amount.ToString();

            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = md5.ComputeHash(bytes);

                string hash = BitConverter.ToString(hashBytes).ToLower();

                return $"{BaseLink}{hash}";
            }
        }
    }

    class KeySystem : IPaymentSystem
    {
        private const string BaseLink = "system3.com/pay?amount=";
        private readonly string _secretKey = "секретный ключ";

        public string GetPayingLink(Order order)
        {
            string data = order.Amount.ToString()
                         + order.Id.ToString()
                         + _secretKey;

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = sha1.ComputeHash(bytes);

                string hash = BitConverter.ToString(hashBytes).ToLower();

                return $"{BaseLink}{order.Amount}&curency=RUB&hash={hash}";
            }
        }
    }
}