using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainOfResponsibility
{
    //Уникає зв’язності відправника запиту із його адресатом, шляхом надання іншим об’єктам можливість обробити запит.Передає отримані об’єкти вздовж ланцюжка допоки якась
    //ланка не обробить об’єкт.
    class Program
    {
        static void Main(string[] args)
        {
            var cappuccino1 = new Food("Cappuccino", new List<string> {"Coffee", "Milk",
                                        "Sugar"});
            var cappuccino2 = new Food("Cappuccino", new List<string> { "Coffee", "Milk" });

            var late = new Food("Cappuccino", new List<string> { "Coffee", "Milk" });


            var soup1 = new Food("Soup with meat", new List<string> {"Meat", "Water", "Potato"});
            var soup2 = new Food("Soup with potato", new List<string> { "Water", "Potato" });
            var meat = new Food("Meat", new List<string> { "Meat" });

            var girlFriend = new GirlFriend(null);
            var me = new Me(girlFriend);
            var bestFriend = new BestFriend(me);

            bestFriend.HandleFood(cappuccino1);
            bestFriend.HandleFood(cappuccino2);
            bestFriend.HandleFood(soup1);
            bestFriend.HandleFood(soup2);
            bestFriend.HandleFood(meat);
            bestFriend.HandleFood(late);


            Console.ReadLine();
        }
    }

    abstract class WierdCafeVisitor
    {
        public WierdCafeVisitor CafeVisitor { get; private set; }
        protected WierdCafeVisitor(WierdCafeVisitor cafeVisitor)
        {
            CafeVisitor = cafeVisitor;
        }
        public virtual void HandleFood(Food food)
        {
            // Якщо не в змозі подужати їжу, передаємо її ближчому другові
            if (CafeVisitor != null)
            {
                CafeVisitor.HandleFood(food);
            }
        }
    }

    class BestFriend : WierdCafeVisitor
    {
        public List<Food> CoffeeContainingFood { get; private set; }
        public BestFriend(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        {
            CoffeeContainingFood = new List<Food>();
        }
        public override void HandleFood(Food food)
        {
            if (food.Ingradients.Contains("Meat"))
            {
                Console.WriteLine(
                "BestFriend: I just ate {0}. It was tasty.",
                food.Name);
                return;
            }
            if (food.Ingradients.Contains("Coffee") && CoffeeContainingFood.Count < 1)
            {
                CoffeeContainingFood.Add(food);
                Console.WriteLine(
                "BestFriend: I have to take something with coffee. {0} looks fine.",
                food.Name);
                return;
            }
            base.HandleFood(food);
        }
    }

    class Me : WierdCafeVisitor
    {
        public List<Food> CoffeeContainingFood { get; private set; }
        public Me(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        {
            CoffeeContainingFood = new List<Food>();
        }
        public override void HandleFood(Food food)
        {
            if (food.Ingradients.Contains("Coffee") && !CoffeeContainingFood.Any())
            {
                CoffeeContainingFood.Add(food);
                Console.WriteLine(
                "Me: I have to take something with coffee. {0} looks fine.",
                food.Name);
                return;
            }
            base.HandleFood(food);
        }
    }
    public class Food
    {
        public Food(string name, List<string> ingradients)
        {
            Name = name;
            Ingradients = ingradients;
        }
        public string Name { get; set; }
        public List<string> Ingradients { get; set; }
    }

    class GirlFriend : WierdCafeVisitor
    {
        public GirlFriend(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        {
        }
        public override void HandleFood(Food food)
        {
            if (food.Name == "Cappuccino")
            {
                Console.WriteLine("GirlFriend: My lovely cappuccino!!!");
                return;
            }
            // Базовий виклик base.HandleFood(food) для останнього обробітника-дівчини
            // не має сенсу, тому можна викинути ексепшин або нічого не робити
        }
    }
}

