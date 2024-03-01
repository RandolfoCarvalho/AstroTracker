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
                    List<Astro> astros = ProcessaAstroInfo(jsonContent);
                    string json = JsonConvert.SerializeObject(astros, Formatting.Indented);
                    ExibirJsonNoNavegador(json);
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
        try
        {
            string apiUrlEarth = "https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='399'&OBJ_DATA='YES'&MAKE_EPHEM='YES'&EPHEM_TYPE='OBSERVER'&CENTER='500@10'&START_TIME='2024-02-19'&STOP_TIME='2024-02-20'&STEP_SIZE='1%20d'&QUANTITIES='1,9,20,23,24,29'";
            using (HttpClient client = new HttpClient())
            {
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
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro: {e.Message}");
        }
        static List<Astro> ProcessaAstroInfo(string jsonContent)
        {
            JObject jsonObject = JObject.Parse(jsonContent);
            JArray dataArray = (JArray)jsonObject["data"];
            List<Astro> astros = new List<Astro>();
            foreach (JArray dataElement in dataArray)
            {
                Astro astro = new Astro();
                astro.Id = int.Parse((string)dataElement[1]);
                astro.Dist = double.Parse((string)dataElement[4]);
                astro.VelRel = double.Parse((string)dataElement[7]);
                astro.VelInfo = double.Parse((string)dataElement[8]);
                astro.Diameter = dataElement[12] != null ? null : double.Parse((string)dataElement[12]);
                astros.Add(astro);
                Console.WriteLine();
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
