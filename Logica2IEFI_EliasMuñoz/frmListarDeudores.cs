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
    public partial class frmListarDeudores : Form
    {
        public frmListarDeudores()
        {
            InitializeComponent();
        }

        ClassGestionTotal ListarDeudores = new ClassGestionTotal();
        
        string titulo = "Lista de Socios Deudores";
        private void frmListarDeudores_Load(object sender, EventArgs e)
        {
            LimpiarTodo();
        }
        private void LimpiarTodo()
        {
            lblMenor.Text = "";
            lblMayor.Text = "";
            lblPromedio.Text = "";
            lblTotal.Text = "";
            GrillaDeudores.Rows.Clear();
            btnImprimir.Enabled = false;
            btnExportar.Enabled = false;
        }

        private void AsegurarExportadorImpresion()
        {
            if (GrillaDeudores.Rows.Count != 0)
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
        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarDeudores.MostrarSociosDeudores(GrillaDeudores, lblMenor, lblMayor, lblPromedio, lblTotal);
            AsegurarExportadorImpresion();
        }



        private void btnExportar_Click(object sender, EventArgs e)
        {
            ListarDeudores.Exportar(GrillaDeudores, lblMenor, lblMayor, lblPromedio, lblTotal, titulo);
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogoImpresora.ShowDialog();
            Documento.PrinterSettings = DialogoImpresora.PrinterSettings;
            Documento.Print();
        }



        private void Documento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ListarDeudores.Imprimir(e, GrillaDeudores, lblMenor, lblMayor, lblPromedio, lblTotal,titulo);
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
        }



        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
