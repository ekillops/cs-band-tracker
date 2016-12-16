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

    [Fact]
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
  }
}
