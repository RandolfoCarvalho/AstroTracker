using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatron
{
    public class Astro
    {
        public int Id { get; set; }
        public string Des { get; set; }
        public double Dist { get; set; }
        public double VelRel { get; set; }
        public double VelInfo { get; set; }
        public double? Diameter { get; set; }

        public void ConversorDeDadosDoAstro() { 
            {
                Dist = Dist * 149597870.7;
                VelRel = VelRel * 3600;
                VelInfo = VelInfo * 3600;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id} Des: {Des} Dist: {Dist} VelRel: {VelRel} VelInfo: {VelInfo} Diamater {Diameter} ";
        }
    }
}
