using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Entities.Context;

namespace TrabalhoEleicao.Services.VotosService
{
    public class VotosService : IVotosService
    {
        private Context db = new Context();
        private readonly IVotosService votosService;
        public VotosService(IVotosService VotosService)
        {
            votosService = VotosService;
        }
        public VotosService()
        {

        }

        public void ImportFileVotos(string[] array)
        {
            var candidatos = db.Candidatos.ToList();
            for (int i = 0; i < array.Length; i++)
            {
                var linha = array[i];
                var dadosVotacaoCandidato = linha.Split(';');
                var identificador = int.Parse(dadosVotacaoCandidato[0]);
                var votosValidos = int.Parse(dadosVotacaoCandidato[1]);
                var votosNulos = int.Parse(dadosVotacaoCandidato[2]);
                var votosBrancos = int.Parse(dadosVotacaoCandidato[3]);
                var candidato = candidatos.FirstOrDefault(x => x.Id == identificador);

                if (candidato == null)
                    return;

                candidato.VotosValidos = votosValidos;
                candidato.VotosNulos = votosNulos;
                candidato.VotosBrancos = votosBrancos;

                db.Entry(candidato).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}


