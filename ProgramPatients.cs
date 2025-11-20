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
        private List<Detail> _details;

        public WareHouse(List<Detail> details)
        {
            _details = details;
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

        public Detail GetDetailWithType(string type)
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
        private const int MinDetailSets = 3;
        private const int MaxDetailSets = 6;
        private const int MinCarsCount = 2;
        private const int MaxCarsCount = 5;
        private const int FineToDenyRepair = 15;
        private const int PriceToRepair = 10;
        private const string StartRepairCommand = "1";
        private const string RepairDetailCommand = "2";
        private const string DenyRepairCommand = "3";

        private WareHouse _wareHouse;
        private Queue<Car> _cars = new Queue<Car>();
        private int _balance;

        public Service()
        {
            _balance = 0;

            int randomCarsCount = UserUtils.GetRandomNumber(MinCarsCount, MaxCarsCount);
            int randomDetailSetsCount = UserUtils.GetRandomNumber(MinDetailSets, MaxDetailSets);

            _wareHouse = new WareHouse(DetailsFactory.GetDetailSets(randomDetailSetsCount));

            for (int i = 0; i < randomCarsCount; i++)
            {
                _cars.Enqueue(new Car(DetailsFactory.GetAllTypeDetails(true)));
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

        private void RepairCar()
        {
            int priceToRepair = 0;

            if (IsRepairedAllDetails(out priceToRepair))
            {
                priceToRepair += PriceToRepair;
            }
            else
            {
                Console.WriteLine("Вы завершили ремонт раньше времени.");
            }

            _balance += priceToRepair;
        }

        private void DenyRepair()
        {
            _balance -= FineToDenyRepair;
            Console.WriteLine("Вы отменили ремонт.");
            _cars.Dequeue();
        }

        private bool IsRepairedAllDetails(out int resultPrice)
        {
            Console.Clear();
            Car currentCar = _cars.Dequeue();

            List<Detail> brokenDetails = currentCar.GetBrokenDetails();
            resultPrice = 0;
            bool isFullRepaired = true;

            bool isWork = true;

            while (isWork && brokenDetails.Count > 0)
            {
                currentCar.ShowDetails();

                Console.WriteLine($"Напишите название детали чтобы её заменить, или {DenyRepairCommand} чтобы завершить починку");
                string userInput = Console.ReadLine();
                int detailIndex = DetailsFactory.GetIndexWithType(userInput, brokenDetails);

                if (userInput == DenyRepairCommand)
                {
                    isWork = false;
                }
                else if (detailIndex == -1)
                {
                    Console.WriteLine("Такой сломанной детали нету.");
                }
                else
                {
                    if (IsRepairDetail(brokenDetails, detailIndex, currentCar))
                    {
                        resultPrice += brokenDetails[detailIndex].Price;
                    }
                    else
                    {
                        resultPrice -= brokenDetails[detailIndex].Price;

                        isFullRepaired = false;
                    }

                    brokenDetails.RemoveAt(detailIndex);
                }

                Console.ReadKey();
                Console.Clear();
            }

            return isFullRepaired;
        }

        private bool IsRepairDetail(List<Detail> brokenDetails, int detailIndex, Car car)
        {
            if (_wareHouse.IsGetDetail(brokenDetails[detailIndex].Type))
            {
                car.AddRepairedDetail(_wareHouse.GetDetailWithType(brokenDetails[detailIndex].Type));
                Console.WriteLine("Деталь заменена.");

                return true;
            }
            else
            {
                Console.WriteLine("На складе нету этой детали.");
            }

            return false;
        }
    }

    class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details)
        {
            _details = details;
        }

        public List<Detail> GetBrokenDetails()
        {
            List<Detail> brokenDetails = new List<Detail>();

            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].IsBroke)
                {
                    brokenDetails.Add(_details[i]);
                }
            }

            return brokenDetails;
        }

        public void AddRepairedDetail(Detail repairedDetail)
        {
            int detailIndex = DetailsFactory.GetIndexWithType(repairedDetail.Type, _details);

            _details.RemoveAt(detailIndex);
            _details.Add(repairedDetail);
        }

        public void ShowDetails()
        {
            Console.WriteLine("Детали:\n");

            DetailsFactory.ShowDetails(_details);
        }
    }

    class Detail
    {
        public Detail(string type, int price, bool isbroke)
        {
            Type = type;
            Price = price;
            IsBroke = isbroke;
        }

        public bool IsBroke { get; private set; }
        public string Type { get; private set; }
        public int Price { get; private set; }

        public void ShowInfo()
        {
            string messageWithState = (IsBroke) ? "Требуется замена" : "Исправное";

            Console.WriteLine($"Тип: {Type} Цена: {Price} Состояние: {messageWithState}");
        }
    }

    static class DetailsFactory
    {
        private static string s_engine = "Двигатель";
        private static string s_battery = "Аккумулятор";
        private static string s_wheels = "Колеса";
        private static string s_lights = "Фары";

        public static List<Detail> GetDetailSets(int setsCount)
        {
            List<Detail> details = new List<Detail>();

            for (int i = 0; i < setsCount; i++)
            {
                details.AddRange(GetAllTypeDetails(false));
            }

            return details;
        }

        public static List<Detail> GetAllTypeDetails(bool isRandomState)
        {
            List<Detail> details = new List<Detail>();

            if (isRandomState)
            {
                while (IsGetBrokenDetail(details) == false)
                {
                    details.Clear();

                    details.Add(new Detail(s_engine, 15, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                    details.Add(new Detail(s_battery, 5, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                    details.Add(new Detail(s_wheels, 15, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                    details.Add(new Detail(s_lights, 10, Convert.ToBoolean(UserUtils.GetRandomNumber(0, 1))));
                }
            }
            else
            {
                details.Add(new Detail(s_engine, 15, false));
                details.Add(new Detail(s_battery, 5, false));
                details.Add(new Detail(s_wheels, 15, false));
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

        public static void ShowDetails(List<Detail> details)
        {
            Console.WriteLine();

            for (int i = 0; i < details.Count; i++)
            {
                details[i].ShowInfo();
            }

            Console.WriteLine();
        }

        private static bool IsGetBrokenDetail(List<Detail> details)
        {
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].IsBroke)
                {
                    return true;
                }
            }

            return false;
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