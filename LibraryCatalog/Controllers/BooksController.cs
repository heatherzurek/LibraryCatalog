using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System;

namespace LibraryCatalog.Controllers
{
  public class BooksController : Controller
  {

    [HttpGet("/books")]
    public ActionResult Index()
    {
      List<Book> allBooks = Book.GetAll();
      return View(allBooks);
    }

    [HttpGet("/books/new")]
    public ActionResult New()
    {
     return View();
    }

    [HttpPost("/books")]
    public ActionResult Create(string title)
    {
      Book newBook = new Book(title);
      newBook.Save();
      List<Book> allBooks = Book.GetAll();
      return View("Index", allBooks);
    }


    [HttpPost("/books/search")]
    public ActionResult SearchByAuthor(string title)
    {
      // Book searchBook = new Book(title);
      // searchBook.Save();
      List<Book> matchBooks = new List<Book>{};
      matchBooks.Add(Book.Search(title));
      return View("Index", matchBooks);
    }


    [HttpGet("/books/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Book selectedBook = Book.Find(id);
      List<Author> bookAuthors = selectedBook.GetAuthors();
      List<Author> allAuthors = Author.GetAll();
      model.Add("selectedBook", selectedBook);
      model.Add("bookAuthors", bookAuthors);
      model.Add("allAuthors", allAuthors);
      return View(model);
    }

    [HttpPost("/books/{bookId}/authors/new")]
    public ActionResult AddAuthor(int bookId, int authorId)
    {
      Book book = Book.Find(bookId);
      Author author = Author.Find(authorId);
      book.AddAuthor(author);
      return RedirectToAction("Show",  new { id = bookId });
    }

    // [HttpPost("/books/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Book.ClearAll();
    //   return View();
    // }
    //
    // [HttpGet("/authors/{authorId}/books/{bookId}/edit")]
    // public ActionResult Edit(int authorId, int bookId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Author author = Author.Find(authorId);
    //   model.Add("author", author);
    //   Book book = Book.Find(bookId);
    //   model.Add("book", book);
    //   return View(model);
    // }
    //
    // [HttpPost("/authors/{authorId}/books/{bookId}")]
    // public ActionResult Update(int authorId, int bookId, string newDescription)
    // {
    //   Book book = Book.Find(bookId);
    //   book.Edit(newDescription);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Author author = Author.Find(authorId);
    //   model.Add("category", category);
    //   model.Add("book", book);
    //   return View("Show", model);
    // }

  }
}
