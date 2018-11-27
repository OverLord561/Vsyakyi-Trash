using System;

namespace Bridge
{
    //Призначення.Розділяє абстракцію від її імплементаці, таким чином що і те і інше може мінятися
    //незалежно.
    class Program
    {
        static void Main(string[] args)
        {
            var bricks = new BrickWallCreator();
            var slagBlocks = new SlagBloskCreator();

            var buildingCompany = new BuldingCompany();

            buildingCompany.BuildFoundation();
            buildingCompany.WallCreator = bricks;
            buildingCompany.BuildRoom();

            buildingCompany.WallCreator = slagBlocks;
            buildingCompany.BuildRoom();

            buildingCompany.BuildRoof();

            Console.ReadLine();
        }
    }

    interface IBuldingCompany
    {
        void BuildFoundation();
        void BuildRoom();
        void BuildRoof();
        IWallCreator WallCreator { get; set; }
    }
    class BuldingCompany : IBuldingCompany
    {
        public void BuildFoundation()
        {
            Console.WriteLine("Foundation is built.{0}", Environment.NewLine);
        }
        public void BuildRoom()
        {
            WallCreator.BuildWallWithDoor();
            WallCreator.BuildWall();
            WallCreator.BuildWallWithWindow();
            WallCreator.BuildWall();
            Console.WriteLine("Room finished.{0}", Environment.NewLine);
        }
        public void BuildRoof()
        {
            Console.WriteLine("Roof is done.{0}", Environment.NewLine);
        }
        public IWallCreator WallCreator { get; set; }
    }

    interface IWallCreator {
        void BuildWallWithDoor();
        void BuildWall();
        void BuildWallWithWindow();
    }

    class BrickWallCreator : IWallCreator
    {
        public void BuildWall()
        {
            Console.WriteLine("Цегляна стіна");
        }

        public void BuildWallWithDoor()
        {
            Console.WriteLine("Цегляна стіна з дверима");
        }

        public void BuildWallWithWindow()
        {
            Console.WriteLine("Цегляна стіна з вікном");
        }
    }

    class SlagBloskCreator : IWallCreator
    {
        public void BuildWall()
        {
            Console.WriteLine("Шлакоблокова стіна");
        }

        public void BuildWallWithDoor()
        {
            Console.WriteLine("Шлакоблокова стіна з дверима");
        }

        public void BuildWallWithWindow()
        {
            Console.WriteLine("Шлакоблокова стіна з вікном");
        }
    }
}
