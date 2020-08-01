using Microsoft.EntityFrameworkCore.Storage;
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

        public static Website API = new Website("https://api.collegefootballdata.com");
        public static async Task Main()
        {
            
            using var db = new TrenerzyContext();
            db.Database.EnsureCreated();

            var teams = await _getTeams();
            
            var deserializer = JsonSerializer.Deserialize<Teams[]>(teams, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            

            foreach (var item in deserializer)
            {
                db.teams.Add(await _addTeam(item));
                db.SaveChanges();
            };

            

            Console.WriteLine("\n\n-----------------KONIEC-------------------\n\n\n");


        }

        public static async Task<Teams> _addTeam(Teams item)
        {
            
            var ekipa = new Teams
            {
                abbreviation = item.abbreviation,
                school = item.school,
                conference = item.conference,
                team = await _getAdvanced(item.conference)
            };
            
            return ekipa;
        }
       

        public static async Task<string> _getTeams()
        {
            return API.Download("/teams/fbs");
        }
        public static async Task<string> _getAdvanced(string _nazwaConfy)
        {
            List<Advanced> lista = new List<Advanced>();

            var advanced = await _downloadAdvanced();
            var deserializer_advanced = JsonSerializer.Deserialize<Advanced[]>(advanced, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            foreach (var item in deserializer_advanced)
            {
                lista.Add(new Advanced
                {
                    team = item.team,
                    conference = item.conference,
                });
            }
            return await _searchThrough(_nazwaConfy, lista);
        }

        public static async Task<string> _searchThrough(string _Confa, List<Advanced> lista)
        {
            return lista.Where(x => x.conference == _Confa).Select(x => x.team).FirstOrDefault();
        }

        public static async Task<string> _downloadAdvanced()
        {
            return API.Download("/stats/season/advanced?year=2010");
        }
    }
}
