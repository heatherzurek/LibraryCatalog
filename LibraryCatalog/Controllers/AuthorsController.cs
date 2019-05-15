using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;

namespace LibraryCatalog.Controllers
{
  public class AuthorsController : Controller
  {

    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }

    [HttpGet("/authors/new")]
    public ActionResult New()
    {
      return View();
    }
    //
    [HttpPost("/authors")]
    public ActionResult Create(string name)
    {
      Author newAuthor = new Author(name);
      newAuthor.Save();
      List<Author> allAuthors = Author.GetAll();
      return View("Index", allAuthors);
    }

    [HttpPost("/authors/search")]
    public ActionResult SearchByAuthor(string name)
    {
      // Book searchBook = new Book(title);
      // searchBook.Save();
      List<Author> matchAuthors = new List<Author>{};
      matchAuthors.Add(Author.Search(name));
      return View("Index", matchAuthors);
    }

    [HttpGet("/authors/{id}")]
    public ActionResult Show(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Author selectedAuthor = Author.Find(id);
        List<Book> authorBooks = selectedAuthor.GetBooks();
        List<Book> allBooks = Book.GetAll();
        model.Add("author", selectedAuthor);
        model.Add("authorBooks", authorBooks);
        model.Add("allBooks", allBooks);
        return View(model);
    }

    [HttpPost("/authors/{authorId}/books/new")]
    public ActionResult AddBook(int authorId, int bookId)
    {
      Author author = Author.Find(authorId);
      Book book = Book.Find(bookId);
      author.AddBook(book);
      return RedirectToAction("Show",  new { id = authorId });
    }

    [HttpPost("/authors/{authorId}/delete")]
    public ActionResult Delete(int authorId)
    {
      Author Author = Author.Find(authorId);
      Author.Delete();
      List<Author> allAuthors = Author.GetAll();
      return RedirectToAction("Index", allAuthors);
    }

  }
}
