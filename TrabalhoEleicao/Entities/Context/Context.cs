
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrabalhoEleicao.Entities.Models;

namespace TrabalhoEleicao.Entities.Context
{
    public class Context : DbContext
    {
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
    }
}