using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFComNet
{
    public class CelestialBody
    {
        public string Name { get; }
        public double Mass { get; }
        public double Radius { get; }
        public double OrbitalPeriod { get; }
        public double SemiMajorAxis { get; }
        public double Eccentricity { get; }
        public double Inclination { get; }
        public double LongitudeOfAscendingNode { get; }
        public double ArgumentOfPeriapsis { get; }
        public double MeanAnomaly { get; protected set; }
        public Point3D Position { get; protected set; }
        public List<Point3D> Trajectory { get; set; }
        public CelestialBody() { }
        public CelestialBody(string name, double mass, double radius, double orbitalPeriod, double semiMajorAxis,
            double eccentricity, double inclination, double longitudeOfAscendingNode, double argumentOfPeriapsis)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            OrbitalPeriod = orbitalPeriod;
            SemiMajorAxis = semiMajorAxis;
            Eccentricity = eccentricity;
            Inclination = inclination;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            ArgumentOfPeriapsis = argumentOfPeriapsis;
            MeanAnomaly = 0;
            Position = new Point3D(0, 0, 0);
            Trajectory = new List<Point3D>();
        }

        public virtual void UpdatePosition(double time)
        {
            // Calculate the mean anomaly
            double meanMotion = 2 * Math.PI / OrbitalPeriod;
            MeanAnomaly = meanMotion * time;

            // Estimate the eccentric anomaly using Newton's method
            double eccentricAnomaly = MeanAnomaly;
            for (int i = 0; i < 5; i++)
            {
                eccentricAnomaly = MeanAnomaly + Eccentricity * Math.Sin(eccentricAnomaly);
            }

            // Calculate the true anomaly
            double trueAnomaly = 2 * Math.Atan2(Math.Sqrt(1 + Eccentricity) * Math.Sin(eccentricAnomaly / 2),
                                                 Math.Sqrt(1 - Eccentricity) * Math.Cos(eccentricAnomaly / 2));

            // Calculate the radius vector
            double radiusVector = SemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Math.Cos(trueAnomaly));

            // Calculate the position in polar coordinates
            double x = radiusVector * Math.Cos(LongitudeOfAscendingNode + trueAnomaly);
            double y = radiusVector * Math.Sin(LongitudeOfAscendingNode + trueAnomaly);

            // Convert to Cartesian coordinates
            /*double posX = x;
            double posY = y * Math.Cos(Inclination);
            // Update the position
            Position = new Point3D(posX, posY, 0);
            */
            double angle = 2 * Math.PI * time / OrbitalPeriod;
            double newX = SemiMajorAxis * Math.Cos(angle);
            double newY = SemiMajorAxis * Math.Sin(angle);
            double newZ = SemiMajorAxis * Math.Sin(angle * Inclination);
            Position = new Point3D(newX, newY, newZ);
            Trajectory.Add(Position);

        }
    }
}
