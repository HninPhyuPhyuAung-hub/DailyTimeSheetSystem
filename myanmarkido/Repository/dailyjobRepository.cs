using myanmarkido.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace myanmarkido.Repository
{
    public class dailyjobRepository : DataRepositoryBase<dailyjob, dailysheetcontext>
    {
        protected override DbSet<dailyjob> DbSet(dailysheetcontext entityContext)
        {
            return entityContext.Dailyjobset;
        }

        protected override Expression<Func<dailyjob, bool>> IdentifierPredicate(dailysheetcontext entityContext, int id)
        {
            return (e => e.JobId == id);
        }
    }
}

