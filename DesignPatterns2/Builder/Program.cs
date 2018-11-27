using System;

namespace Builder
{
    //Призначення.Розділити створення складного об’єкта від його представлення, щоб той же процес
    //створення міг утворити різні представлення
    class Program
    {
        static void Main(string[] args)
        {
            // Ваша система може мати багато конкретних будівельників
            var gamingBuilder = new GamingLaptopBuilder();
            var shopForYou = new BuyLaptop();//Директор
                                             // Покупець каже, що хоче грати ігри
            shopForYou.SetLaptopBuilder(gamingBuilder);
            shopForYou.ConstructLaptop();
            // Ну то нехай бере що хоче!
            Laptop laptop = shopForYou.GetLaptop();
            Console.WriteLine(laptop.ToString());
            // Вивід: [Laptop: 1900X1200, Core 2 Duo, 3.2 GHz, 6144 Mb, 500 Gb, 6 lbs]
            Console.ReadLine();
        }
    }

    abstract class LaptopBuilder
    {
        protected Laptop Laptop { get; private set; }
        public void CreateNewLaptop()
        {
            Laptop = new Laptop();
        }
        // Метод, який повертає готовий ноутбук назовні
        public Laptop GetMyLaptop()
        {
            return Laptop;
        }
        // Кроки, необхідні щоб створити ноутбук
        public abstract void SetMonitorResolution();
        public abstract void SetProcessor();
        public abstract void SetMemory();
        public abstract void SetHDD();
        public abstract void SetBattery();
    }

    // Таким будівельником може бути працівник, що
    // спеціалізується у складанні «геймерських» ноутів
    class GamingLaptopBuilder : LaptopBuilder
    {
        public override void SetMonitorResolution()
        {
            Laptop.MonitorResolution = "1900X1200";
        }
        public override void SetProcessor()
        {
            Laptop.Processor = "Core 2 Duo, 3.2 GHz";
        }
        public override void SetMemory()
        {
            Laptop.Memory = "6144 Mb";
        }
        public override void SetHDD()
        {
            Laptop.HDD = "500 Gb";
        }
        public override void SetBattery()
        {
            Laptop.Battery = "6 lbs";
        }
    }

    class BuyLaptop
    {
        private LaptopBuilder _laptopBuilder;
        public void SetLaptopBuilder(LaptopBuilder lBuilder)
        {
            _laptopBuilder = lBuilder;
        }

        // Змушує будівельника повернути цілий ноутбук
        public Laptop GetLaptop()
        {
            return _laptopBuilder.GetMyLaptop();
        }
        // Змушує будівельника додавати деталі
        public void ConstructLaptop()
        {
            _laptopBuilder.CreateNewLaptop();
            _laptopBuilder.SetMonitorResolution();
            _laptopBuilder.SetProcessor();
            _laptopBuilder.SetMemory();
            _laptopBuilder.SetHDD();
            _laptopBuilder.SetBattery();
        }
    }

    class Laptop {
        public string MonitorResolution { get; set; }
        public string Memory { get; set; }
        public string Processor { get; set; }
        public string HDD { get; set; }
        public string Battery { get; set; }

    }

}
