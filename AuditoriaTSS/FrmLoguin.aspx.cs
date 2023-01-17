using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.DirectoryServices;
using System.Data.SqlClient;

using System.Data;

namespace AuditoriaTSS
{
    public partial class FrmLoguin : System.Web.UI.Page
    {
       
        [WebMethod]
        public static String validausuario(string user, string pass)
        {
          
            string resultado = "";
            DataTable dt = new DataTable();
            wsServices.AuditoriaWs ws = new wsServices.AuditoriaWs();
            ClsConsulta clcon = new ClsConsulta();

            string Server = "ARSHDCHQ01";
            string ruta = "LDAP://" + Server + "/DC=HUMANO,DC=local";
            System.DirectoryServices.DirectoryEntry raiz = new System.DirectoryServices.DirectoryEntry();
            raiz.Path = ruta;
            raiz.AuthenticationType = AuthenticationTypes.Secure;
            raiz.Username = user;
            raiz.Password = pass;

            string filtro = "sAMAccountName";
            string strSearch = filtro + "=" + user;
            DirectorySearcher dsSystem = new DirectorySearcher(raiz, strSearch);
            dsSystem.SearchScope = SearchScope.Subtree;
            try
            {
                //SearchResult srSystem = dsSystem.FindOne();
                string qrystring = "Select  PERFIL from audit_usuarios where usuario='" + user + "'  and estatus=0";

                dt = clcon.Getusuario(qrystring);
                if (dt.Rows.Count > 0)
                {
                    resultado = dt.Rows[0]["PERFIL"].ToString();
                }
                resultado = "0";
            }
            catch (Exception error)
            {
                resultado = "";
            }

            return resultado;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ClsConsulta var = new ClsConsulta();
                String id = var.GetUserName();

                txtusuario.Value = id;
            }
        }

    }
}