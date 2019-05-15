using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace LibraryCatalog.Models
{
  public class Copy
  {
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Count { get; set; }

    public Copy(int bookId, int count, int id = 0)
    {
      Id = id;
      BookId = bookId;
      Count = count;
    }

  }
}
