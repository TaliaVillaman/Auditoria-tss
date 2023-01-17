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
using System.Threading;

namespace AuditoriaTSS
{
    public partial class FrmArchivosEspecificos : System.Web.UI.Page
    {
       wsServices.AuditoriaWs ws_services = new wsServices.AuditoriaWs();
        private static string files;
        private static string e_error = "";
        private  String TipoConsulta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String id = (string)Session["valor"];
                PermisosMenu(id);
                llenargrid();

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
        public int  LlenaReporteExcel(int CodAuditoria, string ruta,string tipo_parametro,string valor)
        {
            ClsConsulta clcon = new ClsConsulta();
   
            DataTable dataDocsFinales = new DataTable("Data");
            dataDocsFinales = clcon.getDataDocsFinalesIndividual(CodAuditoria, tipo_parametro, valor);
            string path = ruta;
            int vl = 0;
            if (dataDocsFinales.Rows.Count > 0)
            {

                Stream stream = File.Create(ruta);
                string rutaimagen = System.Configuration.ConfigurationManager.AppSettings["imgHumano"].ToString();

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

                    vl = 1;
                }
            }
            return vl;
        }

        private int Ejecutar_Proceso_RAD(string tipAuditoria, int tipoBusqueda, DataTable DocReq, string rutaseleccionada, DataTable dtValues, string tipo_consulta, string valor)
        {
            int resultado = 0;
            try
            {

                ClsConsulta clcon = new ClsConsulta();
                DataTable dt = new DataTable("Data");
                ws_services.Timeout = -1;
                int codigo_auditoria = clcon.CreaNuevaAuditoria_Ws_Bs(1, tipAuditoria);
                clcon.InsertDataDocs(DocReq, codigo_auditoria.ToString());
                ws_services.dtbus_pol_radic_especific_Ws_Bs(tipo_consulta, codigo_auditoria, valor);
                ws_services.ProcesaBusquedaImagenes_Ws_Bs(codigo_auditoria, "RAD_POL");
                ws_services.transferir_sql_Ws_Bs(codigo_auditoria, "RAD_POL", tipo_consulta);
                resultado = ws_services.validaExitimagenesRad_Ws_Bs(codigo_auditoria);
                if (resultado != 0)
                {

                    clcon.CreaCarpetasPolRadicacion(codigo_auditoria, rutaseleccionada, tipo_consulta);
                    resultado = LlenaReporteExcel_rad(codigo_auditoria, rutaseleccionada + "Auditoria No_" + codigo_auditoria.ToString() + "\\" + "No_Auditoria_" + codigo_auditoria + ".xls", tipo_consulta);
                    ws_services.ActualizarAuditoria_Ws_Bs(codigo_auditoria);
                    resultado = codigo_auditoria;
                }
             
            }

            catch (Exception ex)
            {
                e_error = "A Ocurrido un error Procesando : " + ex.ToString();

                string path = @"C:\Temp\Anexo_error_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(e_error);

                    }
                }
            }

            return resultado;

        }

        private int Ejecutar_Proceso(string tipAuditoria, int tipoBusqueda, DataTable DocReq, string rutaseleccionada, DataTable dtValues, string tipo_consulta,string valor )
        {
            int resultado = 0;
            try
            {

 
                ws_services.Timeout = -1;
                ClsConsulta clcon = new ClsConsulta();
                DataTable dt = new DataTable("Data");

                int codigo_auditoria = clcon.CreaNuevaAuditoria_Ws_Bs(1, tipAuditoria);
                clcon.InsertDataDocs(DocReq, codigo_auditoria.ToString());
                clcon.InsertaDataBusquedaIndicidual(tipo_consulta, valor, dtValues, codigo_auditoria);
                ws_services.ProcesaBusquedaAfiliados_Ws_Bs(codigo_auditoria);
                ws_services.ProcesaBusquedaImagenes_Ws_Bs(codigo_auditoria, "");
                ws_services.transferir_sql_Ws_Bs(codigo_auditoria, "", "");
                resultado = ws_services.validaExitimagenes_Ws_Bs(codigo_auditoria);
                if (resultado != 0)
                {
                    clcon.CreaCarpetasIndividual(codigo_auditoria, rutaseleccionada, tipo_consulta, valor);
                    LlenaReporteExcel(codigo_auditoria, rutaseleccionada + "Auditoria No_" + codigo_auditoria.ToString() + "\\" + "No_Auditoria_" + codigo_auditoria + ".xls", tipo_consulta, valor);
                    ws_services.ActualizarAuditoria_Ws_Bs(codigo_auditoria);
                resultado = codigo_auditoria;
            }

                }
 

            catch (Exception ex)
            {

                e_error = "A Ocurrido un error Procesando : " + ex.ToString();

                string path = @"C:\Temp\Anexo_error_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(e_error);

                    }
                }
            }

            return resultado;

        }
        private void llenargrid()
        {


            ClsConsulta clcon = new ClsConsulta();
            DataTable dt = new DataTable();
            wsServices.AuditoriaWs ws = new wsServices.AuditoriaWs();

            dt = clcon.getdocumentos();

            if (dt.Rows.Count > 0)
            {
                Gvaudit.DataSource = dt;
                Gvaudit.DataBind();
            }


        }
        protected void Gvaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvaudit.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            ClsConsulta clcon = new ClsConsulta();

            dt = clcon.getdocumentos();

            if (dt.Rows.Count > 0)
            {
                Gvaudit.DataSource = dt;
                Gvaudit.DataBind();
            }
        }
        public int LlenaReporteExcel_rad(int CodAuditoria, string ruta, string tipo)
        {
            ClsConsulta clcon = new ClsConsulta();
            //ws = new wsServices.AuditoriaWs();
            DataTable dataDocsFinales = new DataTable("Data");
            dataDocsFinales = clcon.getDataDocsFinalesRadic_poliza(CodAuditoria, tipo);
            int val = 0;

            if (dataDocsFinales.Rows.Count > 0)
            {
                string path = ruta;
                string rutaimagen = System.Configuration.ConfigurationManager.AppSettings["imgHumano"].ToString();
                Stream stream = File.Create(ruta);

                using (ExcelPackage pck = new ExcelPackage(stream))
                {
                    System.Drawing.Image logo = System.Drawing.Image.FromFile(rutaimagen + "logo_ARSH.png");
                    ExcelWorksheet Worksheet = pck.Workbook.Worksheets.Add("Datos");
                    Worksheet.Drawings.AddPicture(rutaimagen + "logo_ARSH.png", logo);
                    Worksheet.Cells["A6"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    Worksheet.Cells["A6"].Style.Font.Bold = true;
                    Worksheet.Cells["A6"].Style.Font.Name = "Tahoma";
                    Worksheet.Cells["A8"].LoadFromDataTable(dataDocsFinales, true, OfficeOpenXml.Table.TableStyles.Light1);
                    Worksheet.Cells["A6"].Value = "Reporte Validación de Documentos";
                    Worksheet.Cells["A6"].Style.Font.Size = 13;
                    Worksheet.Cells["A6:H6"].Merge = true;
                    pck.Save();
                    pck.Dispose();
                    pck.Dispose();
                    stream.Dispose();
                    val = 1;
                }
            }
            return val;
        }
        private int Procesar_datos(string tipoconsulta,string valor,string ruta,DataTable dtResultDoc)
        {
            //ws_services.Timeout = -1;
   
            ClsConsulta clcon = new ClsConsulta();
            int RbBusqueda = 0;
            string tipo_auditoria = "I";
            int val = 0;

            DataTable dtValues = new DataTable("Data");
            dtValues.Columns.Add("NSS", typeof(string));
            dtValues.Columns.Add("NSSAfiliado", typeof(string));
            dtValues.Columns.Add("CedulaTitular", typeof(int));
            dtValues.Columns.Add("CedulaDep", typeof(string));
            if (tipoconsulta == "POLIZA")
            {
                val = Ejecutar_Proceso_RAD(tipo_auditoria, RbBusqueda, dtResultDoc, ruta, dtValues, tipoconsulta, valor);
            }
            else if (tipoconsulta == "RADICACION")
            {
                val = Ejecutar_Proceso_RAD(tipo_auditoria, RbBusqueda, dtResultDoc, ruta, dtValues, tipoconsulta, valor);
            }
            else
            {
                dtValues = clcon.dtbus_especifica_Ws_Bs(tipoconsulta, txtcedula.Value, txtnss.Value, txtafiliado.Value, txtradicacion.Value, txtpoliza.Value);
                if (dtValues.Rows.Count > 0)
                {
                    val = Ejecutar_Proceso(tipo_auditoria, RbBusqueda, dtResultDoc, ruta, dtValues, tipoconsulta, valor);
                }
                else
                {
                    val = 0;

                }
            }
            return val;
        }

        protected void btnmarcar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gvaudit.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkAccept");
                check.Checked = true;
            }
        }

        protected void btndescargar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gvaudit.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkAccept");
                check.Checked = false;
            }
        }
       
        protected void btncargar_Click(object sender, EventArgs e)
        {

            string validar = "";

            e_error = "";
            int NoAuditoria = 0;
            if (txtcedula.Value!="")
            {
                TipoConsulta = "CEDULA";
                validar = txtcedula.Value;
             
            }
            else if (txtnss.Value != "")
            {
                TipoConsulta = "NSS";
                validar = txtnss.Value;
                
            }
            else if (txtafiliado.Value != "")
            {
                TipoConsulta = "CODIGO AFILIADO";
                validar = txtafiliado.Value;
            
            }
            else if (txtpoliza.Value != "")
            {
                TipoConsulta = "POLIZA";
                validar = txtpoliza.Value;

            }
            else if (txtradicacion.Value != "")
            {
                TipoConsulta = "RADICACION";
                validar = txtradicacion.Value;

            }


            if (validar != string.Empty)
            {



                DataTable dtResultDoc = new DataTable("data");
                dtResultDoc.Columns.Add("ITEMTYPENUM", typeof(int));
                dtResultDoc.Columns.Add("ITEMTYPENAME", typeof(string));


                foreach (GridViewRow row in Gvaudit.Rows)
                {
                    CheckBox check = (CheckBox)row.FindControl("chkAccept");
                    if (check.Checked)
                    {
                        Literal idDoc = (Literal)Gvaudit.Rows[row.RowIndex].FindControl("ltr_doc");
                        dtResultDoc.Rows.Add(Convert.ToInt32(idDoc.Text), row.Cells[1].Text.Trim());
                    }
                }

                    files = System.Configuration.ConfigurationManager.AppSettings["RepIndividual"].ToString();

                if (dtResultDoc.Rows.Count > 0)
                {
                    NoAuditoria = Procesar_datos(TipoConsulta, validar, files, dtResultDoc);

                    if (e_error == "" )
                    {
                        if (NoAuditoria > 0)
                        {
                            Type cstype = this.GetType();
                            string script = "<script type=text/javascript>mesaggeExito('Proceso Completado. Su Numero de Auditoria  es " + NoAuditoria.ToString() + "');</script>";
                            ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                            limpiaParametros();
                        }
                        else
                        {

                            Type cstype = this.GetType();
                            string script = "<script type=text/javascript>mesaggeinfo('No Existen Datos para esta consulta!!');</script>";
                            ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                        }
                    }
                    else
                    {
                        Type cstype = this.GetType();
                        string script = "<script type=text/javascript>mesaggealerta('Proceso No completado, Favor verificar!!');</script>";
                        ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                    }
                }
                else
                {

                    Type cstype = this.GetType();
                    string script = "<script type=text/javascript>mesaggeinfo('Favor de elegir los documentos a Buscar');</script>";
                    ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                }
            }
            else
            {
                Type cstype = this.GetType();
                string script = "<script type=text/javascript>mesaggeinfo('Favor Digitar los valores a consultar!!');</script>";
                ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
                    return;
            }

        }

        private void limpiaParametros()
        {
            txtcedula.Value = string.Empty;
            txtpoliza.Value = string.Empty;
            txtafiliado.Value = string.Empty;
            txtnss.Value = string.Empty;
            txtradicacion.Value = string.Empty;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            limpiaParametros();
            foreach (GridViewRow row in Gvaudit.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkAccept");
                check.Checked = false;
            }

        }

    }
}
    
    

