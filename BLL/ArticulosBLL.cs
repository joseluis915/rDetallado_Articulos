using System;
using System.Collections.Generic;
using System.Text;
//Añadí estos using
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using rDetallado_Articulos.DAL;
using rDetallado_Articulos.Entidades;

namespace rDetallado_Articulos.BLL
{
    public class ArticulosBLL
    {
        //=====================================================[ GUARDAR ]=====================================================
        public static bool Guardar(Articulos articulos)
        {
            if (!Existe(articulos.IdArticulo))
                return Insertar(articulos);
            else
                return Modificar(articulos);
        }
        //=====================================================[ INSERTAR ]=====================================================
        private static bool Insertar(Articulos articulos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Articulos.Add(articulos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ MODIFICAR ]=====================================================
        public static bool Modificar(Articulos articulos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                //-------------------------------------------[ REGISTRO DETALLADO ]-------------------------------------------------
                contexto.Database.ExecuteSqlRaw($"Delete FROM ArticulosDetalle Where TareaId={articulos.IdArticulo}");

                foreach (var item in articulos.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                //------------------------------------------------------------------------------------------------------------------

                contexto.Entry(articulos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ ELIMINAR ]=====================================================
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var articulos = contexto.Articulos.Find(id);
                if (articulos != null)
                {
                    contexto.Articulos.Remove(articulos);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ BUSCAR ]=====================================================
        public static Articulos Buscar(int id)
        {
            //-------------------[ REGISTRO DETALLADO ] -------------------
            Articulos articulos = new Articulos();
            //-------------------------------------------------------------
            Contexto contexto = new Contexto();
            //Articulos articulos;
            try
            {
                //-------------------[ REGISTRO DETALLADO ] -------------------
                articulos = contexto.Articulos.Include(x => x.Detalle)
                    .Where(x => x.IdArticulo == id)
                    .SingleOrDefault();
                //-------------------------------------------------------------
                //articulos = contexto.Articulos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return articulos;
        }
        //=====================================================[ GET LIST ]===================================================== 
        public static List<Articulos> GetList(Expression<Func<Articulos, bool>> criterio)
        {
            List<Articulos> lista = new List<Articulos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Articulos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        //=====================================================[ EXISTE ]===================================================== 
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Articulos.Any(d => d.IdArticulo == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }
        //=====================================================[ GET ]=====================================================
        public static List<Articulos> GetArticulos()
        {
            List<Articulos> lista = new List<Articulos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Articulos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
