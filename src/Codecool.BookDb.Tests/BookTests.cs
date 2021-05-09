using System;
using System.Collections.Generic;
using System.Text;
using Codecool.BookDb.Model;
using NUnit.Framework;

namespace Codecool.BookDb.Tests
{
    [TestFixture]
    class BookTests
    {
        [Test]
        public void Book_ToString_Formated_Correctly()
        {
            string firstName = "Adam";
            string lastName = "Nowinski";
            DateTime dateTime = DateTime.Now;

            Author author = new Author(firstName, lastName, dateTime);

            string title = "Hobbit";

            Book book = new Book(author, title);

            string expectedString = $"0, {title}, {firstName}, {lastName}";
            Assert.AreEqual(expectedString, book.ToString());
        }

        [Test]
        public void Book_ToString_Throws_NullException()
        {
            string title = "Hobbit";

            Book book = new Book(null, title);

            Assert.Throws<NullReferenceException>(() => book.ToString());
        }



    }
}
