using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using Xunit;

namespace UnitTests
{
    public class MockDataSourceTests
    {
        [Fact]
        public void DataSourceWithinRange()
        {
            IDataSource mockDataSource = new MockDataSource();
            Assert.InRange(mockDataSource.GetCurrentValue(), 0d, 100d);
        }
    }
}