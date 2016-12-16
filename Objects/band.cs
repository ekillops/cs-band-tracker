
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public int NumberMembers {get;set;}

    public Band(string newName, int newNumberMembers, int newId = 0)
    {
      this.Name = newName;
      this.NumberMembers = newNumberMembers;
      this.Id = newId;
    }

    public override bool Equals(System.Object inputObject)
    {
      if (!(inputObject is Band))
      {
        return false;
      }
      else
      {
        Band testBand = (Band) inputObject;
        bool idEquality = this.Id == testBand.Id;
        bool nameEquality = this.Name == testBand.Name;
        bool membersEquality = this.NumberMembers == testBand.NumberMembers;
        return (idEquality && nameEquality && membersEquality);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, number_members) OUTPUT INSERTED.id VALUES (@name, @numberMembers);", conn);
      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@numberMembers", this.NumberMembers);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    //READ
    public static Band Find(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      int foundNumberMembers = 0;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundNumberMembers = rdr.GetInt32(2);
      }

      Band foundBand = new Band(foundName, foundNumberMembers, foundId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundBand;
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        int foundNumberMembers = rdr.GetInt32(2);
        allBands.Add(new Band(foundName, foundNumberMembers, foundId));
      }


      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return allBands;
    }

    //UPDATE
    public static void Update(int targetId, string newName, int newNumberMembers)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands (name, number_members) WHERE id = @targetId VALUES (@name, @numberMembers);", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.Parameters.AddWithValue("@name", newName);
      cmd.Parameters.AddWithValue("@@numberMembers", newNumberMembers);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    //DESTROY
    public static void Delete(int targetId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @targetId;", conn);
      cmd.Parameters.AddWithValue("@targetId", targetId);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
  }
}
