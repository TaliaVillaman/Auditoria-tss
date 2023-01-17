
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;

using System.Web.Services;
using Oracle.Web;

using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml;

namespace AuditoriaTSS
{
    public partial class frmRepote : System.Web.UI.Page
    {
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
        ClsConsulta clcon;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                String id = (string)Session["valor"];
                PermisosMenu(id);

            }

        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            Gvaudit.Visible = true;
            gvdetalle.Visible = true;
            DataTable dt = new DataTable();
            DateTime desde;
            DateTime hasta;
            DateTime.TryParse(dtfechainicial.Value, out desde);
            DateTime.TryParse(dtfechafinal.Value, out hasta);
            string sdesde = desde.ToString("yyyy-MM-dd");
            string shasta = hasta.ToString("yyyy-MM-dd");
            clcon = new ClsConsulta();
            Gvaudit.DataSource = null;
            Gvaudit.DataBind();
            gvdetalle.DataSource = null;
            gvdetalle.DataBind();
            if (txtaditoria.Value == string.Empty)
            {
                dt = clcon.GetReporteAudit(0, sdesde, shasta);
                if (dt.Rows.Count > 0)
                {
                    Gvaudit.DataSource = dt;
                    Gvaudit.DataBind();
                    gvdetalle.Visible = false;
                }
                else
                {
                    Type cstype = this.GetType();
                    string script = "<script type=text/javascript>mesaggeinfo('No Existen Datos para esta Consulta');</script>";
                    ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                    gvdetalle.Visible = false;
                    Gvaudit.Visible = false;
                    LimpiaCampos();
                    return;
           
                }
            }
            else
            {
                dt = clcon.getDataDocsFinales(Convert.ToInt32(txtaditoria.Value));
                if (dt.Rows.Count > 0)
                {
                    gvdetalle.DataSource = dt;
                    gvdetalle.DataBind();
                    Gvaudit.Visible = false;
                }
                else
                {

                    Type cstype = this.GetType();
                    string script = "<script type=text/javascript>mesaggeinfo('No Existen Datos para esta Consulta');</script>";
                    ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                    
                    gvdetalle.Visible = false;
                    Gvaudit.Visible = false;
                    LimpiaCampos();
                    return;
                }
            }

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
         
            DateTime desde;
            DateTime hasta;
            Stream stream;
            DataTable dataDocsFinales = new DataTable("Data");
            DateTime.TryParse(dtfechainicial.Value, out desde);
            DateTime.TryParse(dtfechafinal.Value, out hasta);
            string sdesde = desde.ToString("yyyy-MM-dd");
            string shasta = hasta.ToString("yyyy-MM-dd");
            clcon = new ClsConsulta();
            if (txtaditoria.Value == string.Empty)
            {
                dataDocsFinales = clcon.GetReporteAudit(0, sdesde, shasta).DefaultView.ToTable();
            }
            else
            {
                dataDocsFinales =  clcon.getDataDocsFinales(Convert.ToInt32(txtaditoria.Value));
            }
            string ruta = System.Configuration.ConfigurationManager.AppSettings["RepReporteria"].ToString();
            string rutaimagen = System.Configuration.ConfigurationManager.AppSettings["imgHumano"].ToString();

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            if (txtaditoria.Value != string.Empty)
            {
                 stream = File.Create(ruta + "Auditoria_" + Convert.ToInt32(txtaditoria.Value) + "_" + DateTime.Now.ToString("yyyyMMdd") + "_.xls");
            }
            else
            {
                 stream = File.Create(ruta + "Reporte_Auditoriaa_usuarios.xls");
            }
            if (dataDocsFinales.Rows.Count > 0)
            {
                using (ExcelPackage pck = new ExcelPackage(stream))
                {
                    System.Drawing.Image logo = System.Drawing.Image.FromFile(rutaimagen + "logo_ARSH.png");
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Datos");
                    ws.Drawings.AddPicture(rutaimagen + "logo_ARSH.png", logo);
                    ws.Cells["A6"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    ws.Cells["A6"].Style.Font.Bold = true;
                    ws.Cells["A6"].Style.Font.Name = "Tahoma";
                    ws.Cells["A8"].LoadFromDataTable(dataDocsFinales, true, OfficeOpenXml.Table.TableStyles.Light1);
                    ws.Cells["A6"].Value = "Reporte Validación de Documentos";
                    ws.Cells["A6"].Style.Font.Size = 13;
                    ws.Cells["A6:H6"].Merge = true;

                    pck.Save();
                    pck.Dispose();
                    ws.Dispose();
                    stream.Dispose();



                }

            }
            Type cstype = this.GetType();
            string script = "<script type=text/javascript>mesaggeExito('Consulta generada Sactisfactoriamente!!');</script>";
            ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
            Gvaudit.DataSource = null;
            Gvaudit.DataBind();
            gvdetalle.DataSource = null;
            gvdetalle.DataBind();
            LimpiaCampos();
        }

        protected void Gvaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvaudit.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            DateTime desde;
            DateTime hasta;
            int no_auditoria = 0;
             clcon = new ClsConsulta();
            DateTime.TryParse(dtfechainicial.Value, out desde);
            DateTime.TryParse(dtfechafinal.Value, out hasta);
            string sdesde = desde.ToString("yyyy-MM-dd");
            string shasta = hasta.ToString("yyyy-MM-dd");

            if (txtaditoria.Value != string.Empty)
            {
                no_auditoria = Convert.ToInt32(txtaditoria.Value);
            }
            dt = clcon.GetReporteAudit(no_auditoria, sdesde, shasta);
            Gvaudit.DataSource = dt;
            Gvaudit.DataBind();
        }

        protected void Gvaudit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = Gvaudit.Rows[index];
                DateTime desde;
                DateTime hasta;
                string Valor = row.Cells[0].Text;
                DataTable dataDocsFinales = new DataTable();
                DateTime.TryParse(dtfechainicial.Value, out desde);
                DateTime.TryParse(dtfechafinal.Value, out hasta);
                string sdesde = desde.ToString("yyyy-MM-dd");
                string shasta = hasta.ToString("yyyy-MM-dd");
                clcon = new ClsConsulta();

                if (Valor == string.Empty)
                {
                    dataDocsFinales = clcon.GetReporteAudit(0, sdesde, shasta).DefaultView.ToTable();
                }
                else
                {
                    dataDocsFinales = clcon.getDataDocsFinales(Convert.ToInt32(Valor));
                }
                string ruta = System.Configuration.ConfigurationManager.AppSettings["RepReporteria"].ToString();
                string rutaimagen = System.Configuration.ConfigurationManager.AppSettings["imgHumano"].ToString();
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                Stream stream = File.Create(ruta + "Auditoria_" + Valor + "_"+ DateTime.Now.ToString("yyyyMMdd")+"_.xls");
                if (dataDocsFinales.Rows.Count > 0)
                {
                    using (ExcelPackage pck = new ExcelPackage(stream))
                    {
                        System.Drawing.Image logo = System.Drawing.Image.FromFile(rutaimagen + "logo_ARSH.png");
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Datos");
                        ws.Drawings.AddPicture(rutaimagen + "logo_ARSH.png", logo);
                        ws.Cells["A6"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                        ws.Cells["A6"].Style.Font.Bold = true;
                        ws.Cells["A6"].Style.Font.Name = "Tahoma";
                        ws.Cells["A8"].LoadFromDataTable(dataDocsFinales, true, OfficeOpenXml.Table.TableStyles.Light1);
                        ws.Cells["A6"].Value = "Reporte Validación de Documentos";
                        ws.Cells["A6"].Style.Font.Size = 13;
                        ws.Cells["A6:H6"].Merge = true;

                        pck.Save();
                        pck.Dispose();
                        ws.Dispose();
                        stream.Dispose();

                    }
   
                }
                Type cstype = this.GetType();
                string script = "<script type=text/javascript>mesaggeExito('Consulta generada Sactisfactoriamente!!');</script>";
                ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                Gvaudit.DataSource = null;
                Gvaudit.DataBind();
                gvdetalle.DataSource = null;
                gvdetalle.DataBind();
                LimpiaCampos();

            }
        }
        private void LimpiaCampos()
        {
            txtaditoria.Value = string.Empty;
            dtfechafinal.Value = string.Empty;
            dtfechainicial.Value = string.Empty;
        }
        protected void gvdetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdetalle.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            clcon = new ClsConsulta();
            dt = clcon.getDataDocsFinales(Convert.ToInt32(txtaditoria.Value));
            if (dt.Rows.Count > 0)
            {
                gvdetalle.DataSource = dt;
                gvdetalle.DataBind();
            }
        }
    }
}
