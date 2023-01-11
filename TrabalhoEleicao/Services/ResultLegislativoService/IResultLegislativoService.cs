using System.Collections.Generic;
using TrabalhoEleicao.ViewModels;

namespace TrabalhoEleicao.Services.ResultLegislativoService
{
    public interface IResultLegislativoService
    {
        List<ResultLegislativoViewModel> GetResultadoEleicaoLegislativo();
    }
}