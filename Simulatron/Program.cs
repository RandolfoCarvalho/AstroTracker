using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulatron;

namespace Simulatron
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Astro astro = new Astro();
            await astro.ConsultaAsteroides();
            await Earth.ConsultaPosicao();
        }
    }
}

