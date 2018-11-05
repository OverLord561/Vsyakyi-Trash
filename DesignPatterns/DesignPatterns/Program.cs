using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerSingleton.GetInstance();

            HardProcessor processor = new HardProcessor(1);
            logger.Log("Hard work started...");            processor.ProcessTo(5);
            logger.Log("Hard work finished...");

            Console.ReadLine();
        }
    }
}
