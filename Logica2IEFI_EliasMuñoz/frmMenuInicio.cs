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
    public partial class frmMenuInicio : Form
    {
        public frmMenuInicio()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void agregarNuevosSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargaClientes darAlta = new frmCargaClientes();
            darAlta.ShowDialog();
        }

        private void dNIYGestionarInformaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBusquedaModifyBaja BusqModify = new frmBusquedaModifyBaja();
            BusqModify.ShowDialog();
        }

        private void porNombreYVerSuInformaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarNombreVer NombreySoloVer = new frmBuscarNombreVer();
            NombreySoloVer.ShowDialog();
        }

        private void deTodosLosSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarTodos TodosSocios = new frmListarTodos();
            TodosSocios.ShowDialog();
        }

        private void sociosMorososToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarDeudores ListDeudores = new frmListarDeudores();
            ListDeudores.ShowDialog();
        }

        private void sociosPorActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarActividad ListActiv = new frmListarActividad();
            ListActiv.ShowDialog();
        }

        private void sociosPorResidenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarBarrio ListBarrio = new frmListarBarrio();
            ListBarrio.ShowDialog();
        }

        private void delDesarrolladorDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeveloper Perfil = new frmDeveloper();
            Perfil.ShowDialog();
        }

        private void detallesDelProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetalles DetailProduct = new frmDetalles();
            DetailProduct.ShowDialog();
        }
    }
}
