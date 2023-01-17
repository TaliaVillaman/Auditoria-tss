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
    public partial class frmConsultaArchivo : System.Web.UI.Page
    {
        wsServices.AuditoriaWs ws_services = new wsServices.AuditoriaWs();
        private static string e_error = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                String id = (string)Session["valor"];
                PermisosMenu(id);
                llenargrid();

            }
        }
        private int Ejecutar_Proceso(string tipAuditoria, int tipoBusqueda, DataTable DocReq, string rutaseleccionada, DataTable dtValues)
        {
            int val = 0;
            try
            {

                ClsConsulta clcon = new ClsConsulta();
                ws_services.Timeout = -1;

                int codigo_auditoria = ws_services.CreaNuevaAuditoria_Ws_Bs(tipoBusqueda, tipAuditoria);
                clcon.InsertDataDocs(DocReq, codigo_auditoria.ToString());
                clcon.InsertaDataBusqueda(dtValues, codigo_auditoria);
                ws_services.ProcesaBusquedaAfiliados_Ws_Bs(codigo_auditoria);
                ws_services.ProcesaBusquedaImagenes_Ws_Bs(codigo_auditoria, "");
                ws_services.transferir_sql_Ws_Bs(codigo_auditoria, "", "");
                val = ws_services.validaExitimagenes_Ws_Bs(codigo_auditoria);

                if (val != 0)
                {
                    clcon.CreaCarpetas(codigo_auditoria, rutaseleccionada,   tipoBusqueda.ToString());
                    LlenaReporteExcel(codigo_auditoria, rutaseleccionada + "Auditoria No_" + codigo_auditoria.ToString() + "\\" + "No_Auditoria_" + codigo_auditoria.ToString() + ".xls");
                    ws_services.ActualizarAuditoria_Ws_Bs(codigo_auditoria);
                    val = codigo_auditoria;
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
            return val;

        }
        private int Ejecutar_Proceso_Adic(string tipAuditoria,  DataTable DocReq, string rutaseleccionada, DataTable dtValues)
        {
            ClsConsulta clcon = new ClsConsulta();
            DataTable dt = new DataTable("Data");
            ws_services.Timeout = -1;

            string tipoBusqueda = "";

            if ((ckpol.Checked) && (ckrad.Checked))
            {
                tipoBusqueda = "TODOS";
            }
            else if (ckpol.Checked)
            {
                tipoBusqueda = "POLIZA";
            }
            else if (ckrad.Checked)
            {
                tipoBusqueda = "RADICACION";
            }
            int Val = 0;
            try
            {

                int codigo_auditoria = ws_services.CreaNuevaAuditoria_Ws_Bs(1, tipAuditoria);
                clcon.InsertDataDocs(DocReq, codigo_auditoria.ToString());
                clcon.InsertaDataBusqueda(dtValues, codigo_auditoria);
                ws_services.ProcesaBusquedaAfiliados_Adicional_Ws_Bs(codigo_auditoria);
                ws_services.ProcesaBusquedaImagenes_Ws_Bs(codigo_auditoria, "NSS_RAD");
                ws_services.transferir_sql_Ws_Bs(codigo_auditoria, "NSS_RAD", "NSS_RAD");
                Val = clcon.validaExitimagenes_Ws_Bs(codigo_auditoria);
                if (Val != 0)
                {

                    clcon.CreaCarpetasadic(codigo_auditoria, rutaseleccionada, tipoBusqueda);
                    Val = LlenaReporteExcel_Adic(codigo_auditoria, rutaseleccionada + "Auditoria No_" + codigo_auditoria.ToString() + "\\" + "No_Auditoria_" + codigo_auditoria + ".xls", tipoBusqueda);
                    ws_services.ActualizarAuditoria_Ws_Bs(codigo_auditoria);
                    Val = codigo_auditoria;
                }
            }

            catch (Exception ex)
            {
                e_error = "A Ocurrido un error Procesando : " + ex.ToString();

                string path = @"C:\Temp\Anexo_error_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path) == false)
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(e_error);

                    }
                }
            }
        
            return Val;
        }
        public int LlenaReporteExcel_Adic(int CodAuditoria, string ruta, string tipo)
        {
            ClsConsulta clcon = new ClsConsulta();
            DataTable dataDocsFinales = new DataTable("Data");
            dataDocsFinales = clcon.getDataDocsFinalesADIC(CodAuditoria);
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

        public int LlenaReporteExcel(int CodAuditoria ,string ruta)
        {
            ClsConsulta clcon = new ClsConsulta();

            DataTable dataDocsFinales = new DataTable("Data");
            dataDocsFinales = clcon.getDataDocsFinales(CodAuditoria);
            int val = 0;
 
            if (dataDocsFinales.Rows.Count > 0)
            {
                string path = ruta;
                string rutaimagen = System.Configuration.ConfigurationManager.AppSettings["imgHumano"].ToString();
                Stream stream = File.Create(ruta);

                using (ExcelPackage pck = new ExcelPackage(stream))
                {
                    System.Drawing.Image logo = System.Drawing.Image.FromFile(rutaimagen+ "logo_ARSH.png");
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
        protected void Gvaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           Gvaudit.PageIndex = e.NewPageIndex;
            llenargrid();
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
            cknucleo.Checked = true;
        }
        public DataTable ExcelDataTable()
        {
            DataTable tbl = new DataTable("Data");
            string rutaArchivo = System.Configuration.ConfigurationManager.AppSettings["RutaArchivo"].ToString();
            if (myFile.HasFile)
            {

                string rutaOrigen = rutaArchivo + Path.GetFileName(myFile.PostedFile.FileName);
                var pck = new OfficeOpenXml.ExcelPackage();
                pck.Load(File.OpenRead(rutaOrigen));
                var ws = pck.Workbook.Worksheets.First();
               
                bool hasHeader = true;
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    tbl.Rows.Add(row);
                }
                pck.Dispose();
             
            }
            return tbl;
        }
        private int  Procesar_datos(DataTable dtdoc)
        {
            int noaudit = 0;
            int RbBusqueda = 1;
            string tipo_auditoria = "M";

            DataTable dtValues = new DataTable("Data");
            dtValues.Columns.Add("NSS", typeof(string));
            dtValues.Columns.Add("NSSAfiliado", typeof(string));
            dtValues.Columns.Add("CedulaTitular", typeof(int));
            dtValues.Columns.Add("CedulaDep", typeof(string));
            dtValues = ExcelDataTable();

            string ruta = System.Configuration.ConfigurationManager.AppSettings["RepMasivo"].ToString();
            if (cknucleo.Checked)
            {
                RbBusqueda = 1;
                noaudit = Ejecutar_Proceso(tipo_auditoria, RbBusqueda, dtdoc, ruta, dtValues);
            }
            else if (ckNss.Checked)
            {
                RbBusqueda = 2;
                noaudit = Ejecutar_Proceso(tipo_auditoria, RbBusqueda, dtdoc, ruta, dtValues);
            }
            if ((ckpol.Checked) || (ckrad.Checked))
            {
                noaudit = Ejecutar_Proceso_Adic(tipo_auditoria, dtdoc, ruta, dtValues);
            }
            return noaudit;
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
            int noauditoria = 0;
            if (myFile.HasFile)
            {


                DataTable dtResultDoc = new DataTable("Data");
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

            
                if (dtResultDoc.Rows.Count>0)
                {
                    e_error = "";
                    noauditoria = Procesar_datos(dtResultDoc);
                    if (e_error == "" )
                    {
                        if (noauditoria > 0)
                        {
                            Type cstype = this.GetType();
                            string script = "<script type=text/javascript>mesaggeExito('Proceso Completado. Su Numero de Auditoria es " + noauditoria.ToString() + "');</script>";
                            ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
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
                string script = "<script type=text/javascript>mesaggeinfo('Favor de elegir el archivo en formato Excel');</script>";
                ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
            }
           

        }

        protected void cknucleo_CheckedChanged(object sender, EventArgs e)
        {
            if (cknucleo.Checked)
            {
                ckNss.Checked = false;
                ckpol.Checked = false;
                ckrad.Checked = false;
            }
        }

        protected void ckNss_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNss.Checked)
            {
                cknucleo.Checked = false;
                ckpol.Checked = false;
                ckrad.Checked = false;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gvaudit.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkAccept");
                check.Checked = false;
            }
        }

        protected void ckpol_CheckedChanged(object sender, EventArgs e)
        {
            if (ckpol.Checked)
            {
                ckNss.Checked = false;
                cknucleo.Checked = false;
            }

        }

        protected void ckrad_CheckedChanged(object sender, EventArgs e)
        {
            if (ckrad.Checked)
            {
                ckNss.Checked = false;
                cknucleo.Checked = false;
            }
        }
    }
}