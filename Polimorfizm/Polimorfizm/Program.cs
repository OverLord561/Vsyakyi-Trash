using System;

namespace Polimorfizm
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseClass baseClassc = new BaseClass();
            InheritClass inheritClass = new InheritClass();

            BaseClass test = (BaseClass)inheritClass;
            test.VirtualMethod(); // Hello from inherit
            //test.VirtualMethod(); // Hello from base


            Console.ReadLine();
        }
    }

    class BaseClass
    {
        public virtual void VirtualMethod()
        {
            Console.WriteLine("Hello from base");
        }
    }

    class InheritClass : BaseClass
    {
        public override void VirtualMethod()
        {
            Console.WriteLine("Hello from inherit");
        }

        //public new void VirtualMethod()
        //{
        //    Console.WriteLine("Hello from inherit");
        //}
    }
}
