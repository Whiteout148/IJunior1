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

            IHashService md5 = new MD5Service();
            IHashService sha1 = new SHA1Service();

            List<IPaymentSystem> systems = new List<IPaymentSystem>();

            systems.Add(new NormalSystem(md5));
            systems.Add(new AmountSystem(md5));
            systems.Add(new KeySystem(sha1));

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

        public Order(int id, int amount)
        {
            Id = id;
            Amount = amount;
        }
    }

    interface IPaymentSystem
    {
        string GetPayingLink(Order order);
    }

    interface IHashService
    {
        string GetHash(string startInfo);
    }

    class MD5Service : IHashService
    {
        public string GetHash(string startInfo)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(startInfo);
                byte[] hashBytes = md5.ComputeHash(bytes);

                return BitConverter.ToString(hashBytes).ToLower();
            }
        }
    }

    class SHA1Service : IHashService
    {
        public string GetHash(string startInfo)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(startInfo);
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return BitConverter.ToString(hashBytes).ToLower();
            }
        }
    }

    class NormalSystem : IPaymentSystem
    {
        private const string BaseLink = "pay.system1.ru/order?amount=";

        private readonly IHashService _hashService;

        public NormalSystem(IHashService hashService)
        {
            _hashService = hashService;
        }

        public string GetPayingLink(Order order)
        {
            string id = order.Id.ToString();

            string hash = _hashService.GetHash(id);

            return $"{BaseLink}{order.Amount}RUB&hash={hash}";
        }
    }

    class AmountSystem : IPaymentSystem
    {
        private const string BaseLink = "order.system2.ru/pay?hash=";

        private readonly IHashService _hashService;

        public AmountSystem(IHashService hashService)
        {
            _hashService = hashService;
        }

        public string GetPayingLink(Order order)
        {
            string data = order.Id.ToString() + order.Amount.ToString();

            string hash = _hashService.GetHash(data);

            return $"{BaseLink}{hash}";
        }
    }

    class KeySystem : IPaymentSystem
    {
        private const string BaseLink = "system3.com/pay?amount=";
        private const string SecretKey = "секретный ключ";

        private readonly IHashService _hashService;

        public KeySystem(IHashService hashService)
        {
            _hashService = hashService;
        }

        public string GetPayingLink(Order order)
        {
            string data = order.Amount.ToString() + order.Id.ToString() + SecretKey;

            string hash = _hashService.GetHash(data);

            return $"{BaseLink}{order.Amount}&curency=RUB&hash={hash}";
        }
    }
}