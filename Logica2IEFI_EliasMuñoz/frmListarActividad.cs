using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Logica2IEFI_EliasMuñoz
{
    public partial class frmListarActividad : Form
    {
        public frmListarActividad()
        {
            InitializeComponent();
        }
        ClassGestionTotal ListarSociosActividad = new ClassGestionTotal();
        string titulo = "Lista de Socios por Actividad";
        string Cual = "";
        private void frmListarActividad_Load(object sender, EventArgs e)
        {
            LimpiarTodo();
            ListarSociosActividad.LlenarComboActividad(cboSelecActividad);
        }


        private void LimpiarTodo()
        {
            lblMenor.Text = "";
            lblMayor.Text = "";
            lblPromedio.Text = "";
            lblTotal.Text = "";
            cboSelecActividad.SelectedItem=0;
            GrillaActividad.Rows.Clear();
            btnImprimir.Enabled = false;
            btnExportar.Enabled = false;
        }

        private void AsegurarExportadorImpresion()
        {
            if (GrillaActividad.Rows.Count != 0)
            {
                btnExportar.Enabled = true;
                btnImprimir.Enabled = true;
            }
            else
            {
                btnExportar.Enabled = false;
                btnImprimir.Enabled = false;
            }

        }

        private void cboSelecActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarSociosActividad.MostrarSociosDeActividad(cboSelecActividad, GrillaActividad, lblMenor, lblMayor, lblPromedio, lblTotal);
            AsegurarExportadorImpresion();
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogoImpresora.ShowDialog();
            Documento.PrinterSettings = DialogoImpresora.PrinterSettings;
            Documento.Print();
        }


        private void Documento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Cual = titulo + " " + cboSelecActividad.Text;
            ListarSociosActividad.Imprimir(e, GrillaActividad, lblMenor, lblMayor, lblPromedio, lblTotal,Cual);
        }





        private void btnExportar_Click(object sender, EventArgs e)
        {
            Cual = titulo + " " + cboSelecActividad.Text;
            ListarSociosActividad.Exportar(GrillaActividad, lblMenor, lblMayor, lblPromedio, lblTotal, Cual);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSelecActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarTodo();
        }
    }
}
