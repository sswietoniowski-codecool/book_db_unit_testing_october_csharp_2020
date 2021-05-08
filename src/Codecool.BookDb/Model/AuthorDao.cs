using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace Codecool.BookDb.Model
{
    public class AuthorDao : IAuthorDao
    {
        private readonly string _connectionString;
        
        public AuthorDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Add(Author author)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertAuthorSql =
                    @"
INSERT INTO author (first_name, last_name, birth_date)
VALUES (@FirstName, @LastName, @BirthDate)

SELECT SCOPE_IDENTITY();
";

                command.CommandText = insertAuthorSql;
                command.Parameters.AddWithValue("@FirstName", author.FirstName);
                command.Parameters.AddWithValue("@LastName", author.LastName);
                command.Parameters.AddWithValue("BirthDate", author.BirthDate);

                int authorId = Convert.ToInt32(command.ExecuteScalar());
                author.Id = authorId;
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Update(Author author)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string updateAuthorSql =
                    @"
UPDATE author
SET
    first_name = @FirstName,
    last_name = @LastName,
    birth_date = @BirthDate
WHERE   
    id = @Id
";

                command.CommandText = updateAuthorSql;
                command.Parameters.AddWithValue("@FirstName", author.FirstName);
                command.Parameters.AddWithValue("@LastName", author.LastName);
                command.Parameters.AddWithValue("@BirthDate", author.BirthDate);
                command.Parameters.AddWithValue("@Id", author.Id);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public Author Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectAuthorSql = "SELECT id, first_name, last_name, birth_date FROM author WHERE id = @Id";

                command.CommandText = selectAuthorSql;
                command.Parameters.AddWithValue("@Id", id);

                using var dataReader = command.ExecuteReader();

                Author author = null;
            
                if (dataReader.Read())
                {
                    string firstName = (string)dataReader["first_name"];
                    string lastName = (string) dataReader["last_name"];
                    DateTime birthDate = Convert.ToDateTime(dataReader["birth_date"]);

                    author = new Author(firstName, lastName, birthDate);
                    author.Id = id;
                }
                
                connection.Close();

                return author;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public List<Author> GetAll()
        {
            try
            {
                var authors = new List<Author>();

                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectAuthorsSql = "SELECT id, first_name, last_name, birth_date FROM author";
                command.CommandText = selectAuthorsSql;
            
                using var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = (int)dataReader["id"];
                    string firstName = (string)dataReader["first_name"];
                    string lastName = (string) dataReader["last_name"];
                    DateTime birthDate = Convert.ToDateTime(dataReader["birth_date"]);

                    var author = new Author(firstName, lastName, birthDate);
                    author.Id = id;
                
                    authors.Add(author);
                }

                connection.Close();

                return authors;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}