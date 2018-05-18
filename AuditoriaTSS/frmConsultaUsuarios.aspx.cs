using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace AuditoriaTSS
{
    public partial class frmConsultaUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = "sp_selecciona_usuarios";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 1000000;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    Gvaudit.DataSource = dt;
                    Gvaudit.DataBind();
              
                }
            }
        }
    }
}