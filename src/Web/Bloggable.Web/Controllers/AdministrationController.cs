﻿namespace Bloggable.Web.Controllers
{
    using System.Web.Mvc;

    using Bloggable.Common.Constants;
    using Bloggable.Web.Infrastructure.Attributes.Filters;

    [Authorize(Roles = RoleConstants.Administrator)]
    [AdministrationLog]
    public class AdministrationController : Controller
    {
    }
}