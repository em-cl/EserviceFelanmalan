﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HV2020.Eservice.Klient.MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            FelanmalanServiceReference.DataSender klient = new FelanmalanServiceReference.DataSender();
            
            return View();
        }
    }
}