using System;

namespace Prototype
{
    //Призначення. Визначає різновиди об’єктів, щоб створити їх на основі прототипічного екземпляру, і
    //створює нові об’єкти копіюючи цей прототип.
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadLine();
        }

        public static CalendarEvent GetExistingEvent()
        {
            var beerParty = new CalendarEvent();
            var friends = new Attendee[1];
            var andriy = new Attendee { FirstName = "Andriy", LastName = "Buday" };
            friends[0] = andriy;
            beerParty.Attendees = friends;
            beerParty.StartDateAndTime = new DateTime(2010, 7, 23, 19, 0, 0);
            beerParty.Priority = new Priority();
            return beerParty;
        }
        public static void Run()
        {
            var beerParty = GetExistingEvent();
            var nextFridayEvent = (CalendarEvent)beerParty.Clone();
            nextFridayEvent.StartDateAndTime = new DateTime(2010, 7, 30, 19, 0, 0);
            // Про цей код побалакаємо трішки нижче
            nextFridayEvent.Attendees[0].EmailAddress = "andriybuday@liamg.com";
            nextFridayEvent.Priority.SetPriorityValue(0);
            if (beerParty.Attendees != nextFridayEvent.Attendees)
            {
                Console.WriteLine("GOOD: Each event has own list of attendees.");
            }
            if (beerParty.Attendees[0].EmailAddress ==
            nextFridayEvent.Attendees[0].EmailAddress)
            {
                // В цьому випадку добре мати поверхневу копію кожного із учасників,
                // таким чином моя адреса, ім'я і персональні дані залишаються тими ж
                Console.WriteLine(
                "GOOD: Update to my e-mail address will be reflected in all events.");
            }
            if (beerParty.Priority.IsHigh() != nextFridayEvent.Priority.IsHigh())
            {
                Console.WriteLine(
                "GOOD: Each event should have own priority object, fully-copied.");
            }

        }

        public class CalendarPrototype
        {
            public virtual CalendarPrototype Clone()
            {
                var copyOfPrototype = (CalendarPrototype)this.MemberwiseClone();
                return copyOfPrototype;
            }
        }

        public class CalendarEvent : CalendarPrototype
        {
            public Attendee[] Attendees { get; set; }
            public Priority Priority { get; set; }
            public DateTime StartDateAndTime { get; set; }
            // Зауважимо, що метод Clone не перевантажений (покищо)

            public override CalendarPrototype Clone()
            {
                var copy = (CalendarEvent)base.Clone();
                // Це дозволить нам мати інший список із посиланнями на тих же відвідувачів
                var copiedAttendees = (Attendee[])Attendees.Clone();
                copy.Attendees = copiedAttendees;
                // Також скопіюємо приорітет
                copy.Priority = (Priority)Priority.Clone();
                // День і час не варто копіювати – їх заповнять
                // Повертаємо копію події
                return copy;
            }
        }

        public class Priority : ICloneable
        {
            public int PriorityValue = 1;

            public void SetPriorityValue(int value)
            {
                PriorityValue = value;
            }

            public bool IsHigh()
            {
                return PriorityValue > 0;
            }

            public object Clone()
            {
                return new Priority
                {
                    PriorityValue = this.PriorityValue
                };
            }

            static Priority()
            {

            }
        }

        public class Attendee
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string EmailAddress { get; set; }
        }


    }


}