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
    public partial class frmCargaClientes : Form
    {
        public frmCargaClientes()
        {
            InitializeComponent();
        }

        //objeto de la clase que controla funcionalidad
        protected ClassGestionTotal DarAlta = new ClassGestionTotal();
        
        private void frmCargaClientes_Load(object sender, EventArgs e)
        {
            Inicializar();
            DarAlta.LlenarComboBarrio(cboBarrio);
            DarAlta.LlenarComboActividad(cboActividad);
        }


        //metodo de limpieza de interfaz
        private void Inicializar()
        {
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtSaldo.Text = "";
            cboBarrio.SelectedIndex = -1;
            cboActividad.SelectedIndex = -1;
            btnCargar.Enabled = false;

        }

        //Proteccion de Caja DNI
        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex !=-1 && cboActividad.SelectedIndex !=-1)
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled=false;
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }

        //Proteccion de Caja Nombre
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex != -1 && cboActividad.SelectedIndex != -1)
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled = false;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }


        //Proteccion de Caja Apellido
        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex != -1 && cboActividad.SelectedIndex != -1)
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled = false;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }


        //Proteccion de Caja Direccion
        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex != -1 && cboActividad.SelectedIndex != -1)
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled = false;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        //proteccion de combo barrio
        private void cboBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        //Proteccion de combo Actividad

        private void cboActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        //Proteccion de Caja Saldo
        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex != -1 && cboActividad.SelectedIndex != -1)
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled = false;
            }
        }

        //proteeccion caja de saldo
        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && txtSaldo.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        //carga de clientes
        private void btnCargar_Click(object sender, EventArgs e)
        {
            DarAlta.CargarSocio(int.Parse(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtDireccion.Text, 
                int.Parse(cboBarrio.SelectedValue.ToString()), int.Parse(cboActividad.SelectedValue.ToString()),Convert.ToDecimal( txtSaldo.Text));
            Inicializar();
           
        }

        //limpiar interfaz
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
