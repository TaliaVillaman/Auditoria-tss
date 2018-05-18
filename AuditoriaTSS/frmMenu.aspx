<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMenu.aspx.cs" Inherits="AuditoriaTSS.frmMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Humanos</title>
       <link href="css/animsition.min.css" rel="stylesheet" />
    <link href="css/daterangepicker.css" rel="stylesheet" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/util.css" rel="stylesheet" />
    <link href="css/Menudisplay.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

             <table style="float:right; margin-right:8px; border-bottom: 2px solid #d6a2a2;">
              <tbody>
                  <tr>
                      <td colspan="2" style="/*border-bottom: 2px solid #D6A2A2;*/ padding:5px 0; font-weight:bold; color: #B75858;">
                          Usuario logueado:
                      </td>
                  </tr>
                  <tr>
                      <td style="width:180px; height:30px; background:#eee;">
                          <strong>Usuario:</strong> <asp:TextBox runat="server" ID="txt_usuario" style="border:none; width:100px; height:17px; display:inline; background:#eee;" ></asp:TextBox>
                         
                           </td>
                      <td style="width:210px; height:30px; background:#eee;">
                          <strong>Fecha:</strong> <asp:TextBox runat="server" ID="txtfecha" style="border:none; width:100px; height:17px; display:inline; background:#eee;" ></asp:TextBox>
                      </td>
                  </tr>


              </tbody>
          </table>  



 <div class="col-md-3" id="logo">
      <a href="/"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="/image/logo_ARSH.png"></a>
 </div>
    <div  class="col-md-12">

        <asp:Menu ID="menuaudit" runat="server" style="background:#0063ab;margin: 0 auto;display:block; "  ClientIDMode="Static"  
                Orientation="Vertical" 
                IncludeStyleBlock="true" 
                RenderingMode="List"  >
            <DynamicHoverStyle BackColor="#58ACFA" ForeColor="#EFFBFB" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#58ACFA" />
            <DynamicSelectedStyle BackColor="#58ACFA" />
            <Items >
                <asp:MenuItem Text="Inicio" Value="Inicio" >
                    <asp:MenuItem Text="Salir" Value="Salir"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Procesos" Value="Consultas">
                    <asp:MenuItem Text="Proceso carga archivo Masivo" Value="Proceso carga archivo Masivo" NavigateUrl="~/frmConsultaArchivo.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Proceso carga archivo Especifica" Value="Proceso carga archivo Especifica" NavigateUrl="~/FrmArchivosEspecificos.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Admnistracion de Usuarios" Value="Admnistracion de Usuarios">
                    <asp:MenuItem Text="Creacion de Usuarios" Value="Creacion de Usuarios" NavigateUrl="~/frmCrearUsuario.aspx" ></asp:MenuItem>
                    <asp:MenuItem Text="Consula Usuarios" Value="Consulta Usuarios" NavigateUrl="~/frmConsultaUsuarios.aspx"> </asp:MenuItem>
                     <asp:MenuItem Text="Creacion de perfiles" Value="Creacion de perfiles" NavigateUrl="~/frmCreacionPerfil.aspx"> </asp:MenuItem>
                </asp:MenuItem>

            </Items>
            <StaticHoverStyle BackColor="#58ACFA" ForeColor="#EFFBFB" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#58ACFA" />
        </asp:Menu>
            </div>

        
    </form>
</body>
</html>
