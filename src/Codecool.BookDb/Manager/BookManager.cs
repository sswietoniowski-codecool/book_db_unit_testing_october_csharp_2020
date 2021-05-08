using Codecool.BookDb.Model;
using Codecool.BookDb.View;

namespace Codecool.BookDb.Manager
{
    public class BookManager : Manager
    {
        private readonly IBookDao _bookDao;
        private readonly IAuthorDao _authorDao;
        
        public BookManager(UserInterface ui, IBookDao bookDao, IAuthorDao authorDao) : base(ui)
        {
            _bookDao = bookDao;
            _authorDao = authorDao;
        }

        protected override string GetName()
        {
            return "Book Manager";
        }

        protected override void List()
        {
            foreach (var book in _bookDao.GetAll())
            {
                _ui.PrintLn(book);
            }
        }

        protected override void Add()
        {
            _ui.PrintLn("Authors:");
            foreach (var a in _authorDao.GetAll())
            {
                _ui.PrintLn(a);
            }

            int authorId = _ui.ReadInt("Author ID", 0);
            Author author = _authorDao.Get(authorId);

            if (author == null)
            {
                _ui.PrintLn("Author was not found!");
                return;
            }

            string title = _ui.ReadString("Title", "Z");
            _bookDao.Add(new Book(author, title));

        }

        protected override void Edit()
        {
            int id = _ui.ReadInt("Book ID", 0);
            Book book = _bookDao.Get(id);

            if (book == null)
            {
                _ui.PrintLn("Book not found");
                return;
            }
            
            _ui.PrintLn(book);
            _ui.PrintLn("Authors:");

            foreach (var a in _authorDao.GetAll())
            {
                _ui.PrintLn(a);
            }

            int authorId = _ui.ReadInt("Author ID", book.Author.Id);
            string title = _ui.ReadString("Title", book.Title);
            Author author = _authorDao.Get(authorId);
            if (author == null)
            {
                _ui.PrintLn("Author not found");
                return;
            }

            book.Author = author;
            book.Title = title;
            _bookDao.Update(book);
        }
    }
}