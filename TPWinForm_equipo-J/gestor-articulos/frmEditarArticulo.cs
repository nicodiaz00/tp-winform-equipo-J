using dominio;
using negocio;
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
        private string urlPlaceHolder = "https://img.freepik.com/free-vector/illustration-gallery-icon_53876-27002.jpg";
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
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cboMarca.DataSource = marcaNegocio.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listarCategoria();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "descripcionCategoria";


                txtNombre.Text = articulo1.Nombre;
                txtCodigo.Text = articulo1.CodigoArticulo;
                txtDescripcion.Text = articulo1.Descripcion;
                txtPrecio.Text = articulo1.Precio.ToString();
                cboMarca.SelectedValue = articulo1.Marca.Id;
                cboCategoria.DisplayMember = articulo1.Categoria.DescripcionCategoria;
                
                dgvImagenes.DataSource = articulo1.Imagenes;

                if (articulo1.Imagenes.Count == 0)
                {

                    pbxEditarArticulo.Load(urlPlaceHolder);
                    


                }
                else
                {
                    pbxEditarArticulo.Load(articulo1.Imagenes[0].UrlImagen);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {

        }
    }
}
