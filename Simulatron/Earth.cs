using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatron
{
    class Earth
    {
        public static async Task ConsultaPosicao()
        {
            using(HttpClient client = new HttpClient())
            {
                Console.WriteLine("Posicao da terra");
                try
                {
                    Console.WriteLine("Posição da terra: ");
                    string apiUrlEarth = "https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='399'&OBJ_DATA='YES'&MAKE_EPHEM='YES'&EPHEM_TYPE='OBSERVER'&CENTER='500@399'&START_TIME='2006-01-01'&STOP_TIME='2006-01-20'&STEP_SIZE='1%20d'&QUANTITIES='1,9,20,23,24,29'";
                    HttpResponseMessage response = await client.GetAsync(apiUrlEarth);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao fazer a solicitação: {response.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }
    }
}
