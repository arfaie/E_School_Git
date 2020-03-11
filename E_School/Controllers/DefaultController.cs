using E_School.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace E_School.Controllers
{
    public class DefaultController : Controller
    {
        SettingRepository blSetting = new SettingRepository();
        SiteContentRepository blContent = new SiteContentRepository();
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.Name = blSetting.Select().FirstOrDefault().schoolName;
            ViewBag.AboutUs = blContent.Select().FirstOrDefault().Content;
            ViewBag.Logo = blContent.Select().FirstOrDefault().Logo;
            ViewBag.Address = blContent.Select().FirstOrDefault().Address;
            //var select = blSetting.Select().FirstOrDefault();
            //var content = blContent.Select().FirstOrDefault();
            //string Name = select.schoolName;
            //ViewBag.Name = Name;
            //ViewBag.Address = content.Address;
            //ViewBag.Tell = content.Tell1;
            //if (content.Tell2 != null)
            //{
            //    ViewBag.Tell2 = content.Tell2;
            //}
            //ViewBag.Logo = content.Logo;
            //ViewBag.Content = content.Content;
            //ViewBag.Img = content.backImg;
            return View();
        }
        public ActionResult Admin()
        {
            var select = blSetting.Select().FirstOrDefault();
            string Name = select.schoolName;
            ViewData["Name"] = Name;
            return View();
        }
    }
}