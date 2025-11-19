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

    class WareHouse
    {
        private const int MinDetailsSet = 3;
        private const int MaxDetailsSet = 6;

        private List<Detail> _details = new List<Detail>();

        public WareHouse()
        {
            for (int i = 0; i < UserUtils.GetRandomNumber(MinDetailsSet, MaxDetailsSet); i++)
            {
                _details.AddRange(DetailsManager.GetAllTypeDetails(false));
            }
        }

        public bool IsGetDetail(string type)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public Detail GetDetailsWithType(string type)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Type == type)
                {
                    return _details[i];
                }
            }

            return null;
        }
    }

    class Service
    {
        private const int MinCarsCount = 2;
        private const int MaxCarsCount = 5;
        private const int FineToDenyRepair = 15;
        private const int PriceToRepair = 10;
        private const string StartRepairCommand = "1";
        private const string RepairDetailCommand = "2";
        private const string DenyRepairCommand = "3";

        private Queue<Car> _cars = new Queue<Car>();
        private int _balance;

        public Service()
        {
            _balance = 0;

            for (int i = 0; i < UserUtils.GetRandomNumber(MinCarsCount, MaxCarsCount); i++)
            {
                _cars.Enqueue(new Car());
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
        }

        private void ReplaceDetail()
        {

        }
    }

    class Car
    {
        private List<Detail> _details = new List<Detail>();

        public Car()
        {
            _details.AddRange(DetailsManager.GetAllTypeDetails(true));
        }

        public void AddRepairedDetail(Detail detail)
        {
            int index = DetailsManager.GetIndexWithType(detail.Type ,_details);

            _details[index] = detail;
        }

        public void ShowDetails()
        {
            Console.WriteLine("Детали:\n");

            for (int i = 0; i < _details.Count; i++)
            {
                _details[i].ShowInfo();
            }
        }
    }

    class Detail
    {
        private string _type;
        private int _price;
        private bool _isBroke;

        public Detail(string type, int price, bool isbroke)
        {
            _type = type;
            _price = price;
            _isBroke = isbroke;
        }

        public bool IsBroke => _isBroke;
        public string Type => _type;
        public int Price => _price;

        public void ShowInfo()
        {
            Console.WriteLine($"Тип: {_type} Цена: {_price} Состояние: {GetMessageWithState()}");
        }

        private string GetMessageWithState()
        {
            return (_isBroke) ? "Требуется замена" : "Целое";
        }
    }

    static class DetailsManager
    {
        private static string s_engine = "Двигатель";
        private static string s_battery = "Аккумулятор";
        private static string s_wheels = "Колеса";
        private static string s_lights = "Фары";

        public static List<Detail> GetAllTypeDetails(bool isRandomState)
        {
            List<Detail> details = new List<Detail>();

            if (isRandomState)
            {
                details.Add(new Detail(s_engine, 15, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                details.Add(new Detail(s_battery, 5, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                details.Add(new Detail(s_wheels, 10, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                details.Add(new Detail(s_lights, 10, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
            }
            else
            {
                details.Add(new Detail(s_engine, 15, false));
                details.Add(new Detail(s_battery, 5, false));
                details.Add(new Detail(s_wheels, 10, false));
                details.Add(new Detail(s_lights, 10, false));
            }

            return details;
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

        public static int GetDetailsPrice(List<Detail> details)
        {
            int resultPrice = 0;

            for (int i = 0; i < details.Count; i++)
            {
                resultPrice += details[i].Price;
            }

            return resultPrice;
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max + 1);
        }
    }
}