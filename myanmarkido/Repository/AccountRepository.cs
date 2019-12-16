using myanmarkido.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace myanmarkido.Repository
{
    public class AccountRepository : DataRepositoryBase<Account, dailysheetcontext>
    {
        protected override DbSet<Account> DbSet(dailysheetcontext entityContext)
        {
            return entityContext.Accountset;
        }

        protected override Expression<Func<Account, bool>> IdentifierPredicate(dailysheetcontext entityContext, int id)
        {
            return (e => e.EmpId == id);
        }
    }
}
