using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Enums
{
    public class Candidato
    {
        public enum TipoCandidato
        {
            [Description("Presidente")]
            Presidente,
            [Description("Governador")]
            Governador,
            [Description("Prefeito")]
            Prefeito,
            [Description("Deputado Estadual")]
            DeputadoEstadual,
            [Description("Deputado Federal")]
            DeputadoFederal,
            [Description("Vereador")]
            Vereador
        }
    }
}
