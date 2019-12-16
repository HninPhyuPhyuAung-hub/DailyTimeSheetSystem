using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class Detailjson
    {
        public string[] Revenuetye { get; set; }
        public string[] Rdescription { get; set; }
        public double[] Revenuehour { get; set; }
        public string[] date { get; set; }
        public string[] RJobNo { get; set; }
        public string[] Maintenancetype { get; set; }
        public string[] Mdescription { get; set; }
        public double[] Maintenancehour { get; set; }
     
        public string[] MJobNo { get; set; }

        public string[] Warrantytype { get; set; }
        public string[] Wdescription { get; set; }
        public double[] Warrantyhour { get; set; }

        public string[] WJobNo { get; set; }

        public string[] Expensetype { get; set; }
        public string[] Edescription { get; set; }
        public double[] Expensehour { get; set; }

        public string[] EJobNo { get; set; }
    }
}