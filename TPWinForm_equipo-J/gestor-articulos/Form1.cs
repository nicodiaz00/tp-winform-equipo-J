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
            catch (Exception)
            {

                pictureBox.Load(urlPlaceHolder);
            }
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns[0].Visible = false;
            dgvImagenes.Columns[0].Visible = false;

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

                if(listaArticulos[0].Imagenes.Count() > 0)
                {
                    cargarImagen(pbxArticulo, listaArticulos[0].Imagenes[0].UrlImagen);
                }
                //cargarImagen(pbxArticulo, listaArticulos[0].Imagenes[0].UrlImagen);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cargarDesplegableCampo()
        {
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");

        }
        private void cargarDesplegableCriterio(string opcion)
        {
            switch (opcion)
            {
                case "Precio":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Mayor a");
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Igual a");
                    break;
                case "Descripcion":    
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Empieza con");
                    cboCriterio.Items.Add("Contiene");
                    cboCriterio.Items.Add("Termina con");
                    break;
                case "Nombre":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Empieza con");
                    cboCriterio.Items.Add("Contiene");
                    cboCriterio.Items.Add("Termina con");
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            cargarArticulos();
            cargarDesplegableCampo();
            ocultarColumnas();
           
            

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
                //cargarImagen(pbxArticulo, listaFiltrada[0].Imagenes[0].UrlImagen);
            }
        }
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcionCampo = cboCampo.Text.ToString();
          
                cargarDesplegableCriterio(opcionCampo);
                btnBusquedaAvanzada.Enabled = true;               
        }
        private void btnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            bool error = true;

            if(cboCampo.SelectedItem == null)
            {
                errorProviderPrincipal.SetError(cboCampo, "Elija un campo");
                error = false;
            }
            else
            {
                errorProviderPrincipal.SetError(cboCampo, "");
            }if(cboCriterio.SelectedItem == null)
            {
                errorProviderPrincipal.SetError(cboCriterio, "Elija un criterio");
                error = false;
            }
            else
            {
                errorProviderPrincipal.SetError(cboCriterio, "");
            }
            if(cboCampo.SelectedItem != null)
            {
                if (cboCampo.SelectedItem.ToString() == "Precio")
                {
                    if(txtBusquedaAvanzada.Text == "")
                    {
                        errorProviderPrincipal.SetError(txtBusquedaAvanzada, "Ingrese un valor");
                        error = false;
                    }
                    else
                    {
                        errorProviderPrincipal.SetError(txtBusquedaAvanzada, "");
                    }
                }
            }
            if (!error)
            {
                return;
            }
            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtBusquedaAvanzada.Text;

                listaFiltrada = articuloNegocio.busquedaAvanzada(campo, criterio, filtro);
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaFiltrada;
                txtBusquedaAvanzada.Text = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ;

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            Articulo articulo = new Articulo();     

            try
            {

                articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                articuloNegocio.eliminarArticulo(articulo.Id);
                imagenNegocio.eliminarImagen(articulo.Id);

                cargarArticulos();
                MessageBox.Show("Articulo Eliminado");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo articuloDetalle = new Articulo();

            articuloDetalle = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            
            frmDetalle frmDetalle = new frmDetalle(articuloDetalle);
            frmDetalle.ShowDialog();
           
        }

        private void txtBusquedaAvanzada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
