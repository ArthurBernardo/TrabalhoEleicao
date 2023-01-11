using System.Collections.Generic;
using TrabalhoEleicao.ViewModels;

namespace TrabalhoEleicao.Services.ResultExecutivoService
{
    public interface IResultExecutivoService
    {
        List<ResultExecutivoViewModel> GetResultadoEleicaoExecutivo();
    }
}