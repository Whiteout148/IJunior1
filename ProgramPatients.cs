
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Runtime.InteropServices;

namespace XDproject
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderForm orderForm = new OrderForm();
            PaymentHandler paymentHandler = new PaymentHandler();

            List<ISystemFabric> fabrics = new List<ISystemFabric>();

            fabrics.Add(new CardFabric("MasterCard"));
            fabrics.Add(new QiwiFabric("QIWI"));
            fabrics.Add(new WebMoneyFabric("Web"));

            Steam steam = new Steam(orderForm, paymentHandler, fabrics);

            steam.Work();
        }
    }

    class Steam
    {
        private OrderForm _form;
        private PaymentHandler _handler;
        private List<ISystemFabric> _fabrics;

        public Steam(OrderForm form, PaymentHandler handler, List<ISystemFabric> fabrics)
        {
            if (fabrics == null || fabrics.Count < 1)
                throw new ArgumentNullException(nameof(fabrics));

            _fabrics = fabrics;
            _form = form ?? throw new NullReferenceException();
            _handler = handler ?? throw new NullReferenceException();
        }

        public void Work()
        {
            string userInput = _form.ShowForm(_fabrics.Select(fabric => fabric.Id).ToList());

            if (TryGetFabric(userInput, out PaymentSystem system))
            {
                _handler.ShowPaymentResult(userInput, system);
            }
            else
            {
                Console.WriteLine("Такого способа оплаты нету!");
            }
        }

        private bool TryGetFabric(string id, out PaymentSystem system)
        {
            for (int i = 0; i < _fabrics.Count; i++)
            {
                if (id.ToLower() == _fabrics[i].Id.ToLower())
                {
                    system = _fabrics[i].CreatePaymentSystem(id);

                    return true;
                }
            }

            system = null;

            return false;
        }
    }

    class OrderForm
    {
        public string ShowForm(List<string> paymentsId)
        {
            Console.WriteLine("Мы принимаем:\n");

            for (int i = 0; i < paymentsId.Count; i++)
            {
                Console.Write(paymentsId[i] + ", ");
            }

            Console.WriteLine("Какое системой вы хотите совершить оплату?");

            return Console.ReadLine();
        }
    }

    class PaymentHandler
    {
        public void ShowPaymentResult(string systemId, PaymentSystem system)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentNullException(nameof(systemId));

            if (system == null)
                throw new ArgumentNullException(nameof(system));

            system.Pay();

            Console.WriteLine("Проверка платежа через: " + systemId);
            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    interface ISystemFabric
    {
        string Id { get; }
        PaymentSystem CreatePaymentSystem(string systemId);
    }

    abstract class PaymentSystem
    {
        protected string Id { get; private set; }

        public PaymentSystem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();

            Id = id;
        }

        public abstract void Pay();
    }

    class Card : PaymentSystem
    {
        private const int SymbolsCount = 5;

        public Card(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Оплата через карту: " + Id);
        }
    }

    class CardFabric : ISystemFabric
    {
        public string Id { get; private set; }

        public CardFabric(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public PaymentSystem CreatePaymentSystem(string systemId)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentNullException();

            if (systemId != Id)
                throw new InvalidOperationException();

            return new Card(Id);
        }
    }

    class Qiwi : PaymentSystem
    {
        public Qiwi(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Оплата через страницу: " + Id);
        }
    }

    class QiwiFabric : ISystemFabric
    {
        public string Id { get; private set; }

        public QiwiFabric(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public PaymentSystem CreatePaymentSystem(string systemId)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentNullException();

            if (systemId != Id)
                throw new InvalidOperationException();

            return new Qiwi(Id);
        }
    }

    class WebMoney : PaymentSystem
    {
        public WebMoney(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Оплата через API: " + Id);
        }
    }

    class WebMoneyFabric : ISystemFabric
    {
        public string Id { get; private set; }

        public WebMoneyFabric(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public PaymentSystem CreatePaymentSystem(string systemId)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentNullException();

            if (systemId != Id)
                throw new InvalidOperationException();

            return new WebMoney(Id);
        }
    }
}
