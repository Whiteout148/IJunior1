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

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.GetCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.GetOrder().PayLink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    interface IProductGetter
    {
        bool IsHaveProduct(Product toGet, int count);
        void RemoveProducts(Dictionary<Product, int> products);
    }

    class Shop
    {
        private Warehouse _wareHouse;

        public Shop(Warehouse wareHouse)
        {
            _wareHouse = wareHouse ?? throw new ArgumentNullException(nameof(wareHouse));
        }

        public Cart GetCart()
        {  
            return new Cart(_wareHouse); ;
        }
    }

    class Warehouse : IProductGetter
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
                throw new ArgumentOutOfRangeException();
            }

            _products.Add(product, count);
        }

        public void RemoveProducts(Dictionary<Product, int> products)
        {
            foreach (var product in products)
            {
                if (_products.ContainsKey(product.Key))
                {
                    if (product.Value < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    if (_products[product.Key] - product.Value < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    if (_products[product.Key] - product.Value == 0)
                    {
                        _products.Remove(product.Key);
                    }
                    else
                    {
                        _products[product.Key] -= product.Value;
                    }
                }
            }
        }

        public bool IsHaveProduct(Product product, int count)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_products.ContainsKey(product))
            {
                if (_products[product] >= count)
                {
                    return true;
                }              
            }

            return false;
        }
    }

    class Cart 
    {
        private const int MaxProducts = 5;
        private const int MaxProductsType = 10;

        private Dictionary<Product, int> _products = new Dictionary<Product, int>();
        private Order _order;
        private IProductGetter _productGetter;

        public Cart(IProductGetter productGetter)
        {
            _productGetter = productGetter ?? throw new ArgumentNullException(nameof(productGetter));
        }

        public void Add(Product productToAdd, int count)
        {
            if (productToAdd == null)
            {
                throw new NullReferenceException();
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_productGetter.IsHaveProduct(productToAdd, count) && _products.Count < MaxProductsType)
            {
                if (count < MaxProducts)
                {
                    _products.Add(productToAdd, count);
                }
            }
        }

        public Order GetOrder()
        {
            Order order = new Order("Рандомная ссылка");

            foreach (var product in _products)
            {
                product.Key.ShowInfo();
                Console.WriteLine(" " + product.Value);
            }

            _productGetter.RemoveProducts(_products);

            return order;
        }
    }

    class Order
    {
        public string PayLink { get; private set; }

        public Order(string payLink)
        {
            if (string.IsNullOrWhiteSpace(payLink))
            {
                throw new ArgumentException();
            }

            PayLink = payLink;
        }
    }

    class Product
    {
        private string _name;

        public Product(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            _name = name;
        }

        public void ShowInfo()
        {
            Console.Write(_name);
        }
    }
}
