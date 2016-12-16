using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTest : IDisposable
  {

    public void ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
    }

    [Fact] //Tests find and save at the same time
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


    [Fact]
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

    [Fact]
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


  }
}
