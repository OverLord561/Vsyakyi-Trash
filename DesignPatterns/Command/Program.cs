using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            // Замовник
            var customer = new Customer();
            // Із певних міркуваня, бос завжди знає, що грошей стає тільки
            // на бригаду Z
            var team = new Team("Z");
            // Також бос отримав список вимог, що треба буде передати бригаді
            var requirements = new List<Requirement>() { new Requirement("Cool web site"), new Requirement("Ability to book beer on site") };
            // Ви повинні бути готові бути викликаними замовником
            ICommand commandX = new YouAsProjectManagerCommand(team, requirements);
            // Передача вас у «найми» замовнику 
            customer.AddCommand(commandX);

            // В компанії також є програміст-герой, що кодує на швидкості світла
            var heroDeveloper = new HeroDeveloper();
            // Бос вирішив віддати йому проект A
            ICommand commandA = new HeroDeveloperCommand(heroDeveloper, "A");
            customer.AddCommand(commandA);
            // Як тільки замовник підписує контракт із вашим босом,
            // ваша бригада і програміст-герой готові виконати все, що треба
            // згідно вихідного коду контракту
            customer.SignContractWithBoss();

            Console.ReadLine();
        }
    }

    class Customer
    {
        protected List<ICommand> Commands { get; set; }
        public Customer()
        {
            Commands = new List<ICommand>();
        }
        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }
        public void SignContractWithBoss()
        {
            foreach (var command in Commands)
            {
                command.Execute();
            }
        }
    }

    public interface ICommand
    {
        // Кожна Команда має метод для її запуску
        void Execute();
    }
    // Приклад однієї із Команд до виконання
    class YouAsProjectManagerCommand : ICommand
    {
        public YouAsProjectManagerCommand(Team team, List<Requirement> requirements)
        {
            Team = team;
            Requirements = requirements;
        }
        public void Execute()
        {
            // Реалізація делегує роботу до потрібного отримувача
            Team.CompleteProject(Requirements);
        }
        protected Team Team { get; set; }
        protected List<Requirement> Requirements { get; set; }
    }

    public class Team
    {
        public string Name { get; set; }

        public Team(string name)
        {
            Name = name;
        }
        public void CompleteProject(List<Requirement> Requirements)
        {
            foreach (var req in Requirements) {
                Console.WriteLine(req.Name);
            }
        }
    }

    public class Requirement
    {
        public string Name { get; set; }

        public Requirement(string name)
        {
            Name = name;
        }
    }

    // І ще один приклад
    class HeroDeveloperCommand : ICommand
    {
        public HeroDeveloperCommand(HeroDeveloper heroDeveloper, string projectName)
        {
            HeroDeveloper = heroDeveloper;
            ProjectName = projectName;
        }
        public void Execute()
        {
            // Реалізація делегує роботу до потрібного отримувача
            HeroDeveloper.DoAllHardWork(ProjectName);
        }
        protected HeroDeveloper HeroDeveloper { get; set; }
        public string ProjectName { get; set; }
    }


    public class HeroDeveloper {
        public void DoAllHardWork(string projectName) {

        }
    }
}
