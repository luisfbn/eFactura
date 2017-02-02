using System.Web;
using System.Web.Mvc;
using System.IO;

namespace eFactura.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Panel = "panel-default";
            ViewBag.Title = "Inicio";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, HttpPostedFileBase files)
        {
            if (files==null)
            {
                ViewBag.Panel = "panel-default";
                ViewBag.Title = "Inicio";
                return View();
            }

            SAT.Comprobante comprobanteSAT = new SAT.Comprobante();
            eFactura.Comprobante comprobanteOp = new eFactura.Comprobante();

            comprobanteSAT = comprobanteOp.ConvertirArchivoXmlEntidad(files);

            ViewBag.Title = "Inicio";
            ViewBag.ArchivoNombre = new FileInfo(files.FileName).Name.ToString();
            ViewBag.Panel = "panel-success";


            if (comprobanteOp.EsValido)
            {
                return View(comprobanteSAT);
            }
            else
            {
                ViewBag.Panel = "panel-danger";
                ViewBag.InvalidoPor = comprobanteOp.InvalidoPor;
                return View();

            }

        }

        
    }

  
}
