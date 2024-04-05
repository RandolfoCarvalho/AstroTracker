using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulatron;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = $"https://ssd-api.jpl.nasa.gov/cad.api?body=all&date-min=2024-01-01&date-max=2024-01-02&dist-max=0.2&diameter=true";
        // Cria uma instância de HttpClient

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
                    List<Astro> astro = new List<Astro>();
                    astro = ProcessaAstroInfo(jsonContent);
                    foreach(Astro astrin in astro) {
                        Console.WriteLine(astrin);
                    }
                    //ExibirJsonNoNavegador(jsonContent);
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
       static List<Astro> ProcessaAstroInfo(string jsonContent)
        {
            JObject jsonObject = JObject.Parse(jsonContent);
            JObject nearEarthObjects = (JObject)jsonObject["near_earth_objects"];
            JArray jsonArray = (JArray)nearEarthObjects.First.First;

            List<Astro> astros = new List<Astro>();
            foreach (JObject item in jsonArray)
            {
                Astro astro = new Astro();
                astro.Id = int.Parse((string)item["id"]);
                astro.LuminIntrinseca = double.Parse((string)item["absolute_magnitude_h"]);
                astro.Diameter = double.Parse((string)item["estimated_diameter"]["meters"]["estimated_diameter_min"]);
                astro.Situacao = bool.Parse((string)item["is_potentially_hazardous_asteroid"]);
                // Você precisa verificar se os campos "relative_velocity" e "miss_distance" existem para cada objeto e, em seguida, acessar seus valores conforme necessário.
                // astro.VelRel = double.Parse((string)item["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"]);
                // astro.VelInfo = double.Parse((string)item["close_approach_data"][0]["miss_distance"]["kilometers"]);
                astros.Add(astro);
            }
            return astros;
        }
        static void ExibirJsonNoNavegador(string jsonContent)
        {
            // Cria uma página HTML que exibe o JSON como um objeto JSON formatado
            string htmlContent = $@"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>JSON Viewer</title>
            <style>
                pre {{
                    white-space: pre-wrap;
                    font-family: monospace;
                }}
            </style>
        </head>
        <body>
        <p> Data de acordo com o intervalo de tempo escolhido </p>
            <pre id=""json-data""></pre>
            <script>
                // Função para exibir o JSON como objeto JSON no DOM
                function exibirJson(jsonContent) {{
                    var jsonData = JSON.parse(jsonContent);
                    var formattedJson = JSON.stringify(jsonData, null, 2);
                    document.getElementById('json-data').textContent = formattedJson;
                }}

                // Chamada da função para exibir o JSON passado pelo C#
                exibirJson(`{jsonContent}`);
            </script>
        </body>
        </html>";

            // Salva a página HTML em um arquivo temporário
            string tempHtmlFilePath = System.IO.Path.GetTempFileName().Replace(".tmp", ".html");
            System.IO.File.WriteAllText(tempHtmlFilePath, htmlContent);

            // Abre a página HTML no navegador padrão
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = tempHtmlFilePath,
                UseShellExecute = true
            });
        }
    }
}
