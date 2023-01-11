using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Services.PartidoService;

namespace TrabalhoEleicao.Entities.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }

        //Chamada do services CandidatoService para acessar os metodos publicos pela classe
        private readonly IPartidoService partidoService;
        public Partido(IPartidoService PartidoService)
        {
            partidoService = PartidoService;
        }
        public Partido()
        {

        }

        //Chamada dos metodos
        public List<Partido> GetPartidos()
        {
            return partidoService.GetPartidos();
        }
    }
}