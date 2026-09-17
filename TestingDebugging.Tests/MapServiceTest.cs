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
        public void AddPoi_InvalidLongitude_ThrowsArgumentOutOfRangeException()
        {
            var map = new MapService();

            Action act = () => map.AddPoi("A", new Coordinate(0, 181));

            Assert.Throws<ArgumentOutOfRangeException>(() => act());
        }


        [Fact]
        public void AddPoi_ValidData_HasCoordinated()
        {
            var map = new MapService();

            var poi = map.AddPoi("Парк", new Coordinate(32.1231, -178.0332));

            Assert.Equal(32.1231, poi.Coordinate?.Latitude);
            Assert.Equal(-178.0332, poi.Coordinate?.Longitude);
        }

        [Fact]
        public void AddPoi_ValidData_RemovesAndGetAll()
        {
            var map = new MapService();

            var a = map.AddPoi("A", new Coordinate(0, 0));
            var b = map.AddPoi("B", new Coordinate(1, 1));

            Assert.Equal(2, map.All.Count);

            map.RemovePoi(a.Id);

            Assert.Equal(1, map.All.Count);
            Assert.Equal(map.All[0].Id, b.Id);
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

            var result = map.RemovePoi(999);

            Assert.False(result);
        }
    }
}
