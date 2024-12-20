using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagenes> listarImagenes()
        {
            List<Imagenes> listaImagenes = new List<Imagenes>();
            AccesoDatos accesoDatosImagen = new AccesoDatos();

            try
            {
                accesoDatosImagen.setearConsulta("Select I.Id, I.ImagenUrl from IMAGENES I");
                accesoDatosImagen.ejecutarLectura();
                while (accesoDatosImagen.Lector.Read())
                {
                    Imagenes Imagen = new Imagenes();
                    Imagen.Id = (int)accesoDatosImagen.Lector["Id"];
                    Imagen.UrlImagen = (string)accesoDatosImagen.Lector["ImagenUrl"];

                    listaImagenes.Add(Imagen);
                }

                return listaImagenes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosImagen.cerrarConexion();
            }
        }
        public List<Imagenes> listarImagenesId(int id)
        {
            List<Imagenes> listaImagen = new List<Imagenes>();
            AccesoDatos accesodatosImagen = new AccesoDatos();

            try
            {
                accesodatosImagen.setearConsulta($"Select I.Id, I.IdArticulo, I.ImagenUrl from IMAGENES I where I.IdArticulo ={id}");
                accesodatosImagen.ejecutarLectura();
                while (accesodatosImagen.Lector.Read())
                {
                    Imagenes Imagen = new Imagenes();
                    Imagen.Id = (int)accesodatosImagen.Lector["Id"];
                    Imagen.IdArticulo = (int)accesodatosImagen.Lector["IdArticulo"];
                    Imagen.UrlImagen = (string)accesodatosImagen.Lector["ImagenUrl"];

                    listaImagen.Add(Imagen);
                }
                return listaImagen;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesodatosImagen.cerrarConexion();
            }
        }
        public void crearImagen(Imagenes imagenNueva)
        {
            AccesoDatos accesoDatosImagen = new AccesoDatos();

            try
            {
                accesoDatosImagen.setearConsulta("insert into IMAGENES(IdArticulo, ImagenUrl) values(" + imagenNueva.IdArticulo + ", '" + imagenNueva.UrlImagen + "');");
                accesoDatosImagen.ejecutarAccion();



            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosImagen.cerrarConexion();
            }
        }

        public void eliminarImagenDeArticulo(int id, int idArt)
        {
            AccesoDatos accesodatos = new AccesoDatos();
            try
            {
                accesodatos.setearConsulta("Delete from IMAGENES where Id = @id and IdArticulo = @idArticulo");
                accesodatos.setearParametro("@id", id);
                accesodatos.setearParametro("@idArticulo", idArt);
                accesodatos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesodatos.cerrarConexion();
            }
        }
        public void eliminarImagen(int idArticulo)
        {
            AccesoDatos accesodatos = new AccesoDatos();
            try
            {
                accesodatos.setearConsulta("Delete from IMAGENES where IdArticulo = @idArticulo");
                accesodatos.setearParametro("@idArticulo", idArticulo);
                accesodatos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesodatos.cerrarConexion();
            }
        }
    }
}
