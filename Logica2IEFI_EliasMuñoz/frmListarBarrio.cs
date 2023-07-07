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
    public partial class frmListarBarrio : Form
    {
        public frmListarBarrio()
        {
            InitializeComponent();
        }
        ClassGestionTotal ListarSociosBarrio = new ClassGestionTotal();
        string titulo = "Lista de Socios por Barrio";
        string Cual = "";
        private void frmListarBarrio_Load(object sender, EventArgs e)
        {
            LimpiarTodo();
            ListarSociosBarrio.LlenarComboBarrio(cboSelecBarrio);


        }

        private void LimpiarTodo()
        {
            
            lblTotal.Text = "";
            lblCantidadClientes.Text = "";
            lblTotal.Text = "";
            cboSelecBarrio.SelectedItem = 0;
            grillaBarrio.Rows.Clear();
            btnImprimir.Enabled = false;
            btnExportar.Enabled = false;
        }

        private void AsegurarExportadorImpresion()
        {
            if (grillaBarrio.Rows.Count != 0)
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

        private void cboSelecBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 00 && e.KeyChar <= 255)
            {
                e.Handled = true;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {

            ListarSociosBarrio.MostrarSociosDeBarrio(cboSelecBarrio, grillaBarrio, lblCantidadClientes, lblTotal);
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
            Cual = titulo + " " + cboSelecBarrio.Text;
            ListarSociosBarrio.ImprimirBarrio(e, grillaBarrio, lblCantidadClientes, lblTotal, Cual);
        }
        
        
        
        private void btnExportar_Click(object sender, EventArgs e)
        {
            Cual = titulo + " " + cboSelecBarrio.Text;
            ListarSociosBarrio.ExportarBarrio(grillaBarrio, lblCantidadClientes, lblTotal, Cual = titulo + " " + cboSelecBarrio.Text);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
        }



        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSelecBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarTodo();
        }
    }
}
