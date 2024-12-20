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
using System.Text.RegularExpressions;

namespace gestor_articulos
{
    public partial class frmAltaArticulo : Form
    {

        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public void camposVacios(TextBox texto, ErrorProvider errorProvider)
        {
            bool error = false;

            if (texto.Text == "")
            {
                error = true;

            }
            if (error)
            {
                errorProvider.SetError(texto, "No puede estar vacio");
                btnAceptar.Enabled = false;
            }
            else
            {
                errorProvider.Clear();
                btnAceptar.Enabled=false;
            }
        }
        public void caracteresEspeciales(TextBox texto, ErrorProvider errorProvider)
        {
            string caracteres = "[,.'!#$%&)=?¡*¨\\[\\]:_;,.-]";
            bool error = false;
            if (Regex.IsMatch(texto.Text, caracteres))
            {
                error = true;
                
            }
            if (error)
            {
                errorProviderCaracteres.SetError(texto, "No puede ingresar caracteres especiales ,.'!#$%&)= ... ");
                btnAceptar.Enabled = false;
            }
            else
            {
                errorProviderCaracteres.Clear();
                btnAceptar.Enabled=true;
            }
        }
        public void numerosPositivos(TextBox texto, ErrorProvider errorProvider)
        {
            bool error = false;
            int valor;
            if(texto.Text != ""){
                valor = int.Parse(texto.Text);
                if (valor < 0)
                {
                    error = true;
                }
                if (error)
                {
                    errorProviderNumero.SetError(texto, "Ingrese un valor mayor a CERO");
                }
                else
                {
                    errorProviderNumero.Clear();
                }
            }
            

            
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
                if (!(txtUrlImagen.Text == ""))
                {
                    imagenNueva.UrlImagen = txtUrlImagen.Text;

                    imagenNegocio.crearImagen(imagenNueva);

                }



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
            if(!(txtUrlImagen.Text == ""))
            {
                pbxNuevoArticulo.Load(txtUrlImagen.Text);
            }
            
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            camposVacios(txtCodigo,errorProviderVacio);
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            camposVacios(txtNombre,errorProviderVacio);
            caracteresEspeciales(txtNombre, errorProviderCaracteres);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            camposVacios(txtPrecio,errorProviderVacio);
            numerosPositivos(txtPrecio, errorProviderNumero);
        }
    }
}
