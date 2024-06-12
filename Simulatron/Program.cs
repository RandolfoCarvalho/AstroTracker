using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulatron;

class Program
{
    static async Task Main(string[] args)
    {
        //SBDB Close-Approach Data API
        string apiUrl = $"https://ssd-api.jpl.nasa.gov/cad.api?body=all&date-min=2024-01-03&date-max=2024-01-04&dist-max=0.2&diameter=true";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                // Verifica se a solicitação foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Lê o conteúdo da resposta como uma string JSON
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonContent);
                    List<Astro> astros = Astro.ProcessaAstroInfo(jsonContent);

                    foreach (Astro astro in astros)
                    {
                        Console.WriteLine(astro.ToString());
                    }
                    Navegador.ExibirJsonNoNavegador(jsonContent);
                }
                else
                {
                    Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}