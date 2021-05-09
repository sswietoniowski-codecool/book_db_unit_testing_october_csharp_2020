using Codecool.BookDb.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace Codecool.BookDb.Tests
{
    [TestFixture]
    class AuthorTests
    {
        [Test]
        public void Author_Constructor_ShouldInitialize()
        {
            string firstName = "Pawel";
            string lastName = "Bonecki";
            DateTime birthDay = DateTime.Now;

            Author author = new Author(firstName, lastName, birthDay);

            Assert.AreEqual(firstName, author.FirstName, "Issue with ....");
            Assert.AreEqual(lastName, author.LastName);
            Assert.AreEqual(birthDay, author.BirthDate);

        }
    }
}
