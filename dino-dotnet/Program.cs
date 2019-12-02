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

            HttpClient client = new HttpClient();

            List<double> httpTimeList = new List<double>();
            List<double> jsonTimeList = new List<double>();

            for (int i = 0; i < 100; i++)
            {
                DateTime httpDateFirst = DateTime.Now;
                var response = await client.GetStringAsync("https://raw.githubusercontent.com/tsopenteam/gundem/master/gundem.json");
                DateTime httpDateLast = DateTime.Now;

                DateTime jsonDateFirst = DateTime.Now;
                var result = JSON.DeserializeDynamic(response);
                DateTime jsonDateLast = DateTime.Now;

                TimeSpan httpTime = httpDateLast - httpDateFirst;
                TimeSpan jsonTime = jsonDateLast - jsonDateFirst;

                httpTimeList.Add(httpTime.TotalSeconds);
                jsonTimeList.Add(jsonTime.TotalSeconds);

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