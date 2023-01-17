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
    public partial class FrmConsultaCliente : System.Web.UI.Page
    {
        wsServices.AuditoriaWs ws_services = new wsServices.AuditoriaWs();
        private static string e_error = "";
        public  string rutaOrigen;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
        private void llenargrid()
        {



            DataTable dt = new DataTable();
            wsServices.AuditoriaWs ws = new wsServices.AuditoriaWs();
            ClsConsulta clcon = new ClsConsulta();
            dt = clcon.getdocumentos();
            ckradicacion.Checked = true;
            if (dt.Rows.Count > 0)
            {
                Gvaudit.DataSource = dt;
                Gvaudit.DataBind();
            }
            String id = (string)Session["valor"];
            PermisosMenu(id);

        }
        public DataTable ExcelDataTable()
        {
            DataTable tbl = new DataTable("Data");
            string rutaArchivo = System.Configuration.ConfigurationManager.AppSettings["RutaArchivo"].ToString();
            if (FileUpload1.HasFile)
            {
                string rutaOrigen = rutaArchivo + Path.GetFileName(FileUpload1.PostedFile.FileName);

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
        private int Ejecutar_Proceso(string tipAuditoria, string  tipoBusqueda, DataTable DocReq, string rutaseleccionada, bool blSoloGenerarArchivoExcel)
        {
            int Val = 0;
            try
            {
                ClsConsulta clcon = new ClsConsulta();
                ws_services.Timeout = -1;
                DataTable dtValues = new DataTable("Data");
                DataTable dt = new DataTable("Data");
                dtValues.Columns.Add("Poliza", typeof(string));
                dtValues = ExcelDataTable();
                int codigo_auditoria = ws_services.CreaNuevaAuditoria_Ws_Bs(1, tipAuditoria);
                clcon.InsertDataDocs(DocReq, codigo_auditoria.ToString());
                ws_services.dtbus_pol_radic_Ws_Bs(tipoBusqueda, codigo_auditoria, dtValues);
                ws_services.ProcesaBusquedaImagenes_Ws_Bs(codigo_auditoria, "RAD_POL");
                ws_services.transferir_sql_Ws_Bs(codigo_auditoria, "RAD_POL", tipoBusqueda);
                Val = ws_services.validaExitimagenesRad_Ws_Bs(codigo_auditoria);
                if (Val != 0)
                {
                   
                    clcon.CreaCarpetasPolRadicacion(codigo_auditoria, rutaseleccionada, tipoBusqueda);
                    Val = LlenaReporteExcel(codigo_auditoria, rutaseleccionada + "Auditoria No_" + codigo_auditoria.ToString() + "\\" + "No_Auditoria_" + codigo_auditoria + ".xls", tipoBusqueda);
                    ws_services.ActualizarAuditoria_Ws_Bs(codigo_auditoria);
                    Val = codigo_auditoria;
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
            return Val;
        }


        public int LlenaReporteExcel(int CodAuditoria, string ruta,string  tipo)
        {
            ClsConsulta clcon = new ClsConsulta();
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gvaudit.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkAccept");
                check.Checked = false;
            }
        }
        protected void Gvaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvaudit.PageIndex = e.NewPageIndex;
            llenargrid();
        }
        private int Procesar_datos(DataTable dtResultDoc)
        {
            int noaudit = 0;
            string  RbBusqueda = "";
            string tipo_auditoria = "M";
            ClsConsulta clcon = new ClsConsulta();
         
          
            string ruta = System.Configuration.ConfigurationManager.AppSettings["Repcliente"].ToString(); 
            if (ckpoliza.Checked)
            {
                RbBusqueda ="POLIZA";
                noaudit = Ejecutar_Proceso(tipo_auditoria, RbBusqueda, dtResultDoc, ruta, true);

            }
            else if (ckradicacion.Checked)
            {
                RbBusqueda = "RADICACION";
                noaudit = Ejecutar_Proceso(tipo_auditoria, RbBusqueda, dtResultDoc, ruta, true);

            }

            return noaudit;
        }
       
        protected void ckradicacion_CheckedChanged(object sender, EventArgs e)
        {
            if (ckradicacion.Checked)
            {
                ckpoliza.Checked = false;
                CKNSS.Checked = false;
            }
        }
        protected void ckpoliza_CheckedChanged(object sender, EventArgs e)
        {
            if (ckpoliza.Checked)
            {
                ckradicacion.Checked = false;
                CKNSS.Checked = false;
            }
        }

        protected void btncargar_Click(object sender, EventArgs e)
        {
            int NoAuditoria = 0;
            e_error = "";

            if (FileUpload1.HasFile)
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

                if (dtResultDoc.Rows.Count > 0)
                    {
                        NoAuditoria = Procesar_datos(dtResultDoc);
                        if (e_error == "")
                        {
                            if (NoAuditoria > 0)
                            {
                                Type cstype = this.GetType();
                                string script = "<script type=text/javascript>mesaggeExito('Proceso Completado. Su Numero de Auditoria  es " + NoAuditoria.ToString() + "');</script>";
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
                string script = "<script type=text/javascript>mesaggeinfo('Favor de elegir el archivo en fromato  excel.');</script>";
                ClientScript.RegisterStartupScript(cstype, "urlVolver", script);
            }


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

        protected void CKNSS_CheckedChanged(object sender, EventArgs e)
        {
            if (CKNSS.Checked)
            {
                ckpoliza.Checked = false;
                ckradicacion.Checked = false;
            }
        }
    }
}