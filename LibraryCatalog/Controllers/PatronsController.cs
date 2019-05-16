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

    //
    // [HttpPost("/patrons/search")]
    // public ActionResult SearchByPatron(string name)
    // {
    //   // Patron searchPatron = new Patron(name);
    //   // searchPatron.Save();
    //   List<Patron> matchPatrons = new List<Patron>{};
    //   matchPatrons.Add(Patron.Search(name));
    //   return View("Index", matchPatrons);
    // }


    // [HttpGet("/patrons/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Patron selectedPatron = Patron.Find(id);
    //   List<Author> bookPatron = selectedPatron.GetPatron();
    //   List<Author> allPatron = Author.GetAll();
    //   model.Add("selectedPatron", selectedPatron);
    //   model.Add("bookPatron", bookPatron);
    //   model.Add("allPatron", allPatron);
    //   return View(model);
    // }

    // [HttpPost("/patrons/{bookId}/authors/new")]
    // public ActionResult AddAuthor(int bookId, int authorId)
    // {
    //   Patron book = Patron.Find(bookId);
    //   Author author = Author.Find(authorId);
    //   book.AddAuthor(author);
    //   return RedirectToAction("Show",  new { id = bookId });
    // }
    //
    // [HttpPost("/patrons/{bookId}/delete")]
    // public ActionResult Delete(int bookId)
    // {
    //   Patron Patron = Patron.Find(bookId);
    //   Patron.DeletePatron();
    //   List<Patron> allPatrons = Patron.GetAll();
    //   return RedirectToAction("Index", allPatrons);
    // }
    //
    // [HttpGet("/authors/{authorId}/patrons/{bookId}/edit")]
    // public ActionResult Edit(int authorId, int bookId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Author author = Author.Find(authorId);
    //   model.Add("author", author);
    //   Patron book = Patron.Find(bookId);
    //   model.Add("book", book);
    //   return View(model);
    // }
    //
    // [HttpPost("/authors/{authorId}/patrons/{bookId}")]
    // public ActionResult Update(int authorId, int bookId, string newDescription)
    // {
    //   Patron book = Patron.Find(bookId);
    //   book.Edit(newDescription);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Author author = Author.Find(authorId);
    //   model.Add("category", category);
    //   model.Add("book", book);
    //   return View("Show", model);
    // }

  }
}
