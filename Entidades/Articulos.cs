using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;


namespace PrimerParcial_JoseLuis.Entidades
{
    public class Articulos
    {
        [Key]
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; } 
        public double Costo { get; set; }
        public double ValorInventario { get; set; }
    }
}
