using Codecool.BookDb.Model;
using Codecool.BookDb.View;

namespace Codecool.BookDb.Manager
{
    public abstract class Manager
    {
        protected UserInterface _ui;
        protected abstract string GetName();
        protected abstract void List();
        protected abstract void Add();
        protected abstract void Edit();

        public Manager(UserInterface ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                _ui.PrintTitle(GetName());
                _ui.PrintOption('l', "List");
                _ui.PrintOption('a', "Add");
                _ui.PrintOption('e', "Edit");
                _ui.PrintOption('q', "Quit");

                switch (_ui.Choice("laeq"))
                {
                    case 'l':
                        List();
                        break;
                    case 'a':
                        Add();
                        break;
                    case 'e':
                        Edit();
                        break;
                    case 'q':
                        running = false;
                        break;
                }
            }
        }
    }
}