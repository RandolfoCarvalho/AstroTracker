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
        public string FullName { get; set; }
        public string Des { get; set; }
        public double Dist { get; set; }
        public double VelRel { get; set; }
        public double VelInfo { get; set; }
        public double LuminIntrinseca {get; set;}
        public double? Diameter { get; set; }
        public bool Situacao {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Des: {Des} Luminosidade intreseca: {LuminIntrinseca} VelRel: {VelRel} VelInfo: {VelInfo} Diamater:  {Diameter}  " + 
            $"Perigoso? {Situacao}  ";
        }
    }
}
