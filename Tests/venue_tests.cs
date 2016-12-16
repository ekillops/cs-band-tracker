using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class VenueTests : IDisposable
  {

    public void VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
    }

    [Fact] //Tests find and save at the same time
    public void Save_SavesToDataBase_EquivalentObject()
    {
      //Arrange
      Venue testVenue = new Venue("Doug Fir", "Medium", 300);
      //Act
      testVenue.Save();
      Venue retrievedVenue = Venue.Find(testVenue.Id);
      //Assert
      Assert.Equal(testVenue, retrievedVenue);
    }


    [Fact]
    public void DeleteAll_EmptiesDatabase_EmptyList()
    {
      //Arrange
      Venue testVenue1 = new Venue("Doug Fir", "Medium", 300);
      testVenue1.Save();
      Venue testVenue2 = new Venue("Mississippi Studios", "Medium", 300);
      testVenue2.Save();
      List<Venue> expectedEmptyList = new List<Venue> {};
      //Act
      Venue.DeleteAll();
      List<Venue> resultList = Venue.GetAll();
      //Assert
      Assert.Equal(expectedEmptyList, resultList);
    }

    [Fact]
    public void Delete_DeletesTargetVenue_ListLength1()
    {
      //Arrange
      Venue testVenue1 = new Venue("Doug Fir", "Medium", 300);
      testVenue1.Save();
      Venue testVenue2 = new Venue("Mississippi Studios", "Medium", 300);
      testVenue2.Save();
      List<Venue> expectedList = new List<Venue> {testVenue2};
      //Act
      Venue.Delete(testVenue1.Id);
      List<Venue> resultList = Venue.GetAll();
      //Assert
      Assert.Equal(expectedList, resultList);
    }

    [Fact]//Also tests AddPerformance
    public void GetPerformances_RetrievesDataFromDB_DictionaryOfInfo()
    {
      //Arrange
      Band testBand1 = new Band("Wild Nothing", 5);
      testBand1.Save();
      Band testBand2 = new Band("The Shins", 5);
      testBand2.Save();
      Venue testVenue = new Venue("Doug Fir", "Medium", 300);
      testVenue.Save();

      Dictionary<int, string> expectedPerformances = new Dictionary<int, string>()
      {
        {testBand1.Id, testBand1.Name},
        {testBand2.Id, testBand2.Name}
      };
      //Act
      testVenue.AddPerformance(testBand1.Id);
      testVenue.AddPerformance(testBand2.Id);
      Dictionary<int, string> retrievedPerformances = testVenue.GetPerformances();
      //Assert
      Assert.Equal(expectedPerformances, retrievedPerformances);
    }
  }
}
