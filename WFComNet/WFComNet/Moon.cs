using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFComNet
{
    public class Moon : CelestialBody
    {
        public double MeanAnomaly { get; protected set; }
        public Point3D Position { get; protected set; }
        public List<Point3D> Trajectory { get; set; }
        public Moon(string name, double mass, double radius, double orbitalPeriod, double semiMajorAxis,
                  double eccentricity, double inclination, double longitudeOfAscendingNode, double argumentOfPeriapsis)
        : base(name, mass, radius, orbitalPeriod, semiMajorAxis, eccentricity, inclination, longitudeOfAscendingNode, argumentOfPeriapsis)
        {
            MeanAnomaly = 0;
            Position = new Point3D(0, 0, 0);
            Trajectory = new List<Point3D>();
        }
        public override void UpdatePosition(double time)
        {
            // Calcular a anomalia média
            double meanAnomaly = 2 * Math.PI * time / OrbitalPeriod;

            // Calcular a anomalia verdadeira usando a fórmula de Kepler
            double eccentricity = Eccentricity;
            double trueAnomaly = 2 * Math.Atan2(Math.Sqrt(1 - eccentricity) * Math.Sin(meanAnomaly / 2),
                                                 Math.Sqrt(1 + eccentricity) * Math.Cos(meanAnomaly / 2));

            // Calcular o raio vetor usando a fórmula orbital
            double radiusVector = SemiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Math.Cos(trueAnomaly));

            // Calcular as coordenadas cartesianas da Lua em relação à Terra
            double x = radiusVector * (Math.Cos(LongitudeOfAscendingNode) * Math.Cos(trueAnomaly + ArgumentOfPeriapsis) - Math.Sin(LongitudeOfAscendingNode) * Math.Sin(trueAnomaly + ArgumentOfPeriapsis) * Math.Cos(Inclination));
            double y = radiusVector * (Math.Sin(LongitudeOfAscendingNode) * Math.Cos(trueAnomaly + ArgumentOfPeriapsis) + Math.Cos(LongitudeOfAscendingNode) * Math.Sin(trueAnomaly + ArgumentOfPeriapsis) * Math.Cos(Inclination));
            double z = radiusVector * (Math.Sin(trueAnomaly + ArgumentOfPeriapsis) * Math.Sin(Inclination));

            // Atualizar a posição da Lua
            Position = new Point3D(5, 5,5);
            Trajectory.Add(Position); // Adicionar a posição à trajetória da Lua
        }
    }
}
