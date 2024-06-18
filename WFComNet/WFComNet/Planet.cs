using System;
using System.Collections.Generic;
using WFComNet;

public class Planet : CelestialBody
{
    public Planet(string name, double mass, double radius, double orbitalPeriod, double semiMajorAxis,
                  double eccentricity, double inclination, double longitudeOfAscendingNode, double argumentOfPeriapsis)
        : base(name, mass, radius, orbitalPeriod, semiMajorAxis, eccentricity, inclination, longitudeOfAscendingNode, argumentOfPeriapsis)
    {
        
    }
}

