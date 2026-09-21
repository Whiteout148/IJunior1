using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Хуильник
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.GetOrder().PayLink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    interface IProductGetter
    {
        bool TryGet(Good toGet, int count);
        void RemoveProducts(Dictionary<Good, int> products);
    }

    class Shop
    {
        private Warehouse _wareHouse;

        public Shop(Warehouse wareHouse)
        {
            if (wareHouse == null)
            {
                throw new NullReferenceException();
            }

            _wareHouse = wareHouse;
        }

        public Cart Cart()
        {
            Order order = new Order("рандомная ссылка");
            Cart cart = new Cart(order, _wareHouse);

            return cart;
        }
    }

    class Warehouse : IProductGetter
    {
        private Dictionary<Good, int> _products = new Dictionary<Good, int>();

        public void Delive(Good product, int count)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }

            if (count < 1)
            {
                throw new IndexOutOfRangeException();
            }

            _products.Add(product, count);
        }

        public void RemoveProducts(Dictionary<Good, int> products)
        {
            foreach (var product in products)
            {
                _products.Remove(product.Key);
            }
        }

        public bool TryGet(Good toGet, int count)
        {
            Good productToAdd = null;
            int resultValue = -1;

            foreach (var product in _products)
            {
                if (toGet == product.Key)
                {
                    productToAdd = product.Key;
                    resultValue = product.Value;
                }
            }


            if (productToAdd == null)
            {
                throw new NullReferenceException();
            }

            if (count > resultValue)
            {
                throw new InvalidOperationException();
            }

            return true;
        }
    }

    class Cart
    {
        private Dictionary<Good, int> _products = new Dictionary<Good, int>();
        private Order _order;
        private IProductGetter _productGetter;

        public Cart(Order order, IProductGetter productGetter)
        {
            if (order == null || productGetter == null)
            {
                throw new NullReferenceException();
            }

            _order = order;
            _productGetter = productGetter;
        }

        public void Add(Good toAdd, int count)
        {
            if (_productGetter.TryGet(toAdd, count))
            {
                _products.Add(toAdd, count);
            }
        }

        public Order GetOrder()
        {
            foreach (var product in _products)
            {
                product.Key.ShowInfo();
                Console.WriteLine(" " + product.Value);
            }

            _productGetter.RemoveProducts(_products);

            return _order;
        }
    }

    class Order
    {
        public string PayLink { get; private set; }

        public Order(string payLink)
        {
            if (payLink == null)
            {
                throw new NullReferenceException();
            }

            PayLink = payLink;
        }
    }

    class Good
    {
        private string _name;

        public Good(string name)
        {
            if (name == null)
            {
                throw new NullReferenceException();
            }

            _name = name;
        }

        public void ShowInfo()
        {
            Console.Write(_name);
        }
    }
}
