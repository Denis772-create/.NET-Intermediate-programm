using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Optimization Description:
         * 1. Replaced the previous logic of fetching ImgSources individually by using Take(20).ToList(),
         *    resulting in a single efficient DB query.
         * 2. Utilized LINQ to directly create an IEnumerable<ImageModel> from the fetched ImgSources, eliminating the need for
         *    a separate loop and improving code readability.
         * 3. Encapsulated the usage of the DbContext within a 'using' statement to ensure proper resource disposal and prevent
         *    potential memory leaks.
         */
        public ActionResult Index()
        {
            using (var context = new ProfileSampleEntities())
            {
                var sources = context.ImgSources.Take(20).ToList();

                var model = sources.Select(item => new ImageModel
                {
                    Name = item.Name,
                    Data = item.Data
                }).ToList();

                return View(model);
            }
        }


        public ActionResult Convert()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        byte[] buff = new byte[stream.Length];

                        stream.Read(buff, 0, (int) stream.Length);

                        var entity = new ImgSource()
                        {
                            Name = Path.GetFileName(file),
                            Data = buff,
                        };

                        context.ImgSources.Add(entity);
                        context.SaveChanges();
                    }
                } 
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}