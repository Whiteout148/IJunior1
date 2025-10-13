using System;
using System.Collections.Generic;
using System.Threading;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            SuperMarket superMarket = new SuperMarket(3);

            superMarket.Work();
        }
    }

    class SuperMarket
    {
        private List<Product> _products = new List<Product>();
        private Queue<Client> _clients = new Queue<Client>();

        private int _balance;

        public SuperMarket(int clientsCount)
        {
            _balance = 0;

            _products.Add(new Product("Яйца", 20));
            _products.Add(new Product("Молоко", 10));
            _products.Add(new Product("Добрый кола", 15));
            _products.Add(new Product("Пиво", 30));
            _products.Add(new Product("Энержека", 15));
            _products.Add(new Product("Сигареты", 40));

            AddClients(clientsCount);
        }

        public void Work()
        {
            while (_clients.Count > 0)
            {
                Console.WriteLine(" 5орка II ");
                Console.WriteLine("Список продуктов: ");
                Console.WriteLine();
                ShowProducts();
                Console.WriteLine();
                Console.WriteLine("Список продуктов клиента:");
                _clients.Peek().ShowInfo();

                if (_clients.Peek().Balance < _clients.Peek().GetProductsPrice())
                {
                    _clients.Peek().ThrowProducts();
                }

                _balance += _clients.Peek().GetProductsPrice();
                _clients.Dequeue().AddProductsToBag();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].ShowInfo();
            }
        }
         
        private void AddClients(int clientsCount)
        {
            for (int i = 0; i < clientsCount; i++)
            {
                _clients.Enqueue(new Client(_products));
            }
        }
    }

    class Client
    {
        private const int MinBalance = 20;
        private const int MaxBalance = 50;
        private const int MaxDesiredProducts = 5;

        private List<Product> _bag = new List<Product>();
        private List<Product> _basket = new List<Product>();

        public Client(List<Product> desiredProducts)
        {
            Balance = UserUtils.GetRandomNumber(MinBalance, MaxBalance);

            AddRandomProducts(desiredProducts);
        }

        public int Balance { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Баланс клиента: {Balance}");
            Console.WriteLine("Желаемые продукты:");

            for (int i = 0; i < _basket.Count; i++)
            {
                _basket[i].ShowInfo();
            }
        }

        public void ThrowProducts()
        {
            while (GetProductsPrice() > Balance)
            {
                Console.WriteLine();
                int randomProductIndex = UserUtils.GetRandomNumber(0, _basket.Count - 1);
                Console.WriteLine($"Был оставлен продукт:");
                _basket[randomProductIndex].ShowInfo();
                _basket.RemoveAt(randomProductIndex);

                int halfSecondWithMs = 500;

                Thread.Sleep(halfSecondWithMs);
            }
        }

        public void AddProductsToBag()
        {
            for (int i = 0; i < _basket.Count; i++)
            {
                _bag.Add(_basket[i]);
            }

            _basket = null;
        }

        public int GetProductsPrice()
        {
            int resultPrice = 0;

            for (int i = 0; i < _basket.Count; i++)
            {
                resultPrice += _basket[i].Price;
            }

            return resultPrice;
        }

        private void AddRandomProducts(List<Product> desiredProducts)
        {
            for (int i = 0; i < MaxDesiredProducts; i++)
            {
                _basket.Add(desiredProducts[UserUtils.GetRandomNumber(0, desiredProducts.Count - 1)].GetClone());
            }
        }
    }
    class Product
    {
        private string _name;

        public Product(string name, int price)
        {
            _name = name;
            Price = price;
        }

        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{_name} стоимость: {Price}");
        }

        public Product GetClone()
        {
            return new Product(_name, Price);
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}