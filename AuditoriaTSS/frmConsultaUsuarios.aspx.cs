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


                DataTable dt = new DataTable();
                ClsConexion cscon = new ClsConexion();
                string strconsulta = "SP_USUARIO_DETALLE";
                cscon = new ClsConexion();
                dt = cscon.GetDatatableSql(strconsulta);

                if (dt.Rows.Count > 0)
                {
                    Gvaudit.DataSource = dt;
                    Gvaudit.DataBind();
                }
                String id = (string)Session["valor"];
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
            ClsConexion cscon = new ClsConexion();

            dt = cscon.GetDatatableSql(strconsulta);

            if (dt.Rows.Count > 0)
            {
                resultado = Convert.ToInt32(dt.Rows[0]["perfil"].ToString());
            }

            return resultado;
        }

        [WebMethod]
        public static string[] infoUsuario(string id)
        {
            DataTable dtValidar;


            ClsConexion con = new ClsConexion();

            string srt = "SELECT USUARIO ,";
                    srt += " PERFIL , ";
                    srt += " ESTATUS";
                    srt += " FROM AUDIT_USUARIOS WHERE USUARIO = '" + id  + "'";
            dtValidar = con.GetDatatableSql(srt);


            string usuario, perfil, estatus ;
            usuario = dtValidar.Rows[0]["USUARIO"].ToString().Trim();
            perfil = dtValidar.Rows[0]["PERFIL"].ToString().Trim();
            estatus = dtValidar.Rows[0]["ESTATUS"].ToString().Trim();

            string resultado = usuario + "*" + perfil + "*" + estatus + "*" ;
            string[] a = resultado.Split('*');
            return a;
        }



        [WebMethod]
        public static string actualiza_datos_usuarios(string usuario, string perfil, string estatus)
        {

            ClsConexion con = new ClsConexion();

            string strqery= "SP_actualiza_PERFIL_USUARIO '" + usuario + "','" + perfil + "','" + estatus + "'";

           con.Insert_update_Data_Sql(strqery);
            string resultado = "Bien!";
            return resultado;

        }


    }
}