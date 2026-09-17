
using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

namespace TestingDebugging.Tests;

public class MapServiceSystemTest
{

    [Fact]
    public void AddPoi_ValidData_IncrementedId()
    {
        var map = new MapService();

        var poi = map.AddPoi("Парк", new Coordinate(32.1231, -178.0332));

        Assert.True(poi.Id > 0);
        Assert.Equal("Парк", poi.Name);
        Assert.True(map.All.Count == 1);
    }

    [Fact]
    public void AddPoi_InvalidName_ThrowsArgumentException()
    {
        var map = new MapService();

        Action act = () => map.AddPoi("", new Coordinate(0, 0));

        Assert.Throws<ArgumentException>(() => act());
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
    public void FindByName_ExistingName_ReturnsPoint()
    {
        var map = new MapService();
        map.AddPoi("Музей", new Coordinate(0, 0));


        var result = map.FindByName("Музей");

        Assert.NotNull(result);
        Assert.Equal("Музей", result.Name);
    }

    [Fact]
    public void FindByName_ExistingName_ReturnsNull()
    {
        var map = new MapService();

        var result = map.FindByName("Парк");

        Assert.Null(result);
    }

    [Fact]
    public void GetDistance_ExistingPoints_ReturnsDistanse()
    {
        var map = new MapService();

        var a = map.AddPoi("Парк", new Coordinate(32.1231, -178.0332));
        var b = map.AddPoi("Музей", new Coordinate(-2.0031, 180));

        var result = map.GetDistance(a.Id, b.Id);

        Assert.True(result > 0);
    }
}