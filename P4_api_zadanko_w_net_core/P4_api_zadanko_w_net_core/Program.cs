using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace P4_api_zadanko_w_net_core
{
    
    class Program
    {
        
        
        public static async Task Main(string[] args)
        {
            using var db = new TrenerzyContext();
            db.Database.EnsureCreated();

            var API = new Website(" https://api.collegefootballdata.com");
            var teams = API.DownloadAsync("/teams/fbs").Result.Content;
            var advanced = API.DownloadAsync("/stats/season/advanced?year=2010").Result.Content; 
            //ustawiłem na rok 2010 bo endpoint chcial dostac rok i nie chcial bez niego puscic





            var deserializer = JsonSerializer.Deserialize<Teams[]>(teams, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            //Console.WriteLine(deserializer);

            foreach (var item in deserializer)
            {
                var tmp = new Teams()
                {
                    school = item.school,
                    abbreviation = item.abbreviation,
                    conference = item.conference,
                    AdvancedStats = null
                };
                //Console.WriteLine(item.school);
                db.Add<Teams>(tmp);
            }
            await db.SaveChangesAsync();



           

            //tutaj nie schce zrzucić advanced do klasy, dlaczego?  <<---------------------------- //ok działa
            var deserializer_trenerow =  JsonSerializer.Deserialize<Advanced[]>(advanced, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            

            foreach (var item in deserializer_trenerow)
            {
                var stats = new Advanced
                {
                    //season = item.season,
                    team = item.team,
                    conference = item.conference,
                };
                //Console.WriteLine(item.conference);
                db.Add<Advanced>(stats);
                
                foreach (var item2 in db.teams)
                    if (item.conference == item2.conference) item2.AdvancedStats = stats;
            }
            await db.SaveChangesAsync();
        }
    }
}
