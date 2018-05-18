<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConsultaUsuarios.aspx.cs" Inherits="AuditoriaTSS.frmConsultaUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Humanos</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/switchery.min.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.2.min.js"></script>
    <script src="/js/jquery-ui.js"></script>
    <script src="/js/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   
    
 <div class="col-md-3" id="logo">
      <a href="/"> <img  src="/image/logo_ARSH.png"/></a>
  </div><br>
       <div class="container" style="margin-left: 10px;">

        <asp:GridView ID="Gvaudit"   CssClass="table table-hover" runat="server" Width="100%" AutoGenerateColumns="False" 
                        EmptyDataText="No se han agregado registros!!"  
                        AllowPaging="false"  CellPadding="4" ForeColor="#0063ab" GridLines="None" ShowFooter="True">
                        <Columns>

                            <asp:BoundField DataField="idUsuario" ControlStyle-CssClass="Desc" HeaderText="codigo" Visible="false">
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombre" ControlStyle-CssClass="Desc" HeaderText="Nombre" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   DataFormatString="{0:dd-MM-yyyy}" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Usuario" ControlStyle-CssClass="Desc" HeaderText="Usuario" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   DataFormatString="{0:dd-MM-yyyy}" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Departamento" ControlStyle-CssClass="Desc" HeaderText="Departamento" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="idperfil" ControlStyle-CssClass="Desc" HeaderText="Perfil" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                
                        </Columns>
                 <FooterStyle BackColor="#FFFFCC" ForeColor="#0063ab" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    </asp:GridView>

           </div>
      
    </form>
</body>
</html>
