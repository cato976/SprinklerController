using SprinklerBO;

namespace SolKineticsIrrigation.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.IO;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using SprinklerBO;
    using SolKineticsIrrigation.Web.ViewModels;

    public class SprinklerController : Controller
    {
        // GET: Sprinkler
        public ActionResult Index()
        {
            ScheduleView sc;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/Sprinkler/GetCurrentStatus");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject json = JObject.Parse(responseText);
                var data = JsonConvert.DeserializeObject<Schedule>(json.ToString());
                sc = new ScheduleView();
                sc.StartDateTime = data.StartDateTime;
            }
            return View(sc);
        }

        // GET: Sprinkler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sprinkler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sprinkler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sprinkler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sprinkler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sprinkler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sprinkler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}