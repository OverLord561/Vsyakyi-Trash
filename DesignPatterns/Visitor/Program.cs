using System;
using System.Collections.Generic;

namespace Visitor
{
    // Представляє операцію, яка має бути виконана на елементах структури об’єкта.
    //Відвідувач дозволяє визначити нову операцію без зміни класів елементів-операндів.
    class Program
    {
        static void Main(string[] args)
        {
            var floor1 = new Floor(1);
            floor1.AddRoom(new Room(100));
            floor1.AddRoom(new Room(101));
            floor1.AddRoom(new Room(102));
            var floor2 = new Floor(2);
            floor2.AddRoom(new Room(200));
            floor2.AddRoom(new Room(201));
            floor2.AddRoom(new Room(202));
            var myFirmOffice = new OfficeBuilding("[Design Patterns Center]", 990);
            myFirmOffice.AddFloor(floor1);
            myFirmOffice.AddFloor(floor2);

            var electrician = new ElectricitySystemValidator();
            myFirmOffice.Accept(electrician);

            Console.ReadLine();
        }
    }

    interface IVisitor
    {
        void Visit(OfficeBuilding building);
        void Visit(Floor floor);
        void Visit(Room room);
    }
    interface IElement
    {
        void Accept(IVisitor visitor);
    }    class ElectricitySystemValidator : IVisitor
    {
        public void Visit(OfficeBuilding building)
        {
            var electricityState = (building.ElectricitySystemId > 1000)
            ? "Good" : "Bad";
            Console.WriteLine(
            string.Format("Main electric shield in building {0} is in {1} state.",
            building.BuildingName, electricityState));
        }
        public void Visit(Floor floor)
        {
            Console.WriteLine(
            string.Format("Diagnosting electricity on floor {0}.",
            floor.FloorNumber));
        }
        public void Visit(Room room)
        {
            Console.WriteLine(
            string.Format("Diagnosting electricity in room {0}.", room.RoomNumber));
        }
    }    class OfficeBuilding : IElement
    {
        public OfficeBuilding(string name, int electricitySystemId)
        {
            ElectricitySystemId = electricitySystemId;
            BuildingName = name;
            floors = new List<Floor>();
        }
        public int ElectricitySystemId { get; set; }
        public string BuildingName { get; set; }

        public List<Floor> floors { get; private set; }

        public void AddFloor(Floor floor)
        {
            floors.Add(floor);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var floor in floors)
            {
                floor.Accept(visitor);
            }
        }
    }

    class Room : IElement
    {
        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
        public int RoomNumber { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Floor : IElement
    {
        private readonly IList<Room> _rooms = new List<Room>();
        public int FloorNumber { get; private set; }
        public IEnumerable<Room> Rooms { get { return _rooms; } }
        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;

        }
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var room in Rooms)
            {
                room.Accept(visitor);
            }
        }
    }
}
