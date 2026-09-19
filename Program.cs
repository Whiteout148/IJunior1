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
            Product iPhone12 = new Product("IPhone 12");
            Product iPhone11 = new Product("IPhone 11");

            Warehouse warehouse = new Warehouse();

            int cartsCount = 5;

            Shop shop = new Shop(warehouse, cartsCount);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().PayLink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    class Shop
    {
        private Warehouse _wareHouse;
        private List<Cart> _carts = new List<Cart>();

        public Shop(Warehouse wareHouse, int cartsCount)
        {
            for (int i = 0; i < cartsCount; i++)
            {
                Wallet wallet = new Wallet("рандомная ссылка");
                Cart toAdd = new Cart(wallet);
                toAdd.NeedToAdd += OnNeedToGet;
                _carts.Add(toAdd);
            }

            if (wareHouse == null)
            {
                throw new NullReferenceException();
            }

            _wareHouse = wareHouse;
        }

        public Cart Cart()
        {
            if (_carts.Count < 1)
            {
                throw new NullReferenceException();
            }

            return _carts.First();
        }

        private void OnNeedToGet(Product product, int count, Cart cart)
        {
            if (_wareHouse.TryGet(product, count))
            {
                cart.Add(product, count);
            }
        }
    }

    class Warehouse
    {
        private Dictionary<Product, int> _products = new Dictionary<Product, int>();

        public void Delive(Product product, int count)
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

        public bool TryGet(Product toGet, int count)
        {
            Product productToAdd = null;
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
                throw new IndexOutOfRangeException();
            }

            _products.Remove(toGet);

            return true;
        }
    }

    class Cart
    {
        private Dictionary<Product, int> _products = new Dictionary<Product, int>();
        private Wallet _wallet;

        public event Action<Product, int, Cart> NeedToAdd;     

        public Cart(Wallet wallet)
        {
            if (wallet == null)
            {
                throw new NullReferenceException();
            }

            _wallet = wallet;
        }

        public void Add(Product toAdd, int count)
        {
            NeedToAdd?.Invoke(toAdd, count, this);    
        }

        public void OnAdd(Product product, int count)
        {
            _products.Add(product, count);
        }

        public Wallet Order()
        {
            foreach (var product in _products)
            {
                product.Key.ShowInfo();
                Console.WriteLine(" " + product.Value);
            }

            return _wallet;
        }
    }

    class Wallet
    {
        public string PayLink { get; private set; }

        public Wallet(string payLink)
        {
            if (payLink == null)
            {
                throw new NullReferenceException();
            }

            PayLink = payLink;
        }
    }

    class Product
    {
        private string _name;
        
        public Product(string name)
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
