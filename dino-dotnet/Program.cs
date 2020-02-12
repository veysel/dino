using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jil;
using System.Linq;

namespace dino_dotnet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("start");

            HttpClient httpClient = new HttpClient(new HttpClientHandler()
            {
                UseProxy = false,
                Proxy = null
            });
            
            List<double> httpTimeList = new List<double>();
            List<double> jsonTimeList = new List<double>();

            for (int i = 0; i < 100; i++)
            {
                var httpStopwatch = Stopwatch.StartNew();
                var response = await httpClient.GetStringAsync("https://raw.githubusercontent.com/tsopenteam/gundem/master/gundem.json").ConfigureAwait(false);
                httpStopwatch.Stop();

                var jsonStopwatch = Stopwatch.StartNew();
                var result = JSON.DeserializeDynamic(response);
                jsonStopwatch.Stop();

                httpTimeList.Add(httpStopwatch.Elapsed.TotalSeconds);
                jsonTimeList.Add(jsonStopwatch.Elapsed.TotalSeconds);

                Console.WriteLine($"{i + 1} Http : {httpTimeList[i]}");
                Console.WriteLine($"{i + 1} Json : {jsonTimeList[i]}");
            }

            Console.WriteLine("\nRESULT FOR DOTNET (seconds)");
            Console.WriteLine($"Http First Request Time : {httpTimeList[0]}");
            Console.WriteLine($"Json First Parse Time : {jsonTimeList[0]}");
            Console.WriteLine("\n");
            Console.WriteLine($"Http Average Request Time : {httpTimeList.Average()}");
            Console.WriteLine($"Json Average Parse Time : {jsonTimeList.Average()}");

            Console.WriteLine("end");
        }
    }
}
