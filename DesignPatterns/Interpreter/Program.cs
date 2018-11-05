using System;
using System.Collections.Generic;

namespace Interpreter
{
    //    Маючи мову, визначає представлення її граматики та інтерпретарор, що
    //використовує це представлення, щоб інтерпретувати речення цієї мови
    class Program
    {
        static void Main(string[] args)
        {
            // Дістаємо синтаксичне дерево, що представляє речення
            var truckWithGoods = PrepareTruckWithGoods();
            // Отримуємо останній контекст цін
            var pricesContext = GetRecentPricesContext();
            // Інтерпретуємо
            var totalPriceForGoods = truckWithGoods.Interpret(pricesContext);
            Console.WriteLine("Total: {0}", totalPriceForGoods);
            Console.ReadLine();
        }

        private static CurrentPricesContext GetRecentPricesContext()
        {
            var pricesContext = new CurrentPricesContext();
            
            return pricesContext;
        }

        public static GoodsPackage PrepareTruckWithGoods()
        {
            var truck = new GoodsPackage() { GoodsInside = new List<Goods>() };
            var bed = new Bed();
            var doubleTriplePackedBed = new GoodsPackage()
            {
                GoodsInside = new List<Goods>() { new GoodsPackage() { GoodsInside = new List<Goods>() { bed } } }
            };
            truck.GoodsInside.Add(doubleTriplePackedBed);
            truck.GoodsInside.Add(new TV());
            truck.GoodsInside.Add(new TV());
            truck.GoodsInside.Add(new GoodsPackage()
            {
                GoodsInside = new List<Goods>() { new Laptop(), new Laptop(), new Laptop() }
            });
            return truck;
        }
    }

    // Абстрактний вираз
    abstract class Goods
    {
        public abstract int Interpret(CurrentPricesContext context);
    }

    class GoodsPackage : Goods
    {
        public List<Goods> GoodsInside { get; set; }
        public override int Interpret(CurrentPricesContext context)
        {
            var totalSum = 0;
            foreach (var goods in GoodsInside)
            {
                totalSum += goods.Interpret(context);
            }
            return totalSum;
        }


    }
    // Термінальний вираз (зразу повертає значення взявши із його із контексту)
    class TV : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("TV");
            Console.WriteLine("TV: {0}", price);
            return price;
        }
    }
    // Інші термінальні вирази (Laptop, Bed)
    class Laptop : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("Laptop");
            Console.WriteLine("Laptop: {0}", price);
            return price;
        }
    }

    class Bed : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("Bed");
            Console.WriteLine("Bed: {0}", price);
            return price;
        }
    }

    class CurrentPricesContext
    {

        private Dictionary<string, int> Prices { get; set; }

        public CurrentPricesContext()
        {
            Prices = new Dictionary<string, int>();
            InitializePrices();
        }

        private void InitializePrices()
        {
            SetPrice("TV", 1);
            SetPrice("Laptop", 2);
            SetPrice("Bed", 3);
        }

        public int GetPrice(string key)
        {
            int res = 0;

            Prices.TryGetValue(key, out res);

            return res;
        }

        public void SetPrice(string key, int price)
        {
            Prices.Add(key, price);
        }
    }
}
