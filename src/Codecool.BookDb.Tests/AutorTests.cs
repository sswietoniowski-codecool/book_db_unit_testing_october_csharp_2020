using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Codecool.BookDb.Model;

namespace Codecool.BookDb.Tests
{   [TestFixture]
    class AutorTests
    {
        [Test]
        public void AuthorConstructor_ReturnProperties()
        {
            string firstName = "Adam";
            string lastName = "Nowinski";
            DateTime dateTime = DateTime.Now;

            Author author = new Author(firstName, lastName, dateTime);
            Assert.AreEqual((firstName, lastName, dateTime), (author.FirstName, author.LastName, author.BirthDate));
        }
    }
}
