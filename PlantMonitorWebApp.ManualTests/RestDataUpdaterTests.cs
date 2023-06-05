using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Server.Services.DataUpdater;
using PlantMonitorWebApp.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Configuration;
using System.Collections.Generic;
using Xunit;

namespace PlantMonitorWebApp.ManualTests
{
    public class RestDataUpdaterTests : IDisposable
    {
        private ILogger<RestDataUpdater> _logger;
        private Mock<IDataSourceFactory> _dataSourceFactoryMock;
        private Mock<IMessageSender> _messageSenderMock;

        public RestDataUpdaterTests()
        {
            _logger = new Mock<ILogger<RestDataUpdater>>().Object;
            _dataSourceFactoryMock = new Mock<IDataSourceFactory>();
            _messageSenderMock = new Mock<IMessageSender>();
        }

        public void Dispose()
        {
        }

        //[Fact]
        //public void DataSourcesAreCreatedFromConfig()
        //{
        //    var config = CreateConfigWithOneRestDataSource();
        //    var messageSender = new Mock<IMessageSender>().Object;
        //
        //    Mock<IDataSource> mockSource1 = new();
        //    Mock<IDataSource> mockSource2 = new();
        //    _dataSourceFactoryMock
        //        .Setup(x => x.CreateDataSource("https://www.uri1.de/1"))
        //        .Returns(mockSource1.Object);
        //    _dataSourceFactoryMock
        //        .Setup(x => x.CreateDataSource("https://www.uri2.de/2"))
        //        .Returns(mockSource2.Object);
        //
        //    RestDataUpdater testUpdater = new(_dataSourceFactoryMock.Object, config, messageSender, _logger);
        //    testUpdater.RefreshData(null);
        //
        //    _dataSourceFactoryMock.VerifyAll();
        //}

        private IConfiguration CreateConfigWithOneRestDataSource()
        {
            var confDict = new Dictionary<string, string>()
            {
                {"RestDataSources:0", "https://www.uri1.de/1"},
                {"RestDataSources:1", "https://www.uri2.de/2"}
            };
            var config= new ConfigurationBuilder().AddInMemoryCollection(confDict).Build();
            return config;
        }

    }
}
