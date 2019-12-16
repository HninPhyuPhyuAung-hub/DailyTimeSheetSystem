using myanmarkido.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class Account : IIdentifiableEntity
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }

        public Boolean IsDeleted { get; set; }
        public int EntityId
        {
            get { return EmpId; }
            set { value = EmpId; }
        }
    }
}