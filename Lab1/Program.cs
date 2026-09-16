using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

MapService mapService = new MapService();

PointOfInterest museum = mapService.AddPoi("МузеЙ", new Coordinate(89.001, 32.9191));

void TestAddPoiLatitude1()
{
    PointOfInterest museum = mapService.AddPoi("museum", new Coordinate(-91, 37.6150));
}

void TestAddPoiLatitude2()
{
    PointOfInterest museum = mapService.AddPoi("museum", new Coordinate(91, 37.6150));
}

void TestAddPoiCoordinateNull()
{
    PointOfInterest museum = mapService.AddPoi("museum", null);
}

void TestFindByNameNull()
{
    PointOfInterest? result = mapService.FindByName(null);
}

void TestFindByNameEmpty()
{
    PointOfInterest? result = mapService.FindByName("");
}

void TestFindByNameIgnoreCase()
{
    PointOfInterest? result = mapService.FindByName("Музей");
}
void TestFindByNameWhiteSpace()
{
    PointOfInterest? result = mapService.FindByName("музей            ");
}

TestFindByNameEmpty();