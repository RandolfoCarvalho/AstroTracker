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
            // Create the solar system
            SolarSystem solarSystem = new SolarSystem();

            // Set the initial time
            double time = 0;

            // Run the simulation
            while (true)
            {
                // Update the positions of the planets
                solarSystem.Update(time);

                // Print the positions of the planets
                foreach (Planet planet in solarSystem.Planets)
                {
                    Console.WriteLine("{0}: ({1:F2}, {2:F2})", planet.Name, planet.Position.X, planet.Position.Y);
                }

                // Increment the time
                time += 0.1;

                // Wait for a key press
                Console.ReadKey();
            }
        }
    }
}

