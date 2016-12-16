using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public string Size {get;set;}
    public int Capacity {get;set;}

    public Venue(string newName, string newSize, int newCapacity, int newId = 0)
    {
      this.Name = newName;
      this.Size = newSize;
      this.Capacity = newCapacity;
      this.Id = newId;
    }

    public override bool Equals(System.Object inputObject)
    {
      if (!(inputObject is Venue))
      {
        return false;
      }
      else
      {
        Venue testVenue = (Venue) inputObject;
        bool idEquality = this.Id == testVenue.Id;
        bool nameEquality = this.Name == testVenue.Name;
        bool sizeEquality = this.Size == testVenue.Size;
        bool capactiyEqaultity = this.Capacity == testVenue.Capacity;
        return (idEquality && nameEquality && sizeEquality && capactiyEqaultity);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    //CREATE
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, size, capacity) OUTPUT INSERTED.id VALUES (@name, @size, @capacity);", conn);
      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@size", this.Size);
      cmd.Parameters.AddWithValue("@capacity", this.Capacity);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    //READ
    public static Venue Find(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      string foundSize = null;
      int foundCapacity = 0;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundSize = rdr.GetString(2);
        foundCapacity = rdr.GetInt32(3);
      }

      Venue foundVenue = new Venue(foundName, foundSize, foundCapacity, foundId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundVenue;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        string foundSize = rdr.GetString(2);
        int foundCapacity = rdr.GetInt32(3);
        allVenues.Add(new Venue(foundName, foundSize, foundCapacity, foundId));
      }


      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allVenues;
    }

    //UPDATE
    public static void Update(int targetId, string newName, string newSize, int newCapacity)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues (name, size, capacity) WHERE id = @targetId VALUES (@name, @size, @capacity);", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.Parameters.AddWithValue("@name", newName);
      cmd.Parameters.AddWithValue("@size", newSize);
      cmd.Parameters.AddWithValue("@capactiy", newCapacity);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    //DESTROY
    public static void Delete(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
  }
}
