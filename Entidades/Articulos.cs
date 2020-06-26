using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;
//REGISTRO DETALLADO
using System.ComponentModel.DataAnnotations.Schema;


namespace rDetallado_Articulos.Entidades
{
    public class Articulos
    {
        [Key]
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public double Costo { get; set; }
        public double ValorInventario { get; set; }

        //REGISTRO DETALLADO
        [ForeignKey("IdArticulo")]
        public List<ArticulosDetalle> Detalle { get; set; } = new List<ArticulosDetalle>();
    }
}
