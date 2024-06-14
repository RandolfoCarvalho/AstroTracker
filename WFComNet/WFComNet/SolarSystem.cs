using System.Collections.Generic;
using System;

public class SolarSystem
{
    public List<Planet> Planets { get; }

    public SolarSystem()
    {
        Planets = new List<Planet>
        {
            new Planet("", 1.989 * Math.Pow(10, 30), 4880, 27.97, 0.387, 0.2056, 7.006, 28.331, 19.124),
            new Planet("Mercury", 3.3011 * Math.Pow(10, 23), 4880, 87.97, 0.387, 0.2056, 7.006, 48.331, 29.124),
            new Planet("Venus", 4.8675 * Math.Pow(10, 24), 6052, 224.7, 0.723, 0.0068, 3.394, 76.68, 54.89),
            new Planet("Earth", 5.9724 * Math.Pow(10, 24), 6371, 365.25, 1.000, 0.0167, 0, 0, 102.937),
            new Planet("Mars", 6.4171 * Math.Pow(10, 23), 3390, 686.98, 1.524, 0.0934, 1.850, 49.562, 286.537),
            new Planet("Jupiter", 1.8986 * Math.Pow(10, 27), 71492, 4332.82, 5.204, 0.0484, 1.305, 100.556, 273.867),
            new Planet("Saturn", 5.6834 * Math.Pow(10, 26), 60268, 10755.70, 9.582, 0.0565, 2.485, 113.715, 339.392),
            new Planet("Uranus", 8.6810 * Math.Pow(10, 25), 25559, 30687.15, 19.189, 0.0472, 0.773, 74.229, 96.998),
            new Planet("Neptune", 1.0241 * Math.Pow(10, 26), 24764, 60190.03, 30.07, 0.0086, 1.770, 131.721, 272.846)
        };
    }
}