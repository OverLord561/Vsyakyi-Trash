using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    //Компонує об’єкти в деревовидну структуру для представлення частково-цілої
    //ієрархії.Компонувальник дозволяє розглядати окремі об’єкти і їхні композиції єдиним способом.
    class Program
    {
        static void Main(string[] args)
        {
            var document = new DocumentComponent("ComposableDocument");
            var headerDocumentSection = new HeaderDocumentComponent();
            var body = new DocumentComponent("Body");
            document.AddComponent(headerDocumentSection);
            document.AddComponent(body);
            var customerDocumentSection = new CustomerDocumentComponent(41);
            var orders = new DocumentComponent("Orders");
            var order0 = new OrderDocumentComponent(0);
            var order1 = new OrderDocumentComponent(1);
            orders.AddComponent(order0);
            orders.AddComponent(order1);
            body.AddComponent(customerDocumentSection);
            body.AddComponent(orders);
            string gatheredData = document.GatherData();
            Console.WriteLine(gatheredData);

            Console.ReadLine();
        }
    }

    interface IDocumentComponent
    {
        string GatherData();
        void AddComponent(IDocumentComponent documentComponent);
    }    class OrderDocumentComponent : IDocumentComponent
    {
        public int Id { get; set; }

        public OrderDocumentComponent(int id)
        {
            Id = id;
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }

        public string GatherData()
        {
            return string.Format("<Order>{0}</Order>", Id);
        }
    }    class CustomerDocumentComponent : IDocumentComponent
    {
        private int CustomerIdToGatherData { get; set; }
        public CustomerDocumentComponent(int customerIdToGatherData)
        {
            CustomerIdToGatherData = customerIdToGatherData;
        }
        public string GatherData()
        {
            string customerData;
            switch (CustomerIdToGatherData)
            {
                case 41:
                    customerData = "Andriy Buday";
                    break;

                default:
                    customerData = "Someone else";
                    break;
            }
            return string.Format("<Customer>{0}</Customer>", customerData);
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }
    }


    class HeaderDocumentComponent : IDocumentComponent
    {
        public void AddComponent(IDocumentComponent documentComponent)
        {
            Console.WriteLine("Cannot add to leaf...");
        }

        public string GatherData()
        {
            return string.Format("<Header>{0}</Header>", "Header");
        }
    }

    class DocumentComponent : IDocumentComponent
    {
        public string Name { get; private set; }
        public List<IDocumentComponent> DocumentComponents { get; private set; }
        public DocumentComponent(string name)
        {
            Name = name;
            DocumentComponents = new List<IDocumentComponent>();
        }
        public string GatherData()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<{0}>", Name));
            foreach (var documentComponent in DocumentComponents)
            {
                documentComponent.GatherData();
                stringBuilder.AppendLine(documentComponent.GatherData());
            }
            stringBuilder.AppendLine(string.Format("</{0}>", Name));
            return stringBuilder.ToString();
        }
        public void AddComponent(IDocumentComponent documentComponent)
        {
            DocumentComponents.Add(documentComponent);
        }
    }
}
