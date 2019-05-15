using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace LibraryCatalog.Models
{
  public class Patron
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Patron(string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

  }
}
