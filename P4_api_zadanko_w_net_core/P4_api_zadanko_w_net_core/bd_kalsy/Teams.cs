using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P4_api_zadanko_w_net_core
{
    public class Teams
    {
        [Key]
        public int id { get; set; }
        public string school { get; set; }
        public string abbreviation { get; set; }
        [ForeignKey("KONFA")]
        public string conference { get; set; }
        public Advanced AdvancedStats { get; set; }
    }
}
