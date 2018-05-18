using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;


namespace AimgosWeb
{
    public partial class FrmArchivosEspecificos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenargrid();

            }
        }

             private void llenargrid()
        {
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * from Documentos";
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

