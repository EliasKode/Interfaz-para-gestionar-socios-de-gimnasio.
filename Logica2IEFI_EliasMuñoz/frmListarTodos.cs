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
    public partial class frmListarTodos : Form
    {
        public frmListarTodos()
        {
            InitializeComponent();
        }
        ClassGestionTotal ListarSocios = new ClassGestionTotal();
        string titulo = "Lista de Todos los Socios";

        private void frmListarTodos_Load(object sender, EventArgs e)
        {
            LimpiarTodo();
        }

        private void LimpiarTodo()
        {
            lblMenor.Text = "";
            lblMayor.Text = "";
            lblPromedio.Text = "";
            lblTotal.Text = "";
            GrillaTodos.Rows.Clear();
            btnImprimir.Enabled=false;
            btnExportar.Enabled=false;
        }

        private void AsegurarExportadorImpresion()
        {
            if (GrillaTodos.Rows.Count != 0)
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
            ListarSocios.MostrarTodosLosSocios(GrillaTodos, lblMenor, lblMayor, lblPromedio, lblTotal);
            AsegurarExportadorImpresion();
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            ListarSocios.Exportar(GrillaTodos, lblMenor, lblMayor, lblPromedio, lblTotal, titulo);
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogoImpresora.ShowDialog();
            Documento.PrinterSettings = DialogoImpresora.PrinterSettings;
            Documento.Print();
        }

        private void Documento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ListarSocios.Imprimir(e, GrillaTodos,lblMenor, lblMayor, lblPromedio, lblTotal,titulo);
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
