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
    public partial class FrmLoguin : System.Web.UI.Page
    {

        [System.Web.Services.WebMethod]
        public static String validausuario(string user, string pass)
        {
            string resultado = "";

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select  idrow from  audit_usuarios where usuario='" + user + "' and pass='" + pass + "'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    resultado = dt.Rows[0]["idrow"].ToString();
                }
            }

            return resultado;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}