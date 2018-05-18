<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConsultaArchivo.aspx.cs" Inherits="AuditoriaTSS.frmConsultaArchivo" %>

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
      <a href="/"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="/image/logo_ARSH.png"></a>
  </div>

         <div class="container" style="margin-left: 10px;">
      <div class="col-md-2" style="margin-left:700px; height: 36px; width: 431px;">
          <asp:FileUpload ID="FileUpload1" runat="server" Width="426px" />
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
        ControlToValidate="FileUpload1" 
      ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.xlsx)$">
  </asp:RegularExpressionValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
      ControlToValidate="FileUpload1" ErrorMessage="Debe seleccionar una archivo en formato de excel"></asp:RequiredFieldValidator>

          </div>

                <div class="col-md-2" style="margin-left:720px; height: 36px; width:840px;">
         <asp:Button ID="btnmarcar" style=" width: 200px;height:35px;border:none;border-radius:4px;margin: 0 0 15px 0;font-size: 14px;color: #0063ab;font-weight: bold;text-shadow: 1px 1px 0 rgba(0,0,0,0.3);overflow: hidden;outline: none;" runat="server" Text="Marcar"   />
       <asp:Button ID="btndescargar" style=" width: 200px;height:35px;border:none;border-radius:4px;margin: 0 0 15px 0;font-size: 14px;color: #0063ab;font-weight: bold;text-shadow: 1px 1px 0 rgba(0,0,0,0.3);overflow: hidden;outline: none;" runat="server" Text="Desmarcar"  />
	</div>
         
        <asp:GridView ID="Gvaudit"   CssClass="table table-striped" runat="server" Width="100%" AutoGenerateColumns="False" 
                        EmptyDataText="No se han agregado registros!!"  
                        AllowPaging="false"  CellPadding="4" ForeColor="#0063ab" GridLines="None" ShowFooter="True">
                        <Columns>
                         <asp:TemplateField HeaderStyle-BackColor="#0063ab" HeaderText="Marcar">
                                <ItemTemplate>
                                     <asp:Literal ID="id" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Literal>
                           <asp:Literal ID="ltr_rnc" runat="server" Visible="false" Text='<%# Eval("Descripcion") %>'></asp:Literal>
                                      <asp:CheckBox ID="chkAccept" runat="server" EnableViewState="true" Checked="false"/>
                                </ItemTemplate>
                         </asp:TemplateField>


                            <asp:BoundField DataField="Descripcion" ControlStyle-CssClass="Desc" HeaderText="Documentos"  HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White" >
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683" />
                            </asp:BoundField>
                            
<%--                            <asp:BoundField DataField="Dependiente" ControlStyle-CssClass="Desc" HeaderText="Dependiente" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   DataFormatString="{0:dd-MM-yyyy}" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Ced_titular" ControlStyle-CssClass="Desc" HeaderText="Cedula Titular" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White" >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Ced_Dependiete" ControlStyle-CssClass="Desc" HeaderText="Cedula Dependiente" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>--%>
                
                        </Columns>
                 <FooterStyle BackColor="#FFFFCC" ForeColor="#0063ab" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    </asp:GridView>

             <div class="col-md-2" style=" height: 47px; width:541px; top: 0px; left: 0px;">
                 <strong>Seleccione el tipo de busqueda :</strong> <br />

                 <input id="nucleo" type="radio"  /> <strong>Por nucleo :</strong>
                 <input id="puntual" type="radio"  /> <strong>Puntual :</strong>

                 </div>
                <div class="col-md-2" style="margin-left:720px; height: 36px; width:708px; top: 0px; left: 0px;">
         <asp:Button ID="btncargar" style=" width: 200px;height:35px;border:none;border-radius:4px;margin: 0 0 15px 0;font-size: 14px;color: #0063ab;font-weight: bold;text-shadow: 1px 1px 0 rgba(0,0,0,0.3);overflow: hidden;outline: none;" runat="server" Text="Procesar"  OnClick="btncargar_Click" />
       <asp:Button ID="btnconsultar" style=" width: 200px;height:35px;border:none;border-radius:4px;margin: 0 0 15px 0;font-size: 14px;color: #0063ab;font-weight: bold;text-shadow: 1px 1px 0 rgba(0,0,0,0.3);overflow: hidden;outline: none;" runat="server" Text="Atras" OnClick="btnconsultar_Click" />


	</div>
    </div>
    </form>
</body>
</html>
