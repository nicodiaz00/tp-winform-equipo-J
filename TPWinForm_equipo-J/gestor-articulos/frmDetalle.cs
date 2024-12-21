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
    public partial class frmDetalle : Form
    {
        private Articulo articulo;
        private string urlPlaceHolder = "https://img.freepik.com/free-vector/illustration-gallery-icon_53876-27002.jpg";
        public frmDetalle()
        {
            InitializeComponent();
        }
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            txtId.Text = articulo.Id.ToString();
            txtNombre.Text =articulo.Nombre.ToString();
            txtCodigo.Text = articulo.CodigoArticulo.ToString();
            txtMarca.Text = articulo.Marca.Descripcion.ToString();
            txtCategoria.Text =articulo.Categoria.DescripcionCategoria.ToString();
            txtPrecio.Text = articulo.Precio.ToString();
            txtDescripcion.Text = articulo.Descripcion.ToString();
            dgvImagenesDetalle.DataSource = articulo.Imagenes;

           if(dgvImagenesDetalle.CurrentRow == null)
            {
                pcbDetalle.Load(urlPlaceHolder);
            }

        }

        private void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvImagenesDetalle_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvImagenesDetalle != null)
            {
                Imagenes imagen = new Imagenes();

                imagen = (Imagenes)dgvImagenesDetalle.CurrentRow.DataBoundItem;

                try
                {
                    pcbDetalle.Load(imagen.UrlImagen);
                }
                catch (Exception)
                {

                    pcbDetalle.Load(urlPlaceHolder);
                }
            }
        }
    }
}
