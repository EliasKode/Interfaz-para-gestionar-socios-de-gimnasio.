using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica2IEFI_EliasMuñoz
{
    public partial class frmBuscarNombreVer : Form
    {
        public frmBuscarNombreVer()
        {
            InitializeComponent();
        }

        ClassGestionTotal MostrarEtiquetas = new ClassGestionTotal();
        private void frmBuscarNombreVer_Load(object sender, EventArgs e)
        {
            Inicializar();
            MostrarEtiquetas.LlenarComboSocios(cboNombreSocio);
        }

        private void Inicializar()
        {
            lblDNI.Text = "";
            lblDireccion.Text = "";
            lblSaldo.Text = "";
            lblBarrio.Text= "";
            lblActividad.Text= "";
            cboNombreSocio.SelectedItem =0 ;
        }

        //proteccion del combo de socios
        private void cboNombreSocio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        private void btnMostar_Click(object sender, EventArgs e)
        {
            if(cboNombreSocio.SelectedIndex >=0)
            {
            MostrarEtiquetas.CargarEtiquetas(int.Parse(cboNombreSocio.SelectedValue.ToString()), lblDNI, lblDireccion, lblBarrio, lblActividad, lblSaldo);

            }
            else
            {
                MessageBox.Show("Seleccione un nombre en la lista de socios", "Atención!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
