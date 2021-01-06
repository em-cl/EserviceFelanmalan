﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HV2020.Eservice.Klient.MVC.Models.Emil.DataModels;
using HV2020.Eservice.Klient.MVC.Models.Emil.Utilities;

namespace HV2020.Eservice.Klient.MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            HelperClasses.Keys help = new HelperClasses.Keys();
            return View(help.FetchKeys());
        }
    }
}