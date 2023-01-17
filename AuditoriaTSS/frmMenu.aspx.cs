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
        private ClsConexion cscon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string var = (string)Session["valor"];

                String id = Request.QueryString["id"];
                if (var != null)
                {
                    id = var;
                }
                else
                { 
                Session["valor"] = id;
               }
                PermisosMenu(id);
            }
        }
        private void PermisosMenu(string id)
        {
            int permiso = idperfil(id);


            switch (permiso)
            {
                case 0:
                    break;
                case 1:
                    adm.Visible = false;
                    break;
                case 2:
                    adm.Visible = false;
                    proces.Visible = false;
                    break;

            }
        }




     
        private int idperfil(string id)
        {

            DataTable dt = new DataTable();
            int resultado = -1;
            string strconsulta = "Select  perfil from  audit_usuarios where perfil='" + id + "'";
            cscon = new ClsConexion();

            dt = cscon.GetDatatableSql(strconsulta);

                if (dt.Rows.Count > 0)
                {
                    resultado = Convert.ToInt32(dt.Rows[0]["perfil"].ToString());
                }

                return resultado;
            }
     
   
    }
}