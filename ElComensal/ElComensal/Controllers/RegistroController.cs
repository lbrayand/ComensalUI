namespace ElComensal.Controllers
{
    using ElComensal.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using RestSharp;
    using System.Web.Script.Serialization;
    using Newtonsoft.Json;

    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GrabarCliente(Cliente cliente)
        {
            try
            {
                var urlCliente = ConfigurationManager.AppSettings["ServicioCliente"];
                var client = new RestClient(urlCliente);
                var request = new RestRequest("registrar", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                var jsonCliente = JsonConvert.SerializeObject(cliente);
                request.AddJsonBody(jsonCliente);
                var response = client.Post(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    return Json(new { Result = "NO", Message = "Ha ocurrido un error al intentar grabar" });
                } 
            }
            catch (Exception ex)
            {
                return Json(new { Result = "NO", Message="Ocurrió un error " + ex.Message });
            }

            return Json(new { Result = "OK", Message="Cliente Grabado" });            
        }
    }
}