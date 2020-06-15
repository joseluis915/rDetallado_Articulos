using System;
using System.Collections.Generic;
using System.Text;
//Añadí estos using
using PrimerParcial_JoseLuis.Entidades;
using Microsoft.EntityFrameworkCore;

namespace PrimerParcial_JoseLuis.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Articulos> Articulos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\MyBaseDeDatos");
        }
    }
}
