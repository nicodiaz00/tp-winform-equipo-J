using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulo()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos  accesoDatosArticulo = new AccesoDatos();
            List<Imagenes> listaDeImagenes = new List<Imagenes>();
            ImagenNegocio ImagenNegocio = new ImagenNegocio();
            try
            {
                accesoDatosArticulo.setearConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, C.descripcion as Categoria, M.descripcion as Marca, A.Precio  from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id");
                accesoDatosArticulo.ejecutarLectura();
                while (accesoDatosArticulo.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)accesoDatosArticulo.Lector["Id"];
                    articulo.CodigoArticulo = (string)accesoDatosArticulo.Lector["Codigo"];
                    articulo.Nombre = (string)accesoDatosArticulo.Lector["Nombre"];
                    articulo.Descripcion = (string)accesoDatosArticulo.Lector["Descripcion"];
                    articulo.Precio = Math.Round((decimal)accesoDatosArticulo.Lector["Precio"],0);

                    articulo.Marca = new Marcas();
                    articulo.Marca.Id = (int)accesoDatosArticulo.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)accesoDatosArticulo.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)accesoDatosArticulo.Lector["IdCategoria"];
                    articulo.Categoria.DescripcionCategoria = (string)accesoDatosArticulo.Lector["Categoria"];

                    articulo.Imagenes= ImagenNegocio.listarImagenesId(articulo.Id);

                    listaArticulos.Add(articulo);

                }
                return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosArticulo.cerrarConexion();
            }
        }

        public int agregarArticulo(Articulo nuevoArticulo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("Insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES('" + nuevoArticulo.CodigoArticulo + "' , '"+ nuevoArticulo.Nombre +"', '"+ nuevoArticulo.Descripcion +"', " + nuevoArticulo.Marca.Id +","+ nuevoArticulo.Categoria.Id +", "+nuevoArticulo.Precio+")");
                accesoDatos.ejecutarAccion();

                int idArticulo= accesoDatos.obtenerId();

                return idArticulo;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void modificarArticulo (Articulo articuloModificado)
        {
            AccesoDatos accesoDatos= new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("Update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, Precio = @precio where Id = @id");
                accesoDatos.setearParametro("@codigo", articuloModificado.CodigoArticulo);
                accesoDatos.setearParametro("@nombre", articuloModificado.Nombre);
                accesoDatos.setearParametro("@descripcion", articuloModificado.Descripcion);
                accesoDatos.setearParametro("@idMarca", articuloModificado.Marca.Id);
                accesoDatos.setearParametro("@idCategoria", articuloModificado.Categoria.Id);
                accesoDatos.setearParametro("@precio", articuloModificado.Precio);
                accesoDatos.setearParametro("@id", articuloModificado.Id);

                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public List<Articulo> busquedaAvanzada(string campo, string criterio,string filtro)
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            List<Imagenes> listaImagenes = new List<Imagenes>();
            AccesoDatos accesoDatos = new AccesoDatos();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, C.descripcion as Categoria, M.descripcion as Marca, A.Precio  from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id and ";
                

               if(campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        case "Igual a":
                            consulta += "A.Precio = " + filtro;
                            break;
                    }

                }
                else if( campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "A.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "% A.Nombre like '%" + filtro + "%'";
                            break;
                    }

                }
                else
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "A.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "% A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();

                while(accesoDatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)accesoDatos.Lector["Id"];
                    articulo.CodigoArticulo = (string)accesoDatos.Lector["Codigo"];
                    articulo.Nombre = (string)accesoDatos.Lector["Nombre"];
                    articulo.Descripcion = (string)accesoDatos.Lector["Descripcion"];
                    articulo.Precio = Math.Round((decimal)accesoDatos.Lector["Precio"], 0);

                    articulo.Marca = new Marcas();
                    articulo.Marca.Id = (int)accesoDatos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)accesoDatos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)accesoDatos.Lector["IdCategoria"];
                    articulo.Categoria.DescripcionCategoria = (string)accesoDatos.Lector["Categoria"];

                    articulo.Imagenes = imagenNegocio.listarImagenesId(articulo.Id);
                    listaFiltrada.Add(articulo);
                }

                return listaFiltrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
            
        }
        public void eliminarArticulo(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("Delete from ARTICULOS where Id = @id");
                accesoDatos.setearParametro("@id", id);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }
        
    }
}
