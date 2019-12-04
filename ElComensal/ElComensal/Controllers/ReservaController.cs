namespace ElComensal.Controllers
{
    using ElComensal.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Serialization.Json;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            llenarCombos();
            return View();
        }

        public ActionResult llenarCombos()
        {
            var soap = new ServiceSOAP.ComensalSOAP();
            var locales = soap.listarLocales();
            var listaLocales = new SelectList(locales, "idLocal", "nombreLocal");
            ViewData["locales"] = listaLocales;

            try
            {
                var url = ConfigurationManager.AppSettings["ServicioMotivo"];
                var client = new RestClient(url);
                var request = new RestRequest("listarMotivos", Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                var response = client.Get(request);
                var jsonDeserialize = new JsonDeserializer();
                List<Motivo> motivos = jsonDeserialize.Deserialize<List<Motivo>>(response);
                var listaMotivos = new SelectList(motivos, "id", "descMotivo");
                ViewData["motivos"] = listaMotivos;

            }
            catch (Exception ex)
            {
                return Json(new { Result = "NO", Message = "Ocurrió un error " + ex.Message });
            }

            return View();

            
        }

        public JsonResult GrabarReserva(Reserva reserva)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["ServicioReserva"];
                var client = new RestClient(url);
                var request = new RestRequest("registrar", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                var jsonCliente = JsonConvert.SerializeObject(reserva);
                request.AddJsonBody(jsonCliente);
                var response = client.Post(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    return Json(new { Result = "NO", Message = "Ha ocurrido un error al intentar grabar" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "NO", Message = "Ocurrió un error " + ex.Message });
            }

            return Json(new { Result = "OK", Message = "Reserva Generada. Favor depositar al número de cuenta: 'xxxx xxxxxxx' " });
        }
    }
}