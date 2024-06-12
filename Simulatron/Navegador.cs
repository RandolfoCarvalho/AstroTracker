using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatron
{
    static class Navegador
    {
        public static void ExibirJsonNoNavegador(string jsonContent)
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
