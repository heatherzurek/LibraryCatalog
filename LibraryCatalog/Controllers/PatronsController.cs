using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System;

namespace LibraryCatalog.Controllers
{
  public class PatronsController : Controller
  {

    [HttpGet("/patrons")]
    public ActionResult Index()
    {
      List<Patron> allPatrons = Patron.GetAll();
      return View(allPatrons);
    }

    [HttpGet("/patrons/new")]
    public ActionResult New()
    {
     return View();
    }

    [HttpPost("/patrons")]
    public ActionResult Create(string name)
    {

      Patron newPatron = new Patron(name, DateTime.Now);
      newPatron.Save();
      List<Patron> allPatrons = Patron.GetAll();
      return View("Index", allPatrons);
    }

    // [HttpPost("/patrons/search")]
    // public ActionResult SearchByPatron(string name)
    // {
    //   // Book searchBook = new Book(title);
    //   // searchBook.Save();
    //   List<Patron> matchPatrons = new List<Patron>{};
    //   matchPatrons.Add(Patron.Search(name));
    //   return View("Index", matchPatrons);
    // }

    [HttpGet("/patrons/{id}")]
    public ActionResult Show(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Patron selectedPatron = Patron.Find(id);
        List<Book> patronBooks = selectedPatron.GetBooks();
        List<Book> allBooks = Book.GetAll();
        model.Add("patron", selectedPatron);
        model.Add("patronBooks", patronBooks);
        model.Add("allBooks", allBooks);
        return View(model);
    }

    [HttpPost("/patrons/{patronId}/books/new")]
    public ActionResult AddBook(int patronId, int bookId)
    {
      Patron patron = Patron.Find(patronId);
      Book book = Book.Find(bookId);
      patron.AddBook(book);
      return RedirectToAction("Show",  new { id = patronId });
    }

    // [HttpPost("/patrons/{patronId}/delete")]
    // public ActionResult Delete(int patronId)
    // {
    //   Patron Patron = Patron.Find(patronId);
    //   Patron.Delete();
    //   List<Patron> allPatrons = Patron.GetAll();
    //   return RedirectToAction("Index", allPatrons);
    // }



  }
}
