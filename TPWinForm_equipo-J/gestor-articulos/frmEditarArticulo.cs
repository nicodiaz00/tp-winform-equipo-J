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
    public partial class frmEditarArticulo : Form
    {
        private Articulo articulo1 = null;
        public frmEditarArticulo()
        {
            InitializeComponent();
        }
        public frmEditarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo1 = articulo;
        }

        private void frmEditarArticulo_Load(object sender, EventArgs e)
        {
            txtNombre.Text = articulo1.Nombre;
            txtCodigo.Text = articulo1.CodigoArticulo;
            txtDescripcion.Text = articulo1.Descripcion;
            txtPrecio.Text = articulo1.Precio.ToString();
            dgvImagenes.DataSource = articulo1.Imagenes;
            pbxEditarArticulo.Load(articulo1.Imagenes[0].UrlImagen);

        }
    }
}
