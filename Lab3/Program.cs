using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

Console.WriteLine("Lab3");

var map = new MapService();

PointOfInterest[] points = new PointOfInterest[10];

var rnd = new Random();

for (int i = 0; i < points.Length; i++)
{
    points[i] = map.AddPoi($"Name: {i + 1} | ", new Coordinate(rnd.Next(-90, 90), rnd.Next(-180, 180)));
}

for (int i = 0; i < points.Length; i++)
{
    Console.WriteLine(points[i]);
}