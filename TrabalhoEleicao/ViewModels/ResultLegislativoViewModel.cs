using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoEleicao.ViewModels
{
    public class ResultLegislativoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Enums.Candidato.TipoCandidato TipoCandidato { get; set; }
        public string Partido { get; set; }
        public int QtdVotosRecebidos { get; set; }
        public string PorcentagemVotosRecebidos { get; set; }
        public decimal VotosRecebidosPorcentagemValor { get; set; }
        public bool CandidatoEleito { get; set; }
        public int VagasObtidas { get; set; }
    }
}