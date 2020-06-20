using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P4_api_zadanko_w_net_core
{
    public class coaches
    {
        
        int id { get; set; }
        public string first_name { get; set; }
        [Key]
        public string last_name { get; set; }
        //public string school { get; set; }
        public List<Season> Seasons { get; set; }

        /*internal void Add(Coaches item)
        {
            throw new NotImplementedException();
        }*/
    }
}
