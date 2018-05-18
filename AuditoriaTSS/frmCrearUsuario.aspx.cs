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
    public partial class frmCrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        [WebMethod(EnableSession = true)]
        public static string InsertarRegistro(string nombre, string empresa, string contacto, string correo, string idperfi,string usuario,string pass)
        {
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {   SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Exec sp_crear_Usuario '" + nombre + "','" +empresa  +"','" +contacto  +"','" + correo +"','"+ usuario  + "','" + pass + "',"+ idperfi+"";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            string resultado = "Registro actualizado satisfactoriamente!";
            return resultado;
        }
    }
}