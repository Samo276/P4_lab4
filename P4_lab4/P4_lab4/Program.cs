using System;
using RestSharp;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P4_lab4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            
            var google = new Website("https://google.pl");
            var ath = new Website("https://ath.bielsko.pl");
            var fb = new Website("https://facebook.com");
            var wiki = new Website("https://en.wikipedia.org");
            var anyapi = new Website("https://anyapi.com");
            var plany= new Website("https://plany.ath.bielsko.pl");
            //Console.WriteLine(google.Content);
            //pobranie i wypisanie strony internetowaej

            //var tasks = new List<Task>();
            var tasks = new List<Task<IRestResponse>>();
            
            stopwatch.Start();
            
            
            //pierwsza opcja
            tasks.Add(google.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(ath.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(fb.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(wiki.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(anyapi.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(plany.DownloadAsync("/plan.php?type=0&id=12647"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(ath.DownloadAsync("/graficzne-formy-przekazu-informacji/"));
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("----------------------------");
           // Console.WriteLine(Task.WhenAny(tasks).Result.Result.Content);//.GetAwaiter().GetResult(); //wskoczy kiedy którykolwiek sie zakończy
            Console.WriteLine(stopwatch.Elapsed);
            var htmlCodes =  Task.WhenAll(tasks).Result;//.GetAwaiter().GetResult();//wskoczy kiedy wszystkie sie zakończa
            foreach(var site in htmlCodes)
            {
                Console.WriteLine(site.Content);
            }
            


            //druga opcja
            /*Console.WriteLine(await google.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await ath.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await fb.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await wiki.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await anyapi.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await plany.DownloadAsync("/plan.php?type=0&id=12647"));
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine(await ath.DownloadAsync("/graficzne-formy-przekazu-informacji/"));
            Console.WriteLine(stopwatch.Elapsed);


            Console.WriteLine("----------------------------");
            Console.WriteLine(stopwatch.Elapsed);*/

            //trzecia opcja
            /*Console.WriteLine( google.DownloadAsync("/").Result); //result robi ot samo co async tutaj
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( ath.DownloadAsync("/").Result);
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( fb.DownloadAsync("/").Result);
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( wiki.DownloadAsync("/wiki/.NET_Core").Result);
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( anyapi.DownloadAsync("/wiki/.NET_Core").Result);
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( plany.DownloadAsync("/plan.php?type=0&id=12647").Result);
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine( ath.DownloadAsync("/graficzne-formy-przekazu-informacji/").Result);
            Console.WriteLine(stopwatch.Elapsed);


            Console.WriteLine("----------------------------");
            Console.WriteLine(stopwatch.Elapsed);
*/

            //stworzyć aplikacje która bedzie mjklorzystała z college football data żeby odpytywała (weźmy drrużyny z fbs)
            //mają być wpisane to bazy danych przy użyciu entity frameworka (używając modelu) code first
            //i dla każdej z drużyn znaleźć jej aktualnego coucha (korzystając z api) po czym wpisać do tabeli w bazie danych
            //zrobić dodatkowy proijekt w tej solucji po prostu

            //request do api -> robimy baze -> request coucha -> dopisujemy coucha do druzyny 
            stopwatch.Stop();
        }
    }
}
