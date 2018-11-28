using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerealization
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] deviceAsType = null;
            byte[] deviceAsJson = null;

            DeviceCharacteristics device = new DeviceCharacteristics
            {
                Humidity = 10,
                Temperature = 10
            };


            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, device);
                deviceAsType = ms.ToArray();

                bf.Serialize(ms, JsonConvert.SerializeObject(device));
                deviceAsJson = ms.ToArray();
            }

            Console.WriteLine("JSON - {0}", deviceAsJson.Length);
            Console.WriteLine("Type - {0}", deviceAsType.Length);
            Console.ReadLine();

        }
    }


    [Serializable]
    class DeviceCharacteristics
    {
        public double Temperature { get; set; }

        public double Humidity { get; set; }
    }
}
