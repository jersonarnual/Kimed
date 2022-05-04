using AutoMapper;
using Kimed.Infraestructure.DTO;
using Kimed.UI.Models;
using Kimed.UI.Models.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kimed.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _uri;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration,
                              IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _uri = _configuration.GetValue<string>("UrlApiKimed");
        }

        public async Task<IActionResult> IndexAsync()
        {
            InfoViewModel model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync(_uri);
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync(_uri);
                        var listInfo = JsonSerializer.Deserialize<List<InfoDTO>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model.ListInfo = listInfo;
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
        public async Task<IActionResult> CreateAsync(InfoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    if (model.FileByte != null)
                    {
                        string[] extensions = new string[] { ".jpg", ".jpeg", ".png" };
                        var fileExtension = Path.GetExtension(model?.FileByte?.FileName);
                        if (!extensions.Contains(fileExtension.ToLower()))
                        {
                            TempData["message"] = "extencion del archivo no permitido";
                            return RedirectToAction("Index");
                        }
                        using var ms = new MemoryStream();
                        model.FileByte.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string fileBase = Convert.ToBase64String(fileBytes);
                        model.File = fileBase;
                    }

                    var respuesta = await httpClient.PostAsJsonAsync(_uri, _mapper.Map<InfoDTO>(model));

                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var cuerpo = await respuesta.Content.ReadAsStringAsync();
                        var erroresDelAPI = Util.ExtraerErroresDelWebAPI(cuerpo);
                        List<string> ListError = new();
                        foreach (var campoErrores in erroresDelAPI)
                        {
                            string bodyError = string.Empty;
                            bodyError += $"-{campoErrores.Key}";
                            foreach (var error in campoErrores.Value)
                                bodyError += $"-{error}";
                            ListError.Add(bodyError);
                        }
                        TempData["message"] = ListError.ToString();
                    }
                    TempData["message"] = "Se registro correctamente la info";
              
                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Update(Guid id)
        {
            InfoViewModel model = new();
            using (var httpClient = new HttpClient())
            {
                using var respuesta = await httpClient.GetAsync(_uri);
                if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var response = await respuesta.Content.ReadAsStringAsync();
                    await httpClient.GetStringAsync(_uri);
                    var listInfo = JsonSerializer.Deserialize<List<InfoDTO>>(response,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    model = _mapper.Map<InfoViewModel>(listInfo.Where(x => x.Id.Equals(id)).FirstOrDefault());
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(InfoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    if (model.FileByte != null)
                    {
                        string[] extensions = new string[] { ".jpg", ".jpeg", ".png" };
                        var fileExtension = Path.GetExtension(model?.FileByte?.FileName);
                        if (!extensions.Contains(fileExtension.ToLower()))
                        {
                            TempData["message"] = "extencion del archivo no permitido";
                            return RedirectToAction("Index");
                        }
                        using var ms = new MemoryStream();
                        model.FileByte.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string fileBase = Convert.ToBase64String(fileBytes);
                        model.File = fileBase;
                    }
                    await httpClient.PutAsJsonAsync($"{_uri}/{model.Id}", _mapper.Map<InfoDTO>(model));
                    TempData["message"] = "Se actualizo correctamente la info";
                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await httpClient.DeleteAsync($"{_uri}/{id}");
                    TempData["message"] = "Se Elimino correctamente la info";
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return RedirectToAction("Index");
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
