using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        CashRegister cashRegister = new CashRegister();

        cashRegister.Work();
    }
}

abstract class Human
{
    protected List<Product> _products = new List<Product>();
    protected int _balance;

    public int GetProductsLenght()
    {
        return _products.Count;
    }

    public void ShowProducts()
    {
        for (int i = 0; i < _products.Count; i++)
        {
            _products[i].ShowInfo();
        }

        Console.WriteLine();
        Console.WriteLine("Баланс: " + _balance);
    }
}

class Salesman : Human
{
    public Salesman()
    {
        _products.Add(new Product("Молоко", 10));
        _products.Add(new Product("Хлеб", 3));
        _products.Add(new Product("Пивко", 20));
        _products.Add(new Product("Добрый кола", 9));
        _products.Add(new Product("Жвачка", 5));
    }

    public bool IsHaveMoneyToBuy(int id, int clientBalance)
    {
        if (clientBalance < _products[GetIndexWithId(id)].Price)
        {
            Console.WriteLine("У вас не хватает денег на покупку этого продукта.");
            return false;
        }
        else
        {
            return true;
        }
    }

    public Product GetProduct(int id)
    {
        Product product = _products[GetIndexWithId(id)];
        _balance += _products[GetIndexWithId(id)].Price;
        _products.RemoveAt(GetIndexWithId(id));
        return product;
    }

    public int GetIndexWithId(int id)
    {
        int index = -1;

        for (int i = 0; i < _products.Count; i++)
        {
            if (id == _products[i].Id)
            {
                index = i;
            }
        }

        return index;
    }
}

class Client : Human
{
    public Client(int minMoney, int maxMoney)
    {
        Random random = new Random();

        _balance = random.Next(minMoney, maxMoney);
    }
 
    public void AddProduct(Product product)
    {
        _products.Add(product);
        _balance -= product.Price;
    }

    public int GetBalance()
    {
        return _balance;
    }
}

class CashRegister
{
    private Salesman _salesman = new Salesman();
    private Client _client;

    public CashRegister()
    {
        int minClientBalance = 5;
        int maxClientBalance = 40;

        _client = new Client(minClientBalance, maxClientBalance);
    }

    public void Work()
    {
        const string BuyProductCommand = "1";
        const string ExitCommand = "2";

        bool isWork = true;

        while (isWork)
        {
            Console.WriteLine("** 5орка **");
            Console.WriteLine("Продукты в магазине:");
            Console.WriteLine();
            _salesman.ShowProducts();
            Console.WriteLine();
            Console.WriteLine("Ваши продукты:");
            Console.WriteLine();
            _client.ShowProducts();
            Console.WriteLine();
            Console.WriteLine($"Команда для покупки: {BuyProductCommand}");
            Console.WriteLine($"Команда для выхода из магазина: {ExitCommand}");
            string userInput = GetUserMessage("Введите команду:");

            switch (userInput)
            {
                case BuyProductCommand:
                    BuyProduct();
                    break;

                case ExitCommand:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Неверная команда!");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    public void BuyProduct()
    {
        string userId = GetUserMessage("Введите номер продукта:");

        if (int.TryParse(userId, out int resultId))
        {
            if (IsGetProductId(resultId))
            {
                if (_salesman.IsHaveMoneyToBuy(resultId, _client.GetBalance()))
                {
                    Product productToBuy = _salesman.GetProduct(resultId);
                    _client.AddProduct(productToBuy);
                    Console.WriteLine("Продукт куплен.");
                }
            }
        }
        else
        {
            Console.WriteLine("Введите число!");
        }
    }

    private bool IsGetProductId(int userId)
    {
        if (_salesman.GetIndexWithId(userId) == -1)
        {
            Console.WriteLine("Неверный номер продукта!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private string GetUserMessage(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}

class Product
{
    private static int _idCounter = 1;

    public Product(string name, int price)
    {
        Id = _idCounter++;
        Name = name;
        Price = price;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine($"{Name} Цена: {Price} Номер продукта: {Id}");
    }
}
