using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    //Визначає залежність один до багатьох між об’єктами таким чином, що коли один
    //змінює свій стан, всі залежні є проінформовані та оновлені автоматично.
    class Program
    {
        static void Main(string[] args)
        {
            var boxFight = new BoxFight();
            var riskyPlayer = new RiskyPlayer();
            var conservativePlayer = new ConservativePlayer();
            boxFight.AttachObserver(riskyPlayer);
            boxFight.AttachObserver(conservativePlayer);
            boxFight.NextRound();
            boxFight.NextRound();
            boxFight.NextRound();
            boxFight.NextRound();

            Console.ReadLine();
        }
    }

    interface IObserver
    {
        void Update(ISubject subject);
    }
    class RiskyPlayer : IObserver
    {
        public string BoxerToPutMoneyOn { get; set; }
        public void Update(ISubject subject)
        {
            var boxFight = (BoxFight)subject;
            if (boxFight.BoxerAScore > boxFight.BoxerBScore)
                BoxerToPutMoneyOn = "I put on boxer B, if he win I get more!";
            else BoxerToPutMoneyOn = "I put on boxer A, if he win I get more!";
            Console.WriteLine("RISKYPLAYER:{0}", BoxerToPutMoneyOn);
        }
    }    class ConservativePlayer : IObserver
    {
        public string BoxerToPutMoneyOn { get; set; }
        public void Update(ISubject subject)
        {
            var boxFight = (BoxFight)subject;
            if (boxFight.BoxerAScore < boxFight.BoxerBScore)
                BoxerToPutMoneyOn = "I put on boxer B, better be safe!";
            else BoxerToPutMoneyOn = "I put on boxer A, better be safe!";

            Console.WriteLine("CONSERVATIVEPLAYER:{0}", BoxerToPutMoneyOn);
        }
    }
    interface ISubject
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void Notify();
    }
    class BoxFight : ISubject
    {
        public List<IObserver> Observers { get; private set; }
        public int RoundNumber { get; private set; }
        private Random Random = new Random();
        public int BoxerAScore { get; set; }
        public int BoxerBScore { get; set; }
        public BoxFight()
        {
            Observers = new List<IObserver>();
        }
        public void AttachObserver(IObserver observer)
        {
            Observers.Add(observer);
        }
        public void DetachObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }
        public void NextRound()
        {
            RoundNumber++;
            BoxerAScore += Random.Next(0, 5);
            BoxerBScore += Random.Next(0, 5);
            Notify();
        }
        public void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }
    }
}
