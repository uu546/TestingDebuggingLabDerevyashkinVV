using InteractiveMap.Console.Models;

namespace TestingDebugging.Tests;

public class CoordinateTest
{
    [Theory]
    [InlineData(-90, -180)]
    [InlineData(90, 180)]
    [InlineData(0, 0)]
    [InlineData(55.75, 37.61)]
    public void Constructor_ValidValues_CreatedCoordinate(double lat, double lon)
    {
        var coord = new Coordinate(lat, lon);

        Assert.NotNull(coord);
    }

    [Theory]
    [InlineData(-91, 0)]
    [InlineData(91, 0)]
    public void Constructor_InvalidLatitude_ThrowsArgumentOutOfRange(double lat, double lon)
    {
        Action act = () => new Coordinate(lat, lon);

        Assert.Throws<ArgumentOutOfRangeException>(() => act());
    }

    [Theory]
    [InlineData(0, -181)]
    [InlineData(0, 181)]
    public void Constructor_InvalidLongitude_ThrowsArgumentOutOfRange(double lat, double lon)
    {
        Action act = () => new Coordinate(lat, lon);

        Assert.Throws<ArgumentOutOfRangeException>(() => act());
    }
}