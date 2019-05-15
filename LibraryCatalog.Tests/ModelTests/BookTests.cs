using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System;

namespace LibraryCatalog.Tests
{
  [TestClass]
  public class BookTest : IDisposable
  {

    public void Dispose()
    {
      Book.ClearAll();
      Author.ClearAll();
    }

    public BookTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_catalog_test;";
    }

    [TestMethod]
    public void BookConstructor_CreatesInstanceOfBook_Book()
    {
      Book newBook = new Book("1984");
      Assert.AreEqual(typeof(Book), newBook.GetType());
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_BookList()
    {
      //Arrange
      List<Book> newList = new List<Book> { };

      //Act
      List<Book> result = Book.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsBooks_BookList()
    {
      //Arrange
      Book newBook1 = new Book("1984");
      newBook1.Save();
      Book newBook2 = new Book("Animal Farm");
      newBook2.Save();
      List<Book> newList = new List<Book> { newBook1, newBook2 };

      //Act
      List<Book> result = Book.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectBookFromDatabase_Book()
    {
      //Arrange
      Book testBook = new Book("1984");
      testBook.Save();

      //Act
      Book foundBook = Book.Find(testBook.Id);

      //Assert
      Assert.AreEqual(testBook, foundBook);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Book()
    {
      // Arrange, Act
      Book firstBook = new Book("1984");
      Book secondBook = new Book("1984");

      // Assert
      Assert.AreEqual(firstBook, secondBook);
    }

    [TestMethod]
    public void Save_SavesToDatabase_BookList()
    {
      //Arrange
      Book testBook = new Book("1984");

      //Act
      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Book testBook = new Book("1984");
      testBook.Save();

      //Act
      Book savedBook = Book.GetAll()[0];

      int result = savedBook.Id;
      int testId = testBook.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetAuthors_ReturnsAllBookAuthors_AuthorList()
    {
      //Arrange
      Book testBook = new Book("1984");
      testBook.Save();
      Author testAuthor1 = new Author("Sylvia Plath");
      testAuthor1.Save();
      Author testAuthor2 = new Author("JK Rowling");
      testAuthor2.Save();

      //Act
      testBook.AddAuthor(testAuthor1);
      List<Author> result = testBook.GetAuthors();
      List<Author> testList = new List<Author> {testAuthor1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void AddAuthor_AddsAuthorToBook_AuthorList()
    {
      //Arrange
      Book testBook = new Book("1984");
      testBook.Save();
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();

      //Act
      testBook.AddAuthor(testAuthor);

      List<Author> result = testBook.GetAuthors();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Delete_DeletesBookAssociationsFromDatabase_BookList()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell");
      testAuthor.Save();
      Book testBook = new Book("1984");
      testBook.Save();

      //Act
      testBook.AddAuthor(testAuthor);
      testBook.Delete();
      List<Book> resultAuthorBooks = testAuthor.GetBooks();
      List<Book> testAuthorBooks = new List<Book> {};

      //Assert
      CollectionAssert.AreEqual(testAuthorBooks, resultAuthorBooks);
    }

  }
}
