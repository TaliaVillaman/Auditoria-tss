using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace AuditoriaTSS
{
    public partial class frmMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String id = Request.QueryString["id"];
                txt_usuario.Text = NombreUsuario(id);
                txtfecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                PermisosMenu(id);
            }
        }
        private void PermisosMenu(string id)
        {
            int permiso = idperfil(id);

            if (menuaudit.Items.Count > 0)
            {
                foreach (MenuItem item in menuaudit.Items)
                {

                    switch (permiso)
                    {
                        case 0:
                            break;
                        case 1:
                            if (item.Text == "Admnistracion de Usuarios")
                            {
                                item.Enabled = false;
                            };
                            break;
                        case 2:
                            if (item.Text == "Admnistracion de Usuarios")
                            {
                                item.Enabled = false;
                            };

                            if (item.Text == "Procesos")
                            {
                                item.Enabled = false;
                            };
                            break;

                    }
                }

            }


        }
        private void DisplayChildMenuText(MenuItem item)
        {


            foreach (MenuItem childItem in item.ChildItems)
            {

                //string a = item.Text;

                if (item.Text == "Admnistracion de Usuarios")
                {
                    item.Enabled = false;
                }

            }

        }
        private int idperfil(string id)
        {

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                int resultado = -1;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select  perfil from  audit_usuarios where idrow='" + id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    resultado = Convert.ToInt32(dt.Rows[0]["perfil"].ToString());
                }

                return resultado;
            }
        }
        private string NombreUsuario(string id)
        {
            string resultado = "";
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select  usuario from  audit_usuarios where idrow='" + id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    resultado = dt.Rows[0]["usuario"].ToString();
                }
                return resultado;
            }
        }
    }
}