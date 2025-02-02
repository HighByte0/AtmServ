using System;

namespace AtmView.Licensing.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // generation du fichier client           
            ContactWriter.ToFile(@"d:\ContactData.txt", CreateContact());

            Console.WriteLine("Ficher des données client generé");
            Console.ReadLine();
        }

        private static Contact CreateContact()
        {
            return ContactFactory.Create<Contact>();
        }
    }
}
