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

namespace AuditoriaTSS
{
    public partial class frmConsultaArchivo : System.Web.UI.Page
    {
        public static string rutaOrigen;
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
        private void CagaArchivo(string nsstitular,string nssdependiente,string cedtitular,string ceddependiente,string usuario)
        {
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Exec sp_inserta_documentos_Tss '" + nsstitular + "','" + nssdependiente + "','" + cedtitular + "','" + ceddependiente + "','" + usuario + "'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        private void eliminardatos()
        {
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Truncate table Audit_cargaTSS ";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
       public DataTable ExcelDataTable()
        {
            DataTable tbl = new DataTable();
            if (FileUpload1.HasFile)
            {
                string rutaOrigen = FileUpload1.PostedFile.FileName;

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
        protected void btncargar_Click(object sender, EventArgs e)
        {
            DataTable dt = ExcelDataTable();
            eliminardatos();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string validar = row[0].ToString();

                    if (validar != "")
                    {
                        CagaArchivo(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), Environment.UserName.ToString());
                    }
                }
            }
          //  llenargrid();
        }

        protected void btnconsultar_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ArchivoTSS.xlsx");
            //Response.TransmitFile(Server.MapPath("~/image/logo_ARSH.png"));
            Response.End();

        }
    }
}