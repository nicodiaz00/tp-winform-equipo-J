using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestor_articulos
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        private string urlPlaceHolder = "https://img.freepik.com/free-vector/illustration-gallery-icon_53876-27002.jpg";
        private void cargarImagen(PictureBox pictureBox,string urlImagen)
        {
            try
            {
                
                pictureBox.Load(urlImagen);
            }
            catch (Exception ex)
            {

                pictureBox.Load(urlPlaceHolder);
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        private void cargarArticulos()
        {
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            try
            {
                listaArticulos = negocioArticulo.listarArticulo();
                dgvArticulos.DataSource = listaArticulos;
                dgvImagenes.DataSource = listaArticulos[0].Imagenes;

                cargarImagen(pbxArticulo, listaArticulos[0].Imagenes[0].UrlImagen);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cargarArticulos();

            //pbxArticulo.Load(listaArticulos[0].Imagenes[0].UrlImagen);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            dgvImagenes.DataSource = artSeleccionado.Imagenes;

                if(artSeleccionado.Imagenes.Count != 0)
            {
                cargarImagen(pbxArticulo, artSeleccionado.Imagenes[0].UrlImagen);
            }
            else
            {
                cargarImagen(pbxArticulo, urlPlaceHolder);
            }      
                
            
            
            //pbxArticulo.Load(artSeleccionado.Imagenes[0].UrlImagen);
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvImagenes.CurrentRow != null)
            {
                Imagenes imagenSeleccionada = (Imagenes)dgvImagenes.CurrentRow.DataBoundItem;
                cargarImagen(pbxArticulo, imagenSeleccionada.UrlImagen);
            }
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo ventanaAltaArticulo = new frmAltaArticulo();
            ventanaAltaArticulo.ShowDialog();
            cargarArticulos();
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Articulo artSeleccionado;
            artSeleccionado = (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
            frmEditarArticulo  ventanaEditar = new frmEditarArticulo(artSeleccionado);
            ventanaEditar.ShowDialog();
            cargarArticulos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo>listaFiltrada = new List<Articulo>();
            string nombreArticulo = txtBusqueda.Text;

            if(nombreArticulo != "")
            {
                foreach (var item in listaArticulos)
                {

                    if (item.Nombre.ToLower().Contains(nombreArticulo.ToLower()))
                    {
                        listaFiltrada.Add(item);
                    }

                }

                if(listaFiltrada.Count > 0)
                {
                    dgvArticulos.DataSource = null;
                    dgvArticulos.DataSource = listaFiltrada;

                    if (listaFiltrada[0].Imagenes.Count > 0)
                    {
                        dgvImagenes.DataSource = null;
                        dgvImagenes.DataSource = listaFiltrada[0].Imagenes;
                        cargarImagen(pbxArticulo, listaFiltrada[0].Imagenes[0].UrlImagen);
                    }
                    else
                    {
                        cargarImagen(pbxArticulo, urlPlaceHolder);
                    }
                }
            }
            else
            {
                listaFiltrada = listaArticulos;
                dgvArticulos.DataSource = null;

                dgvArticulos.DataSource = listaFiltrada;
                dgvImagenes.DataSource = listaFiltrada[0].Imagenes;
                cargarImagen(pbxArticulo, listaFiltrada[0].Imagenes[0].UrlImagen);
            }

        }
    }
}
