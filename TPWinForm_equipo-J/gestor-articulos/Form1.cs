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

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            listaArticulos = negocioArticulo.listarArticulo();
            dgvArticulos.DataSource = listaArticulos;
            dgvImagenes.DataSource = listaArticulos[0].Imagenes;

            cargarImagen(pbxArticulo, listaArticulos[0].Imagenes[0].UrlImagen);

            //pbxArticulo.Load(listaArticulos[0].Imagenes[0].UrlImagen);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            dgvImagenes.DataSource = artSeleccionado.Imagenes;
            
                cargarImagen(pbxArticulo, artSeleccionado.Imagenes[0].UrlImagen);
            
            
            //pbxArticulo.Load(artSeleccionado.Imagenes[0].UrlImagen);
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            Imagenes imagenSeleccionada = (Imagenes)dgvImagenes.CurrentRow.DataBoundItem;
            cargarImagen(pbxArticulo, imagenSeleccionada.UrlImagen);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo ventanaAltaArticulo = new frmAltaArticulo();
            ventanaAltaArticulo.ShowDialog();

            
        }
    }
}
