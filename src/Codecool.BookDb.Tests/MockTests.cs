using Codecool.BookDb.Model;
using Moq;
using NUnit.Framework;
using System;

namespace Codecool.BookDb.Tests
{
[TestFixture]
    class MockTests
    {
        [Test]
        public void MockSetupTest()
        {
            var mockAuthorDao = new Mock<IAuthorDao>();
            var author = new Author("bart", "capote", DateTime.Now);

            mockAuthorDao.Setup(x => x.Get(1)).Returns(author);

            var returnedAuthor = mockAuthorDao.Object.Get(1);

            mockAuthorDao.Object.Add(author);
            mockAuthorDao.Verify(x => x.Add(It.IsAny<Author>()), Times.Once);

            Assert.That(returnedAuthor, Is.EqualTo(author));
        }


    }
}
