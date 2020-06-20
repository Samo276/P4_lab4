using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace P4_api_zadanko_w_net_core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            

            var API = new Website(" https://api.collegefootballdata.com");
            var teams = API.DownloadAsync("/teams/fbs").Result.Content;
            var _coaches = API.DownloadAsync("/coaches").Result.Content;
            using var db = new TrenerzyContext();
            
            db.Database.EnsureCreated();
            //var task = new List<Task<IRestResponse>>();
            
            var deserializer = JsonSerializer.Deserialize<Teams[]>(teams, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });


            foreach (var item in deserializer)
            {
                var tmp = new Teams()
                {
                        //id = item.id,
                        school =item.school,
                        abbreviation = item.abbreviation,
                        Coach = null,
                 };
                //Console.WriteLine(item.school);
                db.Add<Teams>(tmp);
            }
            db.SaveChanges();


            
            //tutaj nie schce zrzucić trenerow do klasy, dlaczego?
            var deserializer_trenerow =  JsonSerializer.Deserialize<coaches[]>(_coaches, new JsonSerializerOptions() 
            {
                PropertyNameCaseInsensitive = true
            });
            
            

            foreach (var item in deserializer_trenerow)
            {
                var dude = new coaches
                {
                    last_name = item.last_name,
                    first_name = item.first_name,
                    Seasons = item.Seasons
                };
                //Console.WriteLine(dude.last_name);
                db.Add<coaches>(dude);
            }
            db.SaveChanges();

            foreach (var item in db.teams)
                item.Coach = (coaches)db.coaches.Where(x => item.school == x.Seasons.FirstOrDefault().school);               
            
            db.SaveChanges();
            
        }
    }
}
