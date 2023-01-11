using System.Collections.Generic;
using TrabalhoEleicao.Entities.Models;

namespace TrabalhoEleicao.Services.PartidoService
{
    public interface IPartidoService
    {
        List<Partido> GetPartidos();
    }
}