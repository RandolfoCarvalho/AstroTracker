using Simulatron;
using System;
using System.Collections.Generic;

namespace Simulatron
{
    public class SolarSystem
    {
        private List<Planet> planets;

        public SolarSystem()
        {
            planets = new List<Planet>();

            // Initialize the planets
            planets.Add(new Planet("Sun", 1.989 * Math.Pow(10, 30), 696340, 0, 0, 0, 0, 0, 0));
            planets.Add(new Planet("Mercury", 3.3011 * Math.Pow(10, 23), 4880, 0.88, 0.387, 0.2056, 7.006, 48.85, 168.63));
            planets.Add(new Planet("Venus", 4.8675 * Math.Pow(10, 24), 6052, 224.7, 1.082, 0.0068, 3.394, 76.67, 4.41));
            planets.Add(new Planet("Earth", 5.9724 * Math.Pow(10, 24), 6371, 365.25, 1.000, 0.0167, 0, 0, 359.99));
            planets.Add(new Planet("Mars", 6.4171 * Math.Pow(10, 23), 3390, 687, 1.524, 0.0934, 1.651, 49.57, 337.94));
            planets.Add(new Planet("Jupiter", 1.8986 * Math.Pow(10, 27), 71492, 4.333, 5.2, 0.0484, 1.305, 2.488, 12.45));
            planets.Add(new Planet("Saturn", 5.6834 * Math.Pow(10, 26), 60271, 10.759, 9.537, 0.0472, 2.488, 0.877, 49.25));
            planets.Add(new Planet("Uranus", 8.6832 * Math.Pow(10, 25), 25559, 29.46, 19.229, 0.0471, 0.772, 1.725, 162.55));
            planets.Add(new Planet("Neptune", 1.0285 * Math.Pow(10, 26), 24764, 164.8, 30.06, 0.0086, 1.769, 0, 44.97));

        }

        public void Update(double time)
        {
            foreach (Planet planet in planets)
            {
                planet.UpdatePosition(time);
            }
        }

        public List<Planet> Planets { get { return planets; } }
    }
}

