﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Saloon.Controllers
{
    public class AppRolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
