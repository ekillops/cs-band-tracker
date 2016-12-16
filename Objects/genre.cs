using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Genre
  {
    public int Id {get;set;}
    public string Name {get;set;}

    public Genre(string newName, int newId)
    {
      this.Name = newName;
      this.Id = newId;
    }

    public override bool Equals(System.Object inputObject)
    {
      if (!(inputObject is Genre))
      {
        return false;
      }
      else
      {
        Genre testGenre = (Genre) inputObject;
        bool idEquality = this.Id == testGenre.Id;
        bool nameEquality = this.Name == testGenre.Name;
        return (idEquality && nameEquality);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO genres (name) OUTPUT INSERTED.id VALUES (@name);", conn);
      cmd.Parameters.AddWithValue("@name", this.Name);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    //READ
    public static Genre Find(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM genres WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }

      Genre foundGenre = new Genre(foundName, foundId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundGenre;
    }

    public static List<Genre> GetAll()
    {
      List<Genre> allGenres = new List<Genre> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM genres;", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        allGenres.Add(new Genre(foundName, foundId));
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allGenres;
    }

    //UPDATE
    public static void Update(int targetId, string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @name WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.Parameters.AddWithValue("@name", newName);

      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    //DESTROY
    public static void Delete(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM genres_venues WHERE genre_id = @targetId;; DELETE FROM bands_genres WHERE genre_id = @targetId; DELETE FROM genres WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM genres_venues; DELETE FROM bands_genres; DELETE FROM genres;", conn);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    public List<Band> GetBands()
    {
      List<Band> allBands = new List<Band> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM genres JOIN bands_genres ON (genres.id = bands_genres.genre_id) JOIN bands ON (bands_genres.band_id = bands.id) WHERE genres.id = @genreId;", conn);
      cmd.Parameters.AddWithValue("@genreId", this.Id);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        int bandMembers = rdr.GetInt32(2)
        allBands.Add(new Band(bandName, bandMembers, bandId));
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allBands;
    }

    public List<Venue> GetVenues()
    {
      List<Venue> allVenues = new List<Venue> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM genres JOIN genres_venues ON (genres.id = genres_venues.genre_id) JOIN venues ON (genres_venues.venue_id = venues.id) WHERE genres.id = @genreId;", conn);
      cmd.Parameters.AddWithValue("@genreId", this.Id);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
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
  }
}
