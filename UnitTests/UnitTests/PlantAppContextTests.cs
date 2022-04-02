using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Moq;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using PlantMonitorWebApp.Repository;
using PlantMonitorWebApp.Shared.Models;
using PlantMonitorWebApp.Shared.TestData;

namespace UnitTests
{
    public class PlantAppContextTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<PlantAppContext> _contextOptions;

        public PlantAppContextTests()
        {
            //_connection = new SqliteConnection("Filename=:memory:");
            //_connection.Open();

            _contextOptions = new DbContextOptionsBuilder<PlantAppContext>()
                .UseSqlite("Data Source=C://TEMP/PLANTAPPDB1.sqlite;")
                .Options;
        }

        public void Dispose()
        {
            //_connection.Close();
            //connection.Dispose();
        }

        [Fact]
        public void SeedAndReadData()
        {
            using var context = new PlantAppContext(_contextOptions);

            context.Database.EnsureCreated();

            IEnumerable<Sensor> sensorList = TestPlantProvider.GetTestSensors();
            context.Sensors.AddRange(sensorList);
            context.SaveChanges();

            List<Plant> plantList = TestPlantProvider.GetTestPlants();
            List<Sensor> loadedSensors = context.Sensors.ToListAsync().Result;
            plantList[0].Sensor = loadedSensors.Where(s => s.Id == 1).First();
            plantList[1].Sensor = loadedSensors.Where(s => s.Id == 2).First();
            plantList[2].Sensor = loadedSensors.Where(s => s.Id == 1).First();
            context.Plants.AddRange(plantList);
            context.SaveChanges();

            Assert.Equal(3, context.Plants.ToList().Count);
        }

    }
}
