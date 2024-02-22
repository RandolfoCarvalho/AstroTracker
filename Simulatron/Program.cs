using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulatron;
using System;
using System.Net.Sockets;
using System.Text;
class Program
{
    static async Task Main(string[] args)
    {

        //Query in Jet Propulsin Laboratory
        string apiUrl = $"https://ssd-api.jpl.nasa.gov/cad.api?body=all&date-min=2024-01-01&date-max=2024-01-02&dist-max=0.2&diameter=true";
        // Cria uma instância de HttpClient

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Faz a solicitação GET para a API
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
    }
    static List<Astro> ProcessaAstroInfo(string jsonContent)
    {
        JObject jsonObject = JObject.Parse(jsonContent);
        // Acessa o array "data"
        JArray dataArray = (JArray)jsonObject["data"];
        List<Astro> astros = new List<Astro>();
        foreach (JArray dataElement in dataArray)
        {
            Astro astro = new Astro();
            astro.Id = int.Parse((string)dataElement[1]);
            astro.Dist = double.Parse((string)dataElement[4]) * 149597870.7;
            astro.VelRel = double.Parse((string)dataElement[7]) * 3600;
            astro.VelInfo = double.Parse((string)dataElement[8]) * 3600;
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