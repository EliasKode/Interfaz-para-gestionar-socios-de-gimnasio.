using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Logica2IEFI_EliasMuñoz
{
    public partial class frmBusquedaModifyBaja : Form
    {
        public frmBusquedaModifyBaja()
        {
            InitializeComponent();
        }
        ClassGestionTotal VerModificar = new ClassGestionTotal();
        private void frmBusquedaModifyBaja_Load(object sender, EventArgs e)
        {
            Limpiar();
            VerModificar.LlenarComboBarrio(cboBarrio);
            VerModificar.LlenarComboActividad(cboActividad);
        }

        private void Limpiar()
        {
            txtIngresarDNI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            cboBarrio.SelectedItem =0;
            cboActividad.SelectedItem=0;
           
            txtSaldo.Text = "";
            btnBuscar.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void HabilitarEdicionYBaja()
        {
            if (txtIngresarDNI.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && cboBarrio.SelectedIndex != -1 && cboActividad.SelectedIndex != -1)
            {
                btnModificar.Enabled = true;
                btnBorrar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnBorrar.Enabled = false;
            }
        }


        //proteccion de caja de busqueda
        private void txtIngresarDNI_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
            if(txtIngresarDNI.Text !="")
            {
                btnBuscar.Enabled=true;
            }
            else
            {
                btnBuscar.Enabled=false;
                Limpiar();
            }

        }

        private void txtIngresarDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }


        //Busqueda
        private void btnBuscar_Click(object sender, EventArgs e)
        {
       
            VerModificar.MostrarParaEditar(int.Parse(txtIngresarDNI.Text), txtNombre, txtApellido, txtDireccion, cboBarrio, cboActividad, txtSaldo);

        }


        //proteccion de caja Nombre

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;

            }

        }

        //proteccion caja de apellido
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        //proteccion caja de direccion
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        //proteccion de combos
        private void cboBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        private void cboActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        //proteccion caja de saldo
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }

        private void cboBarrio_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }

        private void cboActividad_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }
        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            HabilitarEdicionYBaja();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ud está por modificar los datos de un socio ¿está seguro de querer realizar los cambios?",
                      "Atención!!!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                          DialogResult.Yes)
            {
                VerModificar.Modificar(int.Parse(txtIngresarDNI.Text), txtNombre, txtApellido, txtDireccion, cboBarrio, cboActividad,txtSaldo);

                
                Limpiar();
            }
            else
            {
                Limpiar();
              
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ud está por borrar los datos de un socio y todos sus datos ¿está seguro de querer realizar la baja?",
                    "Atención!!!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.Yes)
            {
                VerModificar.BajaSocio(int.Parse(txtIngresarDNI.Text));


                Limpiar();
            }
            else
            {
                Limpiar();

            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
