using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Service service = new Service();

            service.Work();
        }
    }

    class Service
    {
        private const int MinCarsCount = 2;
        private const int MaxCarsCount = 5;
        private const int MinDetailsSet = 3;
        private const int MaxDetailsSet = 6;
        private const int FineToDenyRepair = 15;
        private const int PriceToRepair = 10;
        private const string StartRepairCommand = "1";
        private const string RepairDetailCommand = "2";
        private const string DenyRepairCommand = "3";

        private Queue<Car> _cars = new Queue<Car>();
        private List<Detail> _details = new List<Detail>();
        private int _balance;

        public Service()
        {
            _balance = 0;

            for (int i = 0; i < UserUtils.GetRandomNumber(MinCarsCount, MaxCarsCount); i++)
            {
                _cars.Enqueue(new Car());
            }

            for (int i = 0; i < UserUtils.GetRandomNumber(MinDetailsSet, MaxDetailsSet); i++)
            {
                _details.AddRange(UserUtils.GetAllTypeDetails());
            }
        }

        public void Work()
        {
            while (_cars.Count > 0)
            {
                Console.WriteLine("** Автосервис **");
                Console.WriteLine();
                Console.WriteLine("Баланс автосервиса: " + _balance);
                Console.WriteLine();
                Console.WriteLine("Всего машин в очереди: " + _cars.Count);
                Console.WriteLine("Машина в очереди на ремонт:");
                Console.WriteLine();
                _cars.Peek().ShowDetails();
                Console.WriteLine();
                Console.WriteLine($"Команда чтобы начать ремонт: {StartRepairCommand}");
                Console.WriteLine($"Команда для отказа в ремонте: {DenyRepairCommand}");

                switch (Console.ReadLine())
                {
                    case StartRepairCommand:
                        RepairCar();
                        break;

                    case DenyRepairCommand:
                        DenyRepair();
                        break;

                    default:
                        Console.WriteLine("Нету такой команды.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void DenyRepair()
        {
            _balance -= FineToDenyRepair;
            Console.WriteLine("Вы отменили ремонт.");
            _cars.Dequeue();
        }

        private void RepairCar()
        {
            int resultMoney;

            if (IsRepairAllDetails(out resultMoney))
            {
                resultMoney += PriceToRepair;
            }

            _cars.Dequeue();
            _balance += resultMoney;
        }

        private bool IsRepairAllDetails(out int resultMoney)
        {
            resultMoney = 0;
            bool isRepared = true;
            Console.Clear();

            for (int i = 0; i < _cars.Peek().BrokenDetails.Count; i++)
            {
                _cars.Peek().ShowDetails();

                Console.WriteLine("Нажмите любую клавишу чтобы отремонтировать деталь.");

                int detailIndex = UserUtils.GetIndexWithType(_cars.Peek().BrokenDetails[i].Type, _details);

                if (Console.ReadLine() == DenyRepairCommand)
                {
                    int fineToUnRepairedDetails = UserUtils.GetDetailsPrice(_cars.Peek().BrokenDetails);

                    resultMoney -= fineToUnRepairedDetails;
                    Console.WriteLine("Вы отменили во время ремонта, цена за все непочиненные детали:" + fineToUnRepairedDetails);
                }

                if (detailIndex == -1)
                {
                    isRepared = false;
                    Console.WriteLine("У вас нету такой детали.");
                }
                else
                {
                    resultMoney += _details[detailIndex].Price;
                    _cars.Peek().AddDetail(_details[detailIndex]);
                    _details.RemoveAt(detailIndex);
                    _cars.Peek().BrokenDetails.RemoveAt(i);
                }

                Console.ReadKey();
            }

            return isRepared;
        }
    }

    class Car
    {
        private List<Detail> _details = new List<Detail>();
        private List<Detail> _brokenDetails = new List<Detail>();
        public List<Detail> BrokenDetails = new List<Detail>();

        public Car()
        {
            _details = UserUtils.GetAllTypeDetails();
            _brokenDetails = UserUtils.GetBrokenDetails(_details);

            for (int i = 0; i < _brokenDetails.Count; i++)
            {
                BrokenDetails.Add(_brokenDetails[i].GetClone());
            }
        }

        public void AddDetail(Detail detail)
        {
            _details.Add(detail);
        }

        public void ShowDetails()
        {
            Console.WriteLine("Целые детали:");

            for (int i = 0; i < _details.Count; i++)
            {
                _details[i].ShowInfo();
            }

            Console.WriteLine("Сломанные детали:");

            for (int i = 0; i < BrokenDetails.Count; i++)
            {
                BrokenDetails[i].ShowInfo();
            }
        }
    }

    class Detail
    {
        private string _type;
        private int _price;

        public Detail(string type, int price)
        {
            _type = type;
            _price = price;
        }

        public string Type => _type;
        public int Price => _price;

        public Detail GetClone()
        {
            return new Detail(_type, _price);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Тип: {_type} Цена {_price}");
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max + 1);
        }

        public static List<Detail> GetBrokenDetails(List<Detail> details)
        {
            List<Detail> brokenDetails = new List<Detail>();

            for (int i = 0; i < GetRandomNumber(1, details.Count); i++)
            {
                int detailIndex = GetRandomNumber(0, details.Count - 1);

                brokenDetails.Add(details[detailIndex]);
                details.RemoveAt(detailIndex);
            }

            return brokenDetails;
        }

        public static List<Detail> GetAllTypeDetails()
        {
            List<Detail> details = new List<Detail>();

            details.Add(new Detail("Двигатель", 15));
            details.Add(new Detail("Аккумулятор", 5));
            details.Add(new Detail("Колеса", 10));
            details.Add(new Detail("Фары", 10));

            return details;
        }

        public static int GetDetailsPrice(List<Detail> details)
        {
            int resultPrice = 0;

            for (int i = 0; i < details.Count; i++)
            {
                resultPrice += details[i].Price;
            }

            return resultPrice;
        }

        public static int GetIndexWithType(string type, List<Detail> details)
        {
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].Type == type)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}