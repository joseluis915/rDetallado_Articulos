using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;

namespace rDetallado_Articulos.Entidades
{
    public class ArticulosDetalle
    {
        [Key]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string Requerimiento { get; set; }
        public float Valor { get; set; }
        public ArticulosDetalle(int idArticulo, string requerimiento, float valor)
        {
            Id = 0;
            IdArticulo = idArticulo;
            Requerimiento = requerimiento;
            Valor = valor;
        }
    }
}