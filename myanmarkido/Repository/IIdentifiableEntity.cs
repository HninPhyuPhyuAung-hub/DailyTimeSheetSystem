﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myanmarkido.Repository
{
    public interface IIdentifiableEntity
    {
        int EntityId { get; set; }
    }
}