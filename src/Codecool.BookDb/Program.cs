using System;
using Codecool.BookDb.Manager;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;

namespace Codecool.BookDb
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                UserInterface ui = new UserInterface();
                new BookDbManager(ui).Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
