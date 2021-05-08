using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace Codecool.BookDb.Model
{
    public class BookDao : IBookDao
    {
        private readonly string _connectionString;
        public BookDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Add(Book book)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertBookSql =
                    @"
INSERT INTO book (author_id, title)
VALUES (@AuthorId, @Title);

SELECT SCOPE_IDENTITY();
";
                command.CommandText = insertBookSql;

                command.Parameters.AddWithValue("@AuthorId", book.Author.Id);
                command.Parameters.AddWithValue("@Title", book.Title);

                int bookId = Convert.ToInt32(command.ExecuteScalar());
                book.Id = bookId;
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Update(Book book)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string updateBookSql =
                    @"
UPDATE book
SET
    author_id = @AuthorId,
    title = @Title
WHERE   
    id = @Id
";

                command.CommandText = updateBookSql;
                command.Parameters.AddWithValue("@AuthorId", book.Author.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Id", book.Id);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public Book Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectBookSql =
                    @"
SELECT
    b.id as book_id,
    b.title,
    a.id as author_id,
    a.first_name,
    a.last_name,
    a.birth_date
FROM
    book as b
    INNER JOIN
    author as a
    ON (b.author_id = a.id)
WHERE
    b.id = @Id
";
                command.CommandText = selectBookSql;
                command.Parameters.AddWithValue("@Id", id);

                using var dataReader = command.ExecuteReader();

                Book book = null;
            
                if (dataReader.Read())
                {
                    int bookId = (int) dataReader["book_id"];
                    string title = (string) dataReader["title"];
                    int authorId = (int) dataReader["author_id"];
                    string firstName = (string) dataReader["first_name"];
                    string lastName = (string) dataReader["last_name"];
                    DateTime birthDate = Convert.ToDateTime(dataReader["birth_date"]);

                    var author = new Author(firstName, lastName, birthDate);
                    author.Id = authorId;
                    book = new Book(author, title);
                    book.Id = bookId;
                }
                connection.Close();
                return book;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public List<Book> GetAll()
        {
            try
            {
                var books = new List<Book>();
                
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectBookSql =
                    @"
SELECT
    b.id as book_id,
    b.title,
    a.id as author_id,
    a.first_name,
    a.last_name,
    a.birth_date
FROM
    book as b
    INNER JOIN
    author as a
    ON (b.author_id = a.id)
";
                command.CommandText = selectBookSql;

                using var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int bookId = (int) dataReader["book_id"];
                    string title = (string) dataReader["title"];
                    int authorId = (int) dataReader["author_id"];
                    string firstName = (string) dataReader["first_name"];
                    string lastName = (string) dataReader["last_name"];
                    DateTime birthDate = Convert.ToDateTime(dataReader["birth_date"]);

                    var author = new Author(firstName, lastName, birthDate);
                    author.Id = authorId;
                    var book = new Book(author, title);
                    book.Id = bookId;
                
                    books.Add(book);
                }
                connection.Close();
                return books;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}