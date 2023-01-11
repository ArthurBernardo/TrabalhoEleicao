using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Entities.Models;

namespace TrabalhoEleicao.Services.ResultExecutivoService
{
    public class ResultExecutivoService : IResultExecutivoService
    {
        private Context db = new Context();
        private readonly IResultExecutivoService resultExecutivoService;
        public ResultExecutivoService(IResultExecutivoService ResultExecutivoService)
        {
            resultExecutivoService = ResultExecutivoService;
        }
        public ResultExecutivoService()
        {

        }

        public List<ViewModels.ResultExecutivoViewModel> GetResultadoEleicaoExecutivo()
        {
            var resultsExecutivos = new List<ViewModels.ResultExecutivoViewModel>();
            var candidatos = db.Candidatos.ToList();
            var candidatosPresidente = candidatos.Where(x => x.TipoCandidato == Enums.Candidato.TipoCandidato.Presidente).ToList();
            var candidatosGovernador = candidatos.Where(x => x.TipoCandidato == Enums.Candidato.TipoCandidato.Governador).ToList();
            var candidatosPrefeito = candidatos.Where(x => x.TipoCandidato == Enums.Candidato.TipoCandidato.Prefeito).ToList();

            var resultadoEleicaoPresidente = GetResultadoEleicao(candidatosPresidente).OrderByDescending(x => x.QtdVotosRecebidos);
            if (resultadoEleicaoPresidente.Any())
            {
                resultadoEleicaoPresidente.First().CandidatoEleito = true;
                resultsExecutivos.AddRange(resultadoEleicaoPresidente);
            }

            var resultadoEleicaoGovernador = GetResultadoEleicao(candidatosGovernador).OrderByDescending(x => x.QtdVotosRecebidos);
            if (resultadoEleicaoGovernador.Any())
            {
                resultadoEleicaoGovernador.First().CandidatoEleito = true;
                resultsExecutivos.AddRange(resultadoEleicaoGovernador);
            }

            var resultadoEleicaoPrefeito = GetResultadoEleicao(candidatosPrefeito).OrderByDescending(x => x.QtdVotosRecebidos);
            if (resultadoEleicaoPrefeito.Any())
            {
                resultadoEleicaoPrefeito.First().CandidatoEleito = true;
                resultsExecutivos.AddRange(resultadoEleicaoPrefeito);
            }

            return resultsExecutivos;

        }

        private List<ViewModels.ResultExecutivoViewModel> GetResultadoEleicao(List<Candidato> candidatos)
        {
            var totalVotos = candidatos.Sum(x => x.VotosValidos).Value;
            var resultsExecutivos = new List<ViewModels.ResultExecutivoViewModel>();
            foreach (var candidato in candidatos)
            {
                if (candidato.VotosValidos != null)
                {
                    var votosRecebidos = Convert.ToDecimal(candidato.VotosValidos.Value);
                    var totalTodosOsVotos = Convert.ToDecimal(totalVotos);
                    var divisao = votosRecebidos / totalTodosOsVotos;
                    var calculoEleicao = divisao * 100;
                    var resultExecutivo = new ViewModels.ResultExecutivoViewModel
                    {
                        Id = candidato.Id,
                        Nome = candidato.Nome,
                        TipoCandidato = candidato.TipoCandidato,
                        Partido = candidato.Partido,
                        QtdVotosRecebidos = candidato.VotosValidos.Value,
                        PorcentagemVotosRecebidos = calculoEleicao.ToString("N2") + "%",
                        VotosRecebidosPorcentagemValor = calculoEleicao,
                        CandidatoEleito = false
                    };

                    resultsExecutivos.Add(resultExecutivo);
                }
            }

            return resultsExecutivos;
        }
    }
}