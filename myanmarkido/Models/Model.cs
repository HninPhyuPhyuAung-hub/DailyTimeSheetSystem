using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class Model<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public T[] Results { get; set; }
    }
}