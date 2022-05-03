using Kimed.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Kimed.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            InfoViewModel model = new();
            var request = (HttpWebRequest)WebRequest.Create(_configuration.GetValue<string>("UrlApiKimed"));
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return Error();

                        using (StreamReader objReader = new(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return View();
        }

        public ActionResult RegistrarHotel() => View();

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RegistrarHotel(InfoViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Hotel hotel = new Hotel()
        //        {
        //            Id = Guid.NewGuid(),
        //            Nombre = model.Nombre,
        //            Descripcion = model.Descripcion,
        //            CupoMaximo = model.CupoMaximo,
        //            TipoHotelId = model.TipoHotelId,
        //            CiudadId = model.CiudadId,
        //            CreateBy = User.Identity.Name,
        //            CreateTime = DateTime.Now
        //        };

        //        var resultado = await _hotelService.RegistrarHotel(hotel);
        //        if (resultado)
        //            TempData["mensaje"] = $"Se Registro Correctamente el hotel {model.Nombre}";
        //        else
        //            TempData["mensaje"] = $"Ocurrio un problema al tratar de registrar el hotel {model.Nombre}";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
