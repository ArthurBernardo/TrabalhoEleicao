using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Entities.Context;

namespace TrabalhoEleicao.Services.PartidoService
{
    public class PartidoService : IPartidoService
    {
        private Context db = new Context();
        public List<Entities.Models.Partido> GetPartidos()
        {
            var partidos = db.Partidos.ToList();
            return partidos;
        }
    }
}