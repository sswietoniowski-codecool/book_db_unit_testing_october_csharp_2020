using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using Microsoft.Data.SqlClient;

namespace Codecool.BookDb.Manager
{
    public class BookDbManager
    {
        private string ConnectionString => ConfigurationManager.AppSettings["connectionString"]; 

        private readonly UserInterface _ui;
        private IAuthorDao _authorDao;
        private IBookDao _bookDao;
        
        public BookDbManager(UserInterface ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            try
            {
                Setup();
            }
            catch (SqlException)
            {
                Console.WriteLine("Could not connect to the database.");
            }

            bool running = true;
            while (running)
            {
                _ui.PrintLn("Main menu");
                _ui.PrintOption('a', "Authors");
                _ui.PrintOption('b', "Books");
                _ui.PrintOption('q', "Quit");

                switch (_ui.Choice("abq"))
                {
                    case 'a':
                        new AuthorManager(_ui, _authorDao).Run();
                        break;
                    case 'b':
                        new BookManager(_ui, _bookDao, _authorDao).Run();
                        break;
                    case 'q':
                        running = false;
                        break;
                }
            }
        }

        private void Setup()
        {
            string connectionString = ConnectionString;
            _authorDao = new AuthorDao(connectionString);
            _bookDao = new BookDao(connectionString);
        }
    }
}