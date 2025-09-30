using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        Dispatcher dispatcher = new Dispatcher(5);

        dispatcher.Work();
    }
}

class Dispatcher
{
    private RailwayStation _station = new RailwayStation();
    private int _trainRoute;

    public Dispatcher(int trainRoute)
    {
        _trainRoute = trainRoute;
    }

    public void Work()
    {
        while (_station.TrainsCount < _trainRoute)
        {
            Console.WriteLine("** Вокзал **");
            Console.WriteLine("Текующие рейсы:");
            _station.ShowTrains();
            string startPoint = GetUserMessage("Введите начальную точку поезда:");
            string endPoint = GetUserMessage("Введите конечную точку поезда:");
            Console.WriteLine("Ожидайте продажи билетов...");
            int waitTime = 3000;
            Thread.Sleep(waitTime);
            int tickets = _station.GetTickets();
            Console.WriteLine($"Билеты проданы, количество билетов: {tickets}");
            string userCarriages = GetUserMessage($"Введите количество вагонов (в одном вагоне {Carriage.MaxTickets} мест): ");
           
            if(TryToAddTrain(userCarriages, tickets, startPoint, endPoint))
            {
                Console.WriteLine("Рейс добавлен.");
            }
            else
            {
                Console.WriteLine("Не удалось добавить поезд.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine("Смена закончилась.");
    }

    private bool TryToAddTrain(string userCarriagesCount, int tickets, string startPoint, string endPoint)
    {
        if (int.TryParse(userCarriagesCount, out int resultCarriagesCount))
        {
            if (tickets <= resultCarriagesCount * Carriage.MaxTickets)
            {
                Train train = new Train(new DirectionMessage(startPoint, endPoint), _station.CreateCarriages(resultCarriagesCount));
                AddTicketsToTrain(tickets, train);
                _station.AddTrain(train);

                return true;
            }
            else
            {
                Console.WriteLine("В поезде не хватает мест для всех билетов!");
            }
        }
        else
        {
            Console.WriteLine("Введите число!");
        }

        return false;
    }

    private void AddTicketsToTrain(int ticketsQuantity, Train train)
    {
        for (int i = 0; i < train.CarriagesCount; i++)
        {
            if (ticketsQuantity >= Carriage.MaxTickets)
            {
                train.AddCarriagesTickets(Carriage.MaxTickets, i);
                ticketsQuantity -= Carriage.MaxTickets;
            }
            else
            {
                train.AddCarriagesTickets(ticketsQuantity, i);
                ticketsQuantity = 0;
            }
        }
    }

    private string GetUserMessage(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}

class RailwayStation
{
    private List<Train> _trains = new List<Train>();

    public int TrainsCount => _trains.Count;

    public int GetTickets()
    {
        Random random = new Random();

        int minTickets = 10;
        int maxTickets = 100;

        return random.Next(minTickets, maxTickets + 1);
    }

    public void ShowTrains()
    {
        for (int i = 0; i < _trains.Count; i++)
        {
            _trains[i].ShowInfo();
        }
    }

    public void AddTrain(Train train)
    {
        _trains.Add(train);
    }

    public List<Carriage> CreateCarriages(int carriagesCount)
    {
        List<Carriage> attachedCarriages = new List<Carriage>();

        for (int i = 0; i < carriagesCount; i++)
        {
            attachedCarriages.Add(new Carriage());
        }

        return attachedCarriages;
    }
}

class Train
{
    private static int _idCounter = 1;
    private List<Carriage> _carriages;
    private DirectionMessage _directionMessage;

    public Train(DirectionMessage directionMessage, List<Carriage> carriages)
    {
        Id = _idCounter++;
        _directionMessage = directionMessage;
        _carriages = carriages;
    }

    public int CarriagesCount => _carriages.Count;
    public int Id { get; private set; }

    public void ShowInfo()
    {
        int remainingTickets = GetCarriagesAllTickets() - GetBusyTickets();

        Console.WriteLine();
        Console.Write("Рейс с сообщением: ");
        _directionMessage.ShowInfo();
        Console.WriteLine($"Номер рейса: {Id}");
        Console.WriteLine($"Количество вагонов: {_carriages.Count}");
        Console.WriteLine($"Оставшийся билеты: {remainingTickets} занятые билеты: {GetBusyTickets()}");
        Console.WriteLine();
    }

    public int GetCarriagesAllTickets()
    {
        return _carriages.Count * Carriage.MaxTickets;
    }

    public void AddCarriagesTickets(int tickets, int index)
    {
        _carriages[index].AddTickets(tickets);
    }

    private int GetBusyTickets()
    {
        int busyTickets = 0;

        for (int i = 0; i < _carriages.Count; i++)
        {
            busyTickets += _carriages[i].BusyTickets;
        }

        return busyTickets;
    }
}

class Carriage
{
    private static int s_maxTickets = 10;
    public static int MaxTickets {  get { return s_maxTickets; } }

    public Carriage()
    {
        BusyTickets = 0;
    }

    public int BusyTickets { get; private set; }

    public void AddTickets(int ticketsQuantity)
    {
        BusyTickets += ticketsQuantity;
    }
}

class DirectionMessage
{
    private string _startPoint;
    private string _endPoint;

    public DirectionMessage(string startPoint, string endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{_startPoint} - {_endPoint}");
    }
}