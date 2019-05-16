using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace LibraryCatalog.Models
{
  public class Patron
  {
    public int Id { get; set; }
    public DateTime CheckoutDate { get; set; }
    public string Name { get; set; }

    public Patron(string name, DateTime checkoutDate, int id = 0)
    {
      Name = name;
      CheckoutDate = checkoutDate;
      Id = id;
    }

    public override bool Equals(System.Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool idEquality = this.Id == newPatron.Id;
        bool nameEquality = this.Name == newPatron.Name;
        bool checkoutDateEquality = this.CheckoutDate == newPatron.CheckoutDate;
        return (idEquality && nameEquality && checkoutDateEquality);
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM patrons;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO patrons (name, checkout_date) VALUES (@name, @checkoutdate);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);
      MySqlParameter checkoutDate = new MySqlParameter();
      checkoutDate.ParameterName = "@checkoutDate";
      checkoutDate.Value = this.CheckoutDate;
      cmd.Parameters.Add(checkoutDate);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Patron> GetAll()
    {
      List<Patron> allPatrons = new List<Patron> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patrons;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int patronId = rdr.GetInt32(0);
        string patronName = rdr.GetString(1);
        DateTime patronCheckoutDate = rdr.GetDateTime(2);
        Patron newPatron = new Patron(patronName, patronCheckoutDate, patronId);
        allPatrons.Add(newPatron);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPatrons;
    }

    public static Patron Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patrons WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int PatronId = 0;
      string PatronName = "";
      DateTime PatronCheckoutDate = new DateTime(2000, 11, 11);
      while(rdr.Read())
      {
        PatronId = rdr.GetInt32(0);
        PatronName = rdr.GetString(1);
        PatronCheckoutDate = rdr.GetDateTime(2);
      }
      Patron newPatron = new Patron(PatronName, PatronCheckoutDate, PatronId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newPatron;
    }

    // public static Author Find(int id)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = id;
    //   cmd.Parameters.Add(searchId);
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   int AuthorId = 0;
    //   string AuthorName = "";
    //   while(rdr.Read())
    //   {
    //     AuthorId = rdr.GetInt32(0);
    //     AuthorName = rdr.GetString(1);
    //   }
    //   Author newAuthor = new Author(AuthorName, AuthorId);
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return newAuthor;
    // }

    public List<Book> GetBooks()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM patrons
      JOIN checkouts ON (patrons.id = checkouts.patrons_id)
      JOIN books ON (checkouts.book_id = books.id)
      WHERE patrons.id = @PatronId;";
      MySqlParameter patronIdParameter = new MySqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = Id;
      cmd.Parameters.Add(patronIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Book> books = new List<Book>{};
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookName = rdr.GetString(1);
        Book newBook = new Book(bookName, bookId);
        books.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return books;
    }

    public void AddBook(Book newBook)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO checkouts (patrons_id, book_id) VALUES (@PatronId, @BookId);";
      // MySqlParameter patron_id = new MySqlParameter();
      // patron_id.ParameterName = "@PatronId";
      // patron_id.Value = Id;
      // cmd.Parameters.Add(patron_id);
      cmd.Parameters.AddWithValue("@PatronId", Id);
      cmd.Parameters.AddWithValue("@BookId", newBook.Id);

      // MySqlParameter book_id = new MySqlParameter();
      // book_id.ParameterName = "@BookId";
      // book_id.Value = newBook.Id;
      // cmd.Parameters.Add(book_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
