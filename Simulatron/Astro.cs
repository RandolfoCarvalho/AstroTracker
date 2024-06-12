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
                        Diameter = item[11] != null && double.TryParse((string)item[11], out double diameter) ? diameter : 0,
                        Situacao = false
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
    }
}
