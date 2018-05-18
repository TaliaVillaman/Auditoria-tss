<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRepote.aspx.cs" Inherits="AimgosWeb.frmRepote" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 


        ReportSourceID="CrystalReportSource2" 

        ToolPanelWidth="200px" Width="1104px" HasRefreshButton="True"

        EnableDatabaseLogonPrompt="False" ToolPanelView="None"  Visible="true" />

    <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">

        <Report FileName="crReporteValidacion.rpt">

            <DataSources>

                <CR:DataSourceRef DataSourceID="Prueba.xsd" TableName="VenNum" />

            </DataSources>

        </Report>

    </CR:CrystalReportSource>
    </div>
    </form>
</body>
</html>
