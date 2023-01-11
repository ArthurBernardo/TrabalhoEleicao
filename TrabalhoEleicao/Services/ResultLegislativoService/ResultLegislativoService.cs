using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Entities.Models;

namespace TrabalhoEleicao.Services.ResultLegislativoService
{
    public class ResultLegislativoService : IResultLegislativoService
    {
        private Context db = new Context();
        private readonly IResultLegislativoService resultLegislativoService;
        public ResultLegislativoService(IResultLegislativoService ResultLegislativoService)
        {
            resultLegislativoService = ResultLegislativoService;
        }
        public ResultLegislativoService()
        {

        }

        public List<ViewModels.ResultLegislativoViewModel> GetResultadoEleicaoLegislativo()
        {
            var resultsLegislativo = new List<ViewModels.ResultLegislativoViewModel>();
            var candidatos = db.Candidatos.ToList();
            var candidatosLegislativos = candidatos.Where(x => x.TipoCandidato == Enums.Candidato.TipoCandidato.DeputadoFederal ||
            x.TipoCandidato == Enums.Candidato.TipoCandidato.DeputadoEstadual ||
            x.TipoCandidato == Enums.Candidato.TipoCandidato.Vereador).ToList();

            var resultadoEleicaoLegislativo = GetResultadoEleicao(candidatosLegislativos);
            resultsLegislativo.AddRange(resultadoEleicaoLegislativo);

            return resultsLegislativo;

        }

        private List<ViewModels.ResultLegislativoViewModel> GetResultadoEleicao(List<Candidato> candidatos)
        {
            var resultsLegislativo = new List<ViewModels.ResultLegislativoViewModel>();
         
            var candidatosPorPartidos = candidatos.GroupBy(x => x.Partido);

            foreach (var candidatosPorPartido in candidatosPorPartidos)
            {
                var votosValidos = candidatosPorPartido.Sum(x => x.VotosValidos).Value;
                var mediaQE = Convert.ToDecimal(votosValidos / 5);
                var votosPorPartido = candidatosPorPartido.Sum(x => x.VotosValidos);
                if (votosPorPartido.Value >= 1)
                {
                    foreach (var candidato in candidatosPorPartido)
                    {
                        var votosRecebidosPorCandidato = Convert.ToDecimal(candidato.VotosValidos);
                        if (votosRecebidosPorCandidato >= mediaQE)
                        {
                            var resultLegislativo = new ViewModels.ResultLegislativoViewModel
                            {
                                Id = candidato.Id,
                                Nome = candidato.Nome,
                                TipoCandidato = candidato.TipoCandidato,
                                Partido = candidato.Partido,
                                QtdVotosRecebidos = candidato.VotosValidos.Value,
                                PorcentagemVotosRecebidos = candidato.VotosValidos.Value.ToString("N2") + "%",
                                VotosRecebidosPorcentagemValor = candidato.VotosValidos.Value,
                                CandidatoEleito = true
                            };

                            resultsLegislativo.Add(resultLegislativo);
                        }
                        else
                        {
                            var resultLegislativo = new ViewModels.ResultLegislativoViewModel
                            {
                                Id = candidato.Id,
                                Nome = candidato.Nome,
                                TipoCandidato = candidato.TipoCandidato,
                                Partido = candidato.Partido,
                                QtdVotosRecebidos = candidato.VotosValidos.Value,
                                PorcentagemVotosRecebidos = candidato.VotosValidos.Value.ToString("N2") + "%",
                                VotosRecebidosPorcentagemValor = candidato.VotosValidos.Value,
                                CandidatoEleito = false
                            };

                            resultsLegislativo.Add(resultLegislativo);
                        }
                    }
                }

            }

            return resultsLegislativo;
        }
    }
}