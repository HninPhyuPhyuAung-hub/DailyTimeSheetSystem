using myanmarkido.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class dailyjob : IIdentifiableEntity
    {
     
        public int JobId { get; set; }
        public int EmpId { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public DateTime? date { get; set; }
        public string Revenuetype { get; set; }
        public string Rdescription { get; set; }
        public TimeSpan Rhour { get; set; }
        public Double Revenuehour { get; set; }
         public string RJobNo { get; set; }
        public string Maintenancetype { get; set; }
        public TimeSpan Mhour { get; set; }

        public Double Maintenancehour { get; set; }
        public string Mdescription { get; set; }
        public string MJobNo { get; set; }
        public string Warrantytype { get; set; }
        public TimeSpan Whour { get; set; }

        public Double Warrantyhour { get; set; }
        public string Wdescription { get; set; }

        public string WJobNo { get; set; }
        public string Expensestype { get; set; }
        public TimeSpan Ehour { get; set; }
         public Double Expenseshour { get; set; }
      public string Edescritption { get; set; }

        public int EntityId
        {
            get { return JobId; }
            set { value = JobId; }
        }
    }
}