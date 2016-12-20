using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTests : IDisposable
  {

    public void BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [Fact] //CREATE + READ
    public void Save_SavesToDataBase_EquivalentObject()
    {
      //Arrange
      Band testBand = new Band("The Chameleons", 4);
      //Act
      testBand.Save();
      Band retrievedBand = Band.Find(testBand.Id);
      //Assert
      Assert.Equal(testBand, retrievedBand);
    }

    [Fact]//READ
    public void GetAll_RetrievesCorrectObject_EquivalentObject()
    {
      //Arrange
      Band testBand1 = new Band("The Chameleons", 4);
      testBand1.Save();
      Band testBand2 = new Band("The Olllam", 4);
      testBand2.Save();
      List<Band> expectedResult = new List<Band> { testBand1, testBand2 };
      //Act
      List<Band> result = Band.GetAll();
      //Assert
      Assert.Equal(expectedResult, result);
    }

    [Fact]//DELETE
    public void DeleteAll_EmptiesDatabase_EmptyList()
    {
      //Arrange
      Band testBand1 = new Band("The Chameleons", 4);
      testBand1.Save();
      Band testBand2 = new Band("The Olllam", 4);
      testBand2.Save();
      List<Band> expectedEmptyList = new List<Band> {};
      //Act
      Band.DeleteAll();
      List<Band> resultList = Band.GetAll();
      //Assert
      Assert.Equal(expectedEmptyList, resultList);
    }

    [Fact]//DELETE
    public void Delete_DeletesTargetBand_ListLength1()
    {
      //Arrange
      Band testBand1 = new Band("The Chameleons", 4);
      testBand1.Save();
      Band testBand2 = new Band("The Olllam", 4);
      testBand2.Save();
      List<Band> expectedList = new List<Band> {testBand2};
      //Act
      Band.Delete(testBand1.Id);
      List<Band> resultList = Band.GetAll();
      //Assert
      Assert.Equal(expectedList, resultList);
    }

    [Fact]
    public void GetPerformances_RetrievesDataFromDB_DictionaryOfInfo()
    {
      //Arrange
      Band testBand = new Band("Wild Nothing", 5);
      testBand.Save();
      Venue testVenue1 = new Venue("Doug Fir", "Medium", 300);
      testVenue1.Save();
      Venue testVenue2 = new Venue("Mississippi Studios", "Medium", 300);
      testVenue2.Save();
      Dictionary<int, string> expectedPerformances = new Dictionary<int, string>()
      {
        {testVenue1.Id, testVenue1.Name},
        {testVenue2.Id, testVenue2.Name}
      };
      //Act
      testBand.AddPerformance(testVenue1.Id);
      testBand.AddPerformance(testVenue2.Id);
      Dictionary<int, string> retrievedPerformances = testBand.GetPerformances();
      //Assert
      Assert.Equal(expectedPerformances, retrievedPerformances);
    }
  }
}
