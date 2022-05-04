using AutoMapper;
using Kimed.Infraestructure.DTO;
using Kimed.Infraestructure.Util;
using Kimed.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Kimed.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly HttpWebRequest _request;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration,
                              IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _request = (HttpWebRequest)WebRequest.Create(_configuration.GetValue<string>("UrlApiKimed"));
        }

        public IActionResult Index()
        {
            InfoViewModel model = new();
            _request.Method = "GET";
            _request.ContentType = "application/json";
            _request.Accept = "application/json";

            try
            {
                using (WebResponse response = _request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return Error();

                        using (StreamReader objReader = new(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InfoViewModel model)
        {

            string data = JsonConvert.SerializeObject(_mapper.Map<InfoDTO>(model));
            string json =$"{{\"data\":\"{data}\"}}";
            _request.Method = "POST";
            
            _request.ContentType = "application/json";
            _request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = _request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return Error();
                        using (StreamReader objReader = new(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
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
