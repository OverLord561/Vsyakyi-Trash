using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Flyweight
{
    //Ефективна підтримка великої кількості об’єктів шляхом виділення спільної інформації.
    class Program
    {
        static void Main(string[] args)
        {
            var res = ParseHTML();
            Console.ReadLine();
        }

        public static List<Unit> ParseHTML()
        {
            var units = new List<Unit>();
            for (int i = 0; i < 150; i++)
                units.Add(new Dragon());
            for (int i = 0; i < 500; i++)
                units.Add(new Goblin());
            Console.WriteLine("Dragons and Goblins are parsed from HTML page.");
            return units;
        }
    }

    abstract class Unit
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public Image Picture { get; protected set; }
    }

    class Goblin : Unit
    {
        public Goblin()
        {
            Name = "Goblin";
            Health = 8;
            // Picture = Image.FromFile("Goblin.jpg");
            Picture = UnitImagesFactory.CrateGoblinImage();

        }
    }    class Dragon : Unit
    {
        public Dragon()
        {
            Name = "Dragon";
            Health = 50;
            // От власне те, що змінилося від попередньої версії
            //Picture = Image.FromFile("Dragon.jpg");

            Picture = UnitImagesFactory.CrateDragonImage();
        }
    }    class UnitImagesFactory
    {
        public static Dictionary<Type, Image> Images = new Dictionary<Type, Image>();
        public static Image CrateDragonImage()
        {
            if (!Images.ContainsKey(typeof(Dragon)))
            {
                Images.Add(typeof(Dragon), Image.FromFile("Dragon.jpg"));
            }
            return Images[typeof(Dragon)];
        }
        public static Image CrateGoblinImage()
        {
            if (!Images.ContainsKey(typeof(Goblin)))
            {
                Images.Add(typeof(Goblin), Image.FromFile("Goblin.jpg"));
            }
            return Images[typeof(Goblin)];
        }
    }
}
