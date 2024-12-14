using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marcas> listarMarcas()
        {
            List<Marcas> listaMarcas = new List<Marcas>();
            AccesoDatos accesoDatosMarca = new AccesoDatos();

            try
            {
                accesoDatosMarca.setearConsulta("Select M.Id, M.descripcion from MARCAS M");
                accesoDatosMarca.ejecutarLectura();

                while (accesoDatosMarca.Lector.Read())
                {
                    Marcas marca = new Marcas();
                    marca.Id = (int)accesoDatosMarca.Lector["Id"];
                    marca.Descripcion = (string)accesoDatosMarca.Lector["Descripcion"];

                    listaMarcas.Add(marca);
                }
                return listaMarcas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosMarca.cerrarConexion();
            }
        } 
    }
}
