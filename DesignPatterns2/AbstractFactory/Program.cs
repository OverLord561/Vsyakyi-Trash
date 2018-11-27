using System;

namespace AbstractFactory
{
    class Program
    {
        //Призначення: Надати інтерфейс для створення сімейств споріднених або залежних
        //об’єктів без зазначення їхніх конкретних класів.
        static void Main(string[] args)
        {
            // Спочатку створимо «дерев'яну» фабрику
            IToyFactory toyFactory = new WoodenToysFactory();
            Bear bear = toyFactory.GetBear();
            Cat cat = toyFactory.GetCat();
            Console.WriteLine("I've got {0} and {1}", bear.Name, cat.Name);
            // Вивід на консоль буде: [I've got Wooden Bear and Wooden Cat]


            // А тепер створимо «плюшеву» фабрику, наступна лінійка є єдиною різницею в коді
            IToyFactory toyFactory2 = new TeddyToysFactory();
            // Як бачимо код нижче не відрізняється від наведеного вище
            Bear bear2 = toyFactory2.GetBear();
            Cat cat2 = toyFactory2.GetCat();
            Console.WriteLine("I've got {0} and {1}", bear2.Name, cat2.Name);
            // А вивід на консоль буде інший: [I've got Teddy Bear and Teddy Cat]
            Console.ReadLine();
        }



    }

    // абстрактна фабрика (abstract factory)
    public interface IToyFactory
    {
        Bear GetBear();
        Cat GetCat();
    }

    // конкретна фабрика (concrete factory)
    public class TeddyToysFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new TeddyBear();
        }
        public Cat GetCat()
        {
            return new TeddyCat();
        }
    }
    // і ще одна конкретна фабрика
    public class WoodenToysFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new WoodenBear();
        }
        public Cat GetCat()
        {
            return new WoodenCat();
        }
    }

    // Базовий клас для усіх котиків, базовий клас AnimalToy містить Name
    public abstract class Cat : AnimalToy
    {
        protected Cat(string name) : base(name) { }
    }



    public abstract class Bear : AnimalToy
    {
        protected Bear(string name) : base(name) { }
    }
    // Конкретні реалізації
    class WoodenCat : Cat
    {
        public WoodenCat() : base("Wooden Cat") { }
    }
    class TeddyCat : Cat
    {
        public TeddyCat() : base("Teddy Cat") { }
    }
    class WoodenBear : Bear
    {
        public WoodenBear() : base("Wooden Bear") { }
    }
    class TeddyBear : Bear
    {
        public TeddyBear() : base("Teddy Bear") { }
    }

    public class AnimalToy
    {
        public AnimalToy(string name)
        {
            name = name;
        }
        public string Name { get; set; }
    }
}
