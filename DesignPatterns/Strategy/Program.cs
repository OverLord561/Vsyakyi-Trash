using System;

namespace Strategy
{
    //. Визначає сім’ю алгоритмів, інкапсулює кожен з них і робить їх взаємозамінними.
    //Стратегія дозволяє видозмінювати алгоритм незалежно від клієнтського коду, що використовує стратегію.
    class Program
    {
        static void Main(string[] args)
        {
            var me = new Myself();
            me.ChangeStrategy(new RainWearingStrategy());
            me.GoOutside();

            Console.ReadLine();
        }
    }

    class Myself
    {
        private IWearingStrategy _wearingStrategy = new DefaultWearingStrategy();
        public void ChangeStrategy(IWearingStrategy wearingStrategy)
        {
            _wearingStrategy = wearingStrategy;
        }
        public void GoOutside()
        {
            var clothes = _wearingStrategy.GetClothes();
            var accessories = _wearingStrategy.GetAccessories();
            Console.WriteLine("Today I wore {0} and took {1}", clothes, accessories);
        }
    }

    interface IWearingStrategy
    {
        string GetClothes();
        string GetAccessories();
    }

    class SunshineWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "T-Shirt";
        }
        public string GetAccessories()
        {
            return "sunglasses";
        }
    }    class DefaultWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "Shirt";
        }
        public string GetAccessories()
        {
            return "nothing";
        }
    }    class RainWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "Coat";
        }
        public string GetAccessories()
        {
            return "umbrella";
        }
    }
}
