using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System;

namespace LibraryCatalog.Tests
{
  [TestClass]
  public class AuthorTest : IDisposable
  {

    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_catalog_test;";
    }

    public void Dispose()
    {
      Author.ClearAll();
      Book.ClearAll();
    }

    [TestMethod]
    public void AuthorConstructor_CreatesInstanceOfAuthor_Author()
    {
      Author newAuthor = new Author("George Orwell");
      Assert.AreEqual(typeof(Author), newAuthor.GetType());
    }

    [TestMethod]
    public void GetAll_ReturnsAllAuthorObjects_AuthorList()
    {
      //Arrange
      Author newAuthor1 = new Author("George Orwell");
      newAuthor1.Save();
      Author newAuthor2 = new Author("Sylvia Plath");
      newAuthor2.Save();
      List<Author> newList = new List<Author> { newAuthor1, newAuthor2 };

      //Act
      List<Author> result = Author.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsAuthorInDatabase_Author()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();

      //Act
      Author foundAuthor = Author.Find(testAuthor.Id);

      //Assert
      Assert.AreEqual(testAuthor, foundAuthor);
    }

    [TestMethod]
    public void GetBooks_ReturnsEmptyBookList_BookList()
    {
      //Arrange
      // string title = "1984";
      Author newAuthor = new Author("George Orwell");
      List<Book> newList = new List<Book> { };

      //Act
      List<Book> result = newAuthor.GetBooks();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_AuthorsEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Author.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Author()
    {
      //Arrange, Act
      Author firstAuthor = new Author("George Orwell");
      Author secondAuthor = new Author("George Orwell");

      //Assert
      Assert.AreEqual(firstAuthor, secondAuthor);
    }

    [TestMethod]
    public void Save_SavesAuthorToDatabase_AuthorList()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();

      //Act
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToAuthor_Id()
    {
      //Arrange
      Author testAuthor = new Author("Sylvia Plath");
      testAuthor.Save();

      //Act
      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.Id;
      int testId = testAuthor.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetBooks_RetrievesAllBooksWithAuthor_BookList()
    {
      //Arrange, Act
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();
      Book firstBook = new Book("1984");
      firstBook.Save();
      testAuthor.AddBook(firstBook);
      Book secondBook = new Book("animal farm");
      secondBook.Save();
      testAuthor.AddBook(secondBook);
      List<Book> testBookList = new List<Book> {firstBook, secondBook};
      List<Book> resultBookList = testAuthor.GetBooks();

      //Assert
      CollectionAssert.AreEqual(testBookList, resultBookList);
    }

    [TestMethod]
    public void Delete_DeletesAuthorAssociationsFromDatabase_AuthorList()
    {
      //Arrange
      Book testBook = new Book("1984");
      testBook.Save();
      string testName = "MR author";
      Author testAuthor = new Author("MR author");
      testAuthor.Save();

      //Act
      testAuthor.AddBook(testBook);
      testAuthor.Delete();
      List<Author> resultBookAuthors = testBook.GetAuthors();
      List<Author> testBookAuthors = new List<Author> {};

      //Assert
      CollectionAssert.AreEqual(testBookAuthors, resultBookAuthors);
    }

    [TestMethod]
    public void Test_AddBook_AddsBookToAuthor()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();
      Book testBook = new Book("1984");
      testBook.Save();
      Book testBook2 = new Book("Animal Farm");
      testBook2.Save();

      //Act
      testAuthor.AddBook(testBook);
      testAuthor.AddBook(testBook2);
      List<Book> result = testAuthor.GetBooks();
      List<Book> testList = new List<Book>{testBook, testBook2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetBooks_ReturnsAllAuthorBooks_BookList()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();
      Book testBook1 = new Book("1984");
      testBook1.Save();
      Book testBook2 = new Book("Animal Farm");
      testBook2.Save();

      //Act
      testAuthor.AddBook(testBook1);
      List<Book> savedBooks = testAuthor.GetBooks();
      List<Book> testList = new List<Book> {testBook1};

      //Assert
      CollectionAssert.AreEqual(testList, savedBooks);
    }


  }
}
