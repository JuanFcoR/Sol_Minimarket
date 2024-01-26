using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sol_Minimarket.Entidades;
using Sol_Minimarket.Negocio;

namespace Sol_Minimarket_Presentacion
{
    public partial class Frm_Categorias : Form
    {
        public Frm_Categorias()
        {
            InitializeComponent();
        }
        #region "Mis Variables"
        int EstadoGuarda = 0;

        #endregion

        #region "Mis Metodos"
        private void Formato_ca()
        {
            Dgv_principal.Columns[0].Width = 100;
            Dgv_principal.Columns[0].HeaderText = "CODIGO_CA";
            Dgv_principal.Columns[1].Width = 300;
            Dgv_principal.Columns[1].HeaderText = "CATEGORIA";
        }

        private void Listado_ca(string cTexto)
        {
            try
            {
                Dgv_principal.DataSource = N_Categorias.Listado_ca(cTexto);
                this.Formato_ca();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Estado_BotonesPrincipales(bool lEstado)
        {
            this.Btn_nuevo.Enabled = lEstado;
            this.Btn_actualizar.Enabled = lEstado;
            this.Btn_eliminar.Enabled = lEstado;
            this.Btn_reporte.Enabled = lEstado;
            this.Btn_salir.Enabled = lEstado;
        }

        private void Estado_BotonesProcesos(bool lEstado)
        {
            this.Btn_cancelar.Visible = lEstado;
            this.Btn_guardar.Visible = lEstado;
            this.Btn_Retornar.Visible = !lEstado;
        }
        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.Listado_ca("%");
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            if(Txt_descripcion_ca.Text == String.Empty)
            {
                MessageBox.Show("Falta ingresar datos requeridos (*)", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                E_Categorias oCa = new E_Categorias();
                string Rpta = "";
                oCa.Codigo_ca = 0;
                oCa.Descripcion_ca = Txt_descripcion_ca.Text.Trim();
                Rpta = N_Categorias.Guardar_ca(EstadoGuarda, oCa);
                if(Rpta == "OK")
                {
                    MessageBox.Show("Los datos han sido guardados correctamente", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EstadoGuarda = 0;
                    this.Estado_BotonesPrincipales(true);
                    this.Estado_BotonesProcesos(false);
                    Txt_descripcion_ca.Text = String.Empty;
                    Tbp_principal.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Btn_nuevo_Click(object sender, EventArgs e)
        {
            EstadoGuarda = 1;
            this.Estado_BotonesPrincipales(false);
            this.Estado_BotonesProcesos(true);
            Txt_descripcion_ca.Text = String.Empty;
            Txt_descripcion_ca.ReadOnly = false;
            Tbp_principal.SelectedIndex = 1;
            Txt_descripcion_ca.Focus();

        }

        private void Btn_actualizar_Click(object sender, EventArgs e)
        {
            EstadoGuarda = 2;
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            EstadoGuarda = 0;
            Txt_descripcion_ca.Text = String.Empty;
            Txt_descripcion_ca.ReadOnly = true;
            this.Estado_BotonesPrincipales(true);
            this.Estado_BotonesProcesos(false);
            Tbp_principal.SelectedIndex = 0;
        }
    }
}
