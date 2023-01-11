using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoEleicao.ViewModels
{
    public class ImportArciveViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public Enums.Candidato.TipoCandidato TipoCandidato { get; set; }
        public string Partido { get; set; }
        public int? VotosValidos { get; set; }
        public int? VotosNulos { get; set; }
        public int? VotosBrancos { get; set; }
    }
}