using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class EmployeePaging
    {
       
            public IPagedList<Account> employeeList { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public string nextLink { get; set; }
            public string prevLink { get; set; }
        }

        public class EmployeeList
        {
            public IEnumerable<Account> employeeList { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public string nextLink { get; set; }
            public string prevLink { get; set; }
        }
    }
