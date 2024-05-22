using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulatron;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = $"https://ssd-api.jpl.nasa.gov/cad.api?body=all&date-min=2024-01-01&date-max=2024-01-02&dist-max=0.2&diameter=true";

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
                    List<Astro> astros = ProcessaAstroInfo(jsonContent);

                    foreach (Astro astro in astros)
                    {
                        Console.WriteLine($"Designação: {astro.Des}, Diameter: {astro.Diameter}, Situacao: {astro.Situacao}");
                    }
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
    static List<Astro> ProcessaAstroInfo(string jsonContent)
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