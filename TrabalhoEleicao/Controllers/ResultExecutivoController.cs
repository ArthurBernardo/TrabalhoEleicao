using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Services.ResultExecutivoService;
using TrabalhoEleicao.ViewModels;

namespace TrabalhoEleicao.Controllers
{
    public class ResultExecutivoController : Controller
    {
        public ActionResult ResultExecutivo()
        {
            ResultExecutivoService resultExecutivoService;
            resultExecutivoService = new ResultExecutivoService(new ResultExecutivoService());
            var resultadoExecutivo = resultExecutivoService.GetResultadoEleicaoExecutivo();
            return View(resultadoExecutivo);
        }
    }
}