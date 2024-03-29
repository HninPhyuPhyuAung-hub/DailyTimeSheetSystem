﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Repository
{
    public static class DataExtensions
    { 
     public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
    {
        return query.ToList();
    }

    public static IEnumerable<T> ToFullyLoaded<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.ToList();
    }
}
}
