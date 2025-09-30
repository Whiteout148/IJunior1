using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        Dispatcher dispatcher = new Dispatcher();

        dispatcher.Work();
    }
}

class Dispatcher
{
    private RailwayStation _station = new RailwayStation();

    public void Work()
    {
        while (_station.GetTrainsQuantity() < 5)
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
            int resultCarriages;
           
            if(TryToAddTrain(userCarriages, out resultCarriages, tickets, startPoint, endPoint))
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

    private bool TryToAddTrain(string userCarriages, out int resultCarriages, int tickets, string startPoint, string endPoint)
    {
        if (int.TryParse(userCarriages, out resultCarriages))
        {
            if (tickets <= resultCarriages * Carriage.MaxTickets)
            {
                _station.AddTrain(new Message(startPoint, endPoint), resultCarriages);
                _station.AddTicketsToTrain(tickets);

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

    private string GetUserMessage(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}

class RailwayStation
{
    private List<Train> _trains = new List<Train>();

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

    public void AddTicketsToTrain(int ticketsQuantity)
    {
        int trainIndex = _trains.Count - 1;

        for (int i = 0; i < _trains[trainIndex].GetCarriagesQuantity(); i++)
        {
            if (ticketsQuantity >= Carriage.MaxTickets)
            {
                _trains[trainIndex].AddCarriagesTickets(Carriage.MaxTickets, i);
                ticketsQuantity -= Carriage.MaxTickets;
            }
            else
            {
                _trains[trainIndex].AddCarriagesTickets(ticketsQuantity, i);
                ticketsQuantity = 0;
            }
        }
    }

    public int GetTrainsQuantity()
    {
        return _trains.Count;
    }

    public void AddTrain(Message message, int carriagesQuantity)
    {
        _trains.Add(new Train(message, carriagesQuantity));
    }
}

class Train
{
    private static int _idCounter = 1;
    private List<Carriage> _carriages = new List<Carriage>();
    private Message _message;

    public Train(Message message, int carriagesQuantity)
    {
        Id = _idCounter++;
        _message = message;
        AddCarriages(carriagesQuantity);
    }

    public int Id { get; private set; }

    public void ShowInfo()
    {
        int remainingTickets = GetCarriagesAllTickets() - GetBusyTickets();

        Console.WriteLine();
        Console.Write("Рейс с сообщением: ");
        _message.ShowInfo();
        Console.WriteLine($"Номер рейса: {Id}");
        Console.WriteLine($"Количество вагонов: {_carriages.Count}");
        Console.WriteLine($"Оставшийся билеты: {remainingTickets} занятые билеты: {GetBusyTickets()}");
        Console.WriteLine();
    }

    public int GetCarriagesQuantity()
    {
        return _carriages.Count;
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

    private void AddCarriages(int carriagesQuantity)
    {
        for (int i = 0; i < carriagesQuantity; i++)
        {
            _carriages.Add(new Carriage());
        }
    }
}

class Carriage
{
    public Carriage()
    {
        BusyTickets = 0;
    }

    public static int MaxTickets { get; private set; } = 10;
    public int BusyTickets { get; private set; }

    public void AddTickets(int ticketsQuantity)
    {
        BusyTickets += ticketsQuantity;
    }
}

class Message
{
    private string _startPoint;
    private string _endPoint;

    public Message(string startPoint, string endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{_startPoint} - {_endPoint}");
    }
}