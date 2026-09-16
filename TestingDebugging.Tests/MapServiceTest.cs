using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

namespace TestingDebugging.Tests
{
    public class MapServiceTest
    {
        [Fact]
        public void AddPoi_ValidData_IncrementedId()
        {
            var map = new MapService();

            var poi = map.AddPoi("Музей", new Coordinate(32.1231, -178.0332));

            Assert.True(poi.Id > 0);
            Assert.Equal("Музей", poi.Name);
            Assert.True(map.All.Count == 1);
        }

        [Fact]
        public void AddPoi_InvalidName_ThrowsArgumentException()
        {
            var map = new MapService();

            Action act = () => map.AddPoi("", new Coordinate(32.1231, -178.0332));

            Assert.Throws<ArgumentException>(() => act());   
        }

        [Fact]
        public void AddPoi_NullCoordinate_ThrowsArgumentNullException()
        {
            var map = new MapService();

            Action act = () => map.AddPoi("Музей", null!);

            Assert.Throws<InvalidOperationException>(() => act());
        }

        [Fact]
        public void RemovePoi_ExistingId_RemovesAnddReturnsTrue()
        {
            var map = new MapService();
            var poi = map.AddPoi("Музей", new Coordinate(32.1231, -178.0332));

            var result = map.RemovePoi(poi.Id);

            Assert.True(result);
            Assert.True(map.All.Count == 0);
        }

        [Fact]
        public void RemovePoi_UnknownId_ReturnsFalse()
        {
            var map = new MapService();

            var result = map.RemovePoi(-1);

            Assert.False(result);
        }
    }
}
