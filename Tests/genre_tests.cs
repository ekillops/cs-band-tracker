using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class GenreTests : IDisposable
  {

    public void GenreTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Genre.DeleteAll();
    }

    [Fact] //CREATE + READ
    public void Save_SavesToDataBase_EquivalentObject()
    {
      //Arrange
      Genre testGenre = new Genre("Post-Punk");
      //Act
      testGenre.Save();
      Genre retrievedGenre = Genre.Find(testGenre.Id);
      //Assert
      Assert.Equal(testGenre, retrievedGenre);
    }

    [Fact]//READ
    public void GetAll_RetrievesCorrectObject_EquivalentObject()
    {
      //Arrange
      Genre testGenre1 = new Genre("Post-Punk");
      testGenre1.Save();
      Genre testGenre2 = new Genre("Indie-Rock");
      testGenre2.Save();
      List<Genre> expectedResult = new List<Genre> { testGenre1, testGenre2 };
      //Act
      List<Genre> result = Genre.GetAll();
      //Assert
      Assert.Equal(expectedResult, result);
    }

    [Fact]//DELETE
    public void DeleteAll_EmptiesDatabase_EmptyList()
    {
      //Arrange
      Genre testGenre1 = new Genre("Post-Punk");
      testGenre1.Save();
      Genre testGenre2 = new Genre("Indie-Rock");
      testGenre2.Save();
      List<Genre> expectedEmptyList = new List<Genre> {};
      //Act
      Genre.DeleteAll();
      List<Genre> resultList = Genre.GetAll();
      //Assert
      Assert.Equal(expectedEmptyList, resultList);
    }

    [Fact]//DELETE
    public void Delete_DeletesTargetGenre_ListLength1()
    {
      //Arrange
      Genre testGenre1 = new Genre("Post-Punk");
      testGenre1.Save();
      Genre testGenre2 = new Genre("Indie-Rock");
      testGenre2.Save();
      List<Genre> expectedList = new List<Genre> {testGenre2};
      //Act
      Genre.Delete(testGenre1.Id);
      List<Genre> resultList = Genre.GetAll();
      //Assert
      Assert.Equal(expectedList, resultList);
    }
  }
}
