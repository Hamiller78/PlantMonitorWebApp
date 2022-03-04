using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

using PlantMonitorWebApp.Repository;
using PlantMonitorWebApp.Shared.Models;
using PlantMonitorWebApp.Shared.TestClasses;

namespace UnitTests
{
    public class PlantAppContextTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<PlantAppContext> _contextOptions;

        public PlantAppContextTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<PlantAppContext>()
                .UseSqlite(_connection)
                .Options;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        [Fact]
        public void SeedAndReadData()
        {
            using var context = new PlantAppContext(_contextOptions);

            context.Database.EnsureCreated();

            IEnumerable<Sensor> sensorList = TestPlantProvider.GetTestSensors();
            context.Sensors.AddRange(sensorList);

            context.SaveChanges();

            List<Sensor> loadedSensors = context.Sensors.ToListAsync().Result;
            Assert.Equal(2, loadedSensors.Count);
        }

    }
}
