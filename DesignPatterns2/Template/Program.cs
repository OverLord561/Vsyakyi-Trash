using System;

namespace Template
{
    //Визначає скелет алгоритму в операціях, передаючи деякі кроки підкласам.
    //Шаблонний Метод дозволяє підкласам перевизначити певні кроки алгоритму без зміни структури алгоритму.
    class Program
    {
        static void Main(string[] args)
        {
            ImportantMessagesSearcher searcher = new ImportantMessagesSearcher(DateTime.Today, "Sally");
            searcher.Search();

            MessagesSearcher searcher2 = new ImportantMessagesSearcher(DateTime.Today, "Sally");
            searcher2.Search();

            Console.ReadLine();
        }
    }
    class MessagesSearcher
    {
        protected DateTime DateSent;
        protected String PersonName;
        protected int ImportanceLevel;
        public MessagesSearcher(DateTime dateSent, String personName, int
       importanceLevel)
        {
            DateSent = dateSent;
            PersonName = personName;
            ImportanceLevel = importanceLevel;
        }
        // Базові операції (primitive operations)
        protected virtual void CreateDateCriteria()
        {
            Console.WriteLine("Standard date criteria has been applied.");
        }
        protected virtual void CreateSentPersonCriteria()
        {
            Console.WriteLine("Standard person criteria has been applied.");
        }
        protected virtual void CreateImportanceCriteria()
        {
            Console.WriteLine("Standard importance criteria has been applied.");
        }
        // Метод, який називають шаблонним
        public String Search()
        {
            CreateDateCriteria();
            CreateSentPersonCriteria();
            Console.WriteLine("Template method does some verification accordingly to search algo.");

            CreateImportanceCriteria();
            Console.WriteLine("Template method verifies if message could be soimportant or useless from person provided in criteria.");

            return "Some list of messages...";
        }
    }

    class ImportantMessagesSearcher : MessagesSearcher
    {
        public ImportantMessagesSearcher(DateTime dateSent, String personName)
        : base(dateSent, personName, 3) // «3» означає, що повідомлення важливе
        {
        }
        // Одна операція перевантажена (IMPORTANT в кінці)
        protected override void CreateImportanceCriteria()
        {
            Console.WriteLine(
            "Special importance criteria has been formed: IMPORTANT");
        }
    }
}
