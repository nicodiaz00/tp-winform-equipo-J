using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listarCategoria()
        {
            List<Categoria> listaDeCategoria = new List<Categoria>();
            AccesoDatos conexionDatosCategoria = new AccesoDatos();
            try
            {
                conexionDatosCategoria.setearConsulta("Select Id, Descripcion from CATEGORIAS");
                conexionDatosCategoria.ejecutarLectura();
                while (conexionDatosCategoria.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)conexionDatosCategoria.Lector["Id"];
                    categoria.DescripcionCategoria = (string)conexionDatosCategoria.Lector["Descripcion"];
                    listaDeCategoria.Add(categoria);
                }
                return listaDeCategoria;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionDatosCategoria.cerrarConexion();
            }

        }
    }
}
