using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulatron
{
    public class Astro
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Des { get; set; }
        public double DistMax { get; set; }
        public double DistMin { get; set; }
        public string VelRel { get; set; }
        public double VelInfo { get; set; }
        public double LuminIntrinseca {get; set;}
        public double? Diameter { get; set; }
        public bool Situacao {get; set;}
        public static List<Astro> ProcessaAstroInfo(string jsonContent)
        {
            List<Astro> astros = new List<Astro>();
            try
            {
                JObject jsonObject = JObject.Parse(jsonContent);
                JArray dataArray = (JArray)jsonObject["data"];

                foreach (JArray item in dataArray)
                {
                    Astro astro = new Astro
                    {
                        Des = (string)item[0],
                        Diameter = item[12] != null && double.TryParse((string)item[12], out double diameter) ? diameter : null,
                        Situacao = false,
                        VelRel = string.Format("{0:0.00}", (double)item[7]),
                        DistMax = convertAuToKm((double)item[6]),
                        DistMin = convertAuToKm((double)item[5])
                    };
                    // Adicione outras propriedades conforme necessário

                    astros.Add(astro);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erro ao processar o JSON: {ex.Message}");
            }
            return astros;
        }
        public static double convertAuToKm(double value)
        {
            // Conversão de AU para km
            const double auToKm = 149597870.7;
            return value * auToKm;
        }
        public override string ToString()
        {
            return $"Id: {Id} \n Name: {Des} \n Luminosidade intreseca: {LuminIntrinseca} \n VelRel: {VelRel} Km \n VelInfo: {VelInfo} Km \n Distancia Maxima: {DistMax} Km \n Distancia Minima: {DistMin} Km \n Diamater:  {Diameter}  \n " +
            $"Perigoso? {Situacao}  ";
        }
    }
}
