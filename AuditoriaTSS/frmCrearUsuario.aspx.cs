using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace AuditoriaTSS
{
    public partial class frmCrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
        public static string InsertarRegistro(string idperfil,string usuario,string pass)
        {
            string resultado = "";

            ClsConexion cscon = new ClsConexion();
            int result = 0;
            string strconsulta = "";
            string Server = "ARSHDCHQ01";
            string ruta = "LDAP://" + Server + "/DC=HUMANO,DC=local";
            System.DirectoryServices.DirectoryEntry raiz = new System.DirectoryServices.DirectoryEntry(ruta);
            raiz.Path = ruta;
            //raiz.AuthenticationType = AuthenticationTypes.Secure;
            //raiz.Username = usuario;
            string filtro = "sAMAccountName";
            string strSearch = filtro + "=" + usuario;
            DirectorySearcher dsSystem = new DirectorySearcher(raiz, strSearch);
            dsSystem.SearchScope = SearchScope.Subtree;

            try
            {
               // SearchResult srSystem = dsSystem.FindOne();
                strconsulta = "SP_CREA_PERFIL_USUARIO '" + usuario + "','" + idperfil + "','" + pass + "' ";
                result = cscon.Insert_update_Data_Sql(strconsulta);
                resultado = "Registro Insertados satisfactoriamente!";
            }
            catch (Exception error)
            {
                resultado = "";
            }
            return resultado;
        }
    }
}