using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using Moq;
using System;
using Xunit;

namespace UnitTests
{
    public class MockDataSourceTests
    {
        [Fact]
        public void DataSourceWithinRange()
        {
            var rngGenerator = new Random();

            var MockSeedSource = new Mock<ISeedSource>();
            MockSeedSource.Setup(
                x => x.GetSeedValue())
                      .Returns((long)rngGenerator.Next() << 32 + rngGenerator.Next());

            IDataSource mockDataSource = new MockDailyData(MockSeedSource.Object);
            Assert.InRange(mockDataSource.GetCurrentValue(), 0d, 100d);
        }

        [Fact]
        public void SameSeedReturnsSameValue()
        {
            var MockSeedSource = new Mock<ISeedSource>();
            MockSeedSource.Setup(x => x.GetSeedValue()).Returns(3333);

            IDataSource mockDataSource = new MockDailyData(MockSeedSource.Object);
            double value1 = mockDataSource.GetCurrentValue();
            double value2 = mockDataSource.GetCurrentValue();
 
            Assert.Equal(value1, value2);
        }

        [Fact]
        public void DifferentSeedsReturnsDifferentValues()
        {
            var MockSeedSource = new Mock<ISeedSource>();
            MockSeedSource.SetupSequence(
                mocks => mocks.GetSeedValue())
                              .Returns(0)
                              .Returns(3333);
 
            IDataSource mockDataSource = new MockDailyData(MockSeedSource.Object);
            double value1 = mockDataSource.GetCurrentValue();
            double value2 = mockDataSource.GetCurrentValue();

            Assert.NotEqual(value1, value2);
        }
    }
}