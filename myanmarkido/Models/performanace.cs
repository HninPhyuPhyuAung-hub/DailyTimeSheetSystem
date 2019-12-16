using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class performanace
    {
       
        public string Position { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public Double? RAHour { get; set; }

        public Double? ROHour { get; set; }
        public Double? RPHour { get; set; }
        public Double? RTHour { get; set; }
        public Double? RSHour { get; set; }
        public Double? MAHour { get; set; }
        public Double? MOHour { get; set; }

        public Double? MPHour { get; set; }
        public Double? MTHour { get; set; }
        public Double? MSHour { get; set; }

        public Double? WAHour { get; set; }
        public Double? WOHour { get; set; }
        public Double? WPHour { get; set; }
        public Double? WTHour { get; set; }
        public Double? WSHour { get; set; }
        public Double? ECHour { get; set; }
        public Double? EHHour { get; set; }
        public Double? EJHour { get; set; }
        public Double? EKHour { get; set; }
        public Double? ELHour { get; set; }

        public Double? ENHour { get; set; }
        public Double? EPHour { get; set; }
        public Double? EPWHour { get; set; }
        public Double? ERHour { get; set; }
        public Double? ETOHour { get; set; }
        public Double? ETRHour { get; set; }
        public Double? EVHour { get; set; }

        public Double? ETHour { get; set; }

        public Double? Total { get; set; }
    }
}