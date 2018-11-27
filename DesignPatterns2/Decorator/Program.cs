using System;

namespace Decorator
{
    //Призначення. Приєднує додаткові обов’язки до об’єкта динамічно. Декорування надає гнучку
    //альтернативу наслідуванню в питання розширення функціональності.
    class Program
    {
        static void Main(string[] args)
        {
            var doctorsDream = new AmbulanceCar(new Mercedes());
            doctorsDream.Go();
            Console.ReadLine();
        }
    }

    class Car
    {
        protected String BrandName { get; set; }
        public virtual void Go()
        {
            Console.WriteLine("I'm " + BrandName + " and I'm on my way...");
        }
    }
    // Конкретна реалізація класу Car
    class Mercedes : Car
    {
        public Mercedes()
        {
            BrandName = "Mercedes";
        }
    }

    class DecoratorCar : Car
    {
        protected Car DecoratedCar { get; set; }
        public DecoratorCar(Car decoratedCar)
        {
            DecoratedCar = decoratedCar;
        }
        public override void Go()
        {
            DecoratedCar.Go();
        }
    }    class AmbulanceCar : DecoratorCar
    {
        public AmbulanceCar(Car decoratedCar)
        : base(decoratedCar)
        {
        }
        public override void Go()
        {
            base.Go();
            Console.WriteLine("... beep-beep-beeeeeep ...");
        }
    }


}
