using System;

namespace Simulatron
{
    public class Planet
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public double Radius { get; set; }
        public double OrbitalPeriod { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
        public double Inclination { get; set; }
        public double LongitudeOfAscendingNode { get; set; }
        public double MeanAnomaly { get; set; }

        public Planet(string name, double mass, double radius, double orbitalPeriod, double semiMajorAxis, double eccentricity, double inclination, double longitudeOfAscendingNode, double meanAnomaly)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            OrbitalPeriod = orbitalPeriod;
            SemiMajorAxis = semiMajorAxis;
            Eccentricity = eccentricity;
            Inclination = inclination;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            MeanAnomaly = meanAnomaly;
        }

        public void UpdatePosition(double time)
        {
            // Calculate the true anomaly
            double meanMotion = 2 * Math.PI / OrbitalPeriod;
            double trueAnomaly = MeanAnomaly + meanMotion * time;

            // Calculate the eccentric anomaly
            double eccentricAnomaly = Math.Atan2(Math.Sqrt(1 - Eccentricity * Eccentricity) * Math.Sin(trueAnomaly), Eccentricity * Math.Cos(trueAnomaly));

            // Calculate the radius vector
            double radiusVector = SemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Math.Cos(eccentricAnomaly));

            // Calculate the position in polar coordinates
            double x = radiusVector * Math.Cos(LongitudeOfAscendingNode + trueAnomaly);
            double y = radiusVector * Math.Sin(LongitudeOfAscendingNode + trueAnomaly);

            // Convert to Cartesian coordinates
            double posX = x;
            double posY = y * Math.Cos(Inclination);

            // Update the position
            Position = new Point3D(posX, posY, 0);
        }

        public Point3D Position { get; set; }
    }
}
