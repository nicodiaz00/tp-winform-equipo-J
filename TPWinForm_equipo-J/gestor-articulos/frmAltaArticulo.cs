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
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboCategoria.DataSource = categoriaNegocio.listarCategoria();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "descripcionCategoria";
                cboMarca.DataSource = marcaNegocio.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
   
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo articuloNuevo = new Articulo();
            Imagenes imagenNueva = new Imagenes();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                articuloNuevo.CodigoArticulo = txtCodigo.Text;
                articuloNuevo.Nombre = txtNombre.Text;
                articuloNuevo.Descripcion = txtDescripcion.Text;
                articuloNuevo.Marca = (Marcas)cboMarca.SelectedItem;
                articuloNuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articuloNuevo.Precio = decimal.Parse(txtPrecio.Text);
                
                int idCreado= articuloNegocio.agregarArticulo(articuloNuevo);

                imagenNueva.IdArticulo = idCreado;
                imagenNueva.UrlImagen = txtUrlImagen.Text;

                imagenNegocio.crearImagen(imagenNueva);

                

                MessageBox.Show("Articulo agregado");
                Close();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            pbxNuevoArticulo.Load(txtUrlImagen.Text);
        }
    }
}
