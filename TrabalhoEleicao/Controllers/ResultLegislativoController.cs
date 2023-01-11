using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoEleicao.Services.ResultLegislativoService;

namespace TrabalhoEleicao.Controllers
{
    public class ResultLegislativoController : Controller
    {
        public ActionResult ResultLegislativo()
        {
            ResultLegislativoService resultLegislativoService;
            resultLegislativoService = new ResultLegislativoService(new ResultLegislativoService());
            var resultadoLegislativo = resultLegislativoService.GetResultadoEleicaoLegislativo();
            return View(resultadoLegislativo);
        }
    }
}