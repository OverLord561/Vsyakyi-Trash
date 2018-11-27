using System;

namespace Mediator
{
    //Визначає об’єкт, що інкапсулює взаємодію між множиною об’єктів. Медіатор
    //покрацює слабкозв’язність шляхом утримання об’єктів від прямих посилань один на одного, а також дозволяє
    //вам незалежно змінювати взаємодію.
    class Program
    {
        static void Main(string[] args)
        {
            Brain brain = new Brain();

            brain.Ear.HearSomething();

            Console.ReadLine();
        }
    }

    class BodyPart
    {
        private readonly Brain _brain;
        public BodyPart(Brain brain)
        {
            _brain = brain;
        }
        public void Changed()
        {
            _brain.SomethingHappenedToBodyPart(this);
        }
    }    class Ear : BodyPart
    {
        private string _sounds = string.Empty;
        public Ear(Brain brain) : base(brain) { }
        public void HearSomething()
        {
            Console.WriteLine("Enter what you hear:");
            _sounds = Console.ReadLine();
            Changed();
        }

        public string GetSounds()
        {
            return _sounds;
        }
    }    class Face : BodyPart
    {
        public Face(Brain brain)
        : base(brain)
        {
        }
        public void Smile()
        {
            Console.WriteLine("FACE: Smiling...");
        }
    }

    class Brain
    {
        public Brain()
        {
            CreateBodyParts();
        }
        private void CreateBodyParts()
        {
            Ear = new Ear(this);
            Face = new Face(this);

        }
        public Ear Ear { get; private set; }
        public Face Face { get; private set; }
        public void SomethingHappenedToBodyPart(BodyPart bodyPart)
        {
            if (bodyPart is Ear)
            {
                string heardSounds = ((Ear)bodyPart).GetSounds();

                if (heardSounds.Contains("stupid"))
                {
                    // Атакуємо образника
                    Console.WriteLine("Sorry, what did u say?");
                }
                else if (heardSounds.Contains("cool"))
                {
                    Face.Smile();
                }
            }

            //else if (bodyPart is Eye)
            //{
            //    // Мозок може проаналізувати, що ви бачите і
            //    // прореагувати відповідно, використовуючи різні частини тіла
            //}
            //else if (bodyPart is Hand)
            //{
            //    var hand = (Hand)bodyPart;
            //    bool hurtingFeeling = hand.DoesItHurt();
            //    if (hurtingFeeling)
            //    {
            //        Leg.StepBack();
            //    }
            //    bool itIsNice = hand.IsItNice();
            //    if (itIsNice)
            //    {
            //        Leg.StepForward();
            //        Hand.Embrace();
            //    }
            //}
            //else if (bodyPart is Leg)
            //{
            //    // Якщо на ногу впаде цегла, змінюємо вираз обличчя 
            //}
        }
    }
}


