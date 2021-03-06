﻿namespace Bloggable.Web.Config
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Bloggable.Common.Extensions;
    using Bloggable.Web.Infrastructure.Attributes;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IEnumerable<object> otherFilters = null)
        {
            filters.Add(new PreserveViewDataHandleErrorAttribute());

            otherFilters = otherFilters ?? new object[0];

            otherFilters.ForEach(filters.Add);
        }
    }
}
