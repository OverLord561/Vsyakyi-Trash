using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{

    //    Дозволяє об’єкту змінити свою поведінку тоді, коли внутрішній стан змінюється.Буде
    //здаватися, що об’єкт змінив свій клас.
    class Program
    {
        static void Main(string[] args)
        {
   

            Order order = new Order();
            order.WriteCurrentStateName();

            order.AddProduct(products[0]);
            order.WriteCurrentStateName();

            order.AddProduct(products[1]);

            //order.Register();
            //order.WriteCurrentStateName();
            //order.Grant();
            //order.WriteCurrentStateName();
            //order.Ship();
            // order.WriteCurrentStateName();

            Console.ReadLine();
        }

        private static List<Product> products = new List<Product>() {

            new Product {
                Name = "Motoroala",
                Price = 1111
            },
            new Product {
                Name = "Samsung",
                Price = 1488
            }
        };
    }

    class Product
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }

    class Order
    {
        private OrderState _state;
        private List<Product> _products = new List<Product>();

        public Order()
        {
            _state = new NewOrder(this);
        }
        public void SetOrderState(OrderState state)
        {
            _state = state;
        }
        public void WriteCurrentStateName()
        {
            Console.WriteLine("Current Order's state: {0}", _state.GetType().Name);
        }

        public void AddProduct(Product product)
        {
            _state.AddProduct(product);
        }

        public void DoAddProduct(Product product)
        {
            _products.Add(product);
        }

    }

    class OrderState
    {
        public Order _order;
        public OrderState(Order order)
        {
            _order = order;
        }
        public virtual void AddProduct(Product product)
        {
            OperationIsNotAllowed("AddProduct");
        }

        public virtual void Ship()
        {
            Console.WriteLine("Override me");
        }

        public virtual void Cancel()
        {
            Console.WriteLine("Cancel");
        }

        // Наступні методи (Register, Grant, Ship, Invoice, Cancel) виглядають так же
        private void OperationIsNotAllowed(string operationName)
        {
            Console.WriteLine("Operation {0} is not allowed for Order's state {1}",
            operationName, this.GetType().Name);
        }
    }

    class Granted : OrderState
    {
        public Granted(Order order)
        : base(order)
        {
        }
        
        public override void Ship()
        {
            // _order.DoShipping();
            // _order.SetOrderState(new Shipped(_order));
        }
        public override void Cancel()
        {
            //  _order.DoCancel();
            _order.SetOrderState(new Cancelled(_order));
        }
    }

    class Cancelled : OrderState
    {
        public Cancelled(Order order)
        : base(order)
        {
        }

        public void MethodInCancelled()
        {

        }
    }


    class Shipped : OrderState
    {
        public Shipped(Order order)
        : base(order)
        {
        }

        public void MethodInShipped()
        {
        }
    }

    class NewOrder : OrderState
    {
        public NewOrder(Order order)
        : base(order)
        {
        }

        private void NextSate()
        {
            _order.SetOrderState(new Granted(_order));

        }

        public override void AddProduct(Product product)
        {
            _order.DoAddProduct(product);
            Console.WriteLine("Product added");
            NextSate();
        }
    }
}
