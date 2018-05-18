using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace AimgosWeb
{
    public partial class frmRepote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = "Select * from ListadoAuditoria";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 1000000;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load(Server.MapPath(@"crReporteValidacion.rpt"));
                    //rptDoc.SetDataSource(dt);
                    CrystalReportViewer1.ReportSource = rptDoc;
                    CrystalReportSource2.ReportDocument.SetDataSource(dt);
                    CrystalReportSource2.DataBind();
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.Visible = true;
                }
            }

            }

                //dt.TableName = "Crystal Report Example"
                //sqlCon = New SqlConnection("server='servername';
                //Initial Catalog = 'databasename'; user id = 'userid'; password = 'password'")
                //Dim da As New SqlDataAdapter("select Stud_Name, Class, 
                //Subject, Marks from stud_details", sqlCon)
                //da.Fill(dt)
                //ds.Tables(0).Merge(dt)
                //rptDoc.Load(Server.MapPath("SimpleCrystal.rpt"))
                //rptDoc.SetDataSource(ds)
                //CrystalReportViewer1.ReportSource = rptDoc
            }
    }
