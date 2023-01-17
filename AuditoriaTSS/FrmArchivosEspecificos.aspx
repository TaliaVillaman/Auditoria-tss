<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmArchivosEspecificos.aspx.cs" Inherits="AuditoriaTSS.FrmArchivosEspecificos" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>

  <title>Proceso Especifico</title>
    <link rel="shortcut icon" href="image/favicon.png"/>
    <link rel="apple-touch-icon" href="image/apple-touch-icon.png"/>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
     <link href="css/EstiloHumano.css" rel="stylesheet" />
        <script src="js/sweetalert.min.js"></script>
 
<script type="text/javascript">
    $(document).ready(main);

    var contador = 1;
  
    function mesaggeExito(mensaje) {
        swal("Resultados de Busquedas", mensaje, "success");
    }
    function mesaggeinfo(mensaje) {
        swal("Resultados de Busquedas", mensaje, "info");
    }

    function mesaggealerta(mensaje) {
        swal("Resultados de Busquedas", mensaje, "warning");
    }

    function main() {
        var object =document.getElementById("contenedor");
        $('.menu_bar').click(function () {
            if (contador == 1) {
                $('nav').animate({
                    left: '0'

                });
                object.style.marginLeft = '18%';
                contador = 0;
            } else {
                contador = 1;
                $('nav').animate({
                    left: '-100%'
                });
          
                object.style.marginLeft = '0%';
              
            }
        });
     
        $('.submenu').click(function () {
            $(this).children('.children').slideToggle();
        });
    }
    function showWait() {
        var max = 9 * 60 * 9999999;
        $get('UpdateProgress1').style.display = 'block';
        setTimeout(document.images['procesando_gif'].src = 'image/giphy.gif', max);
      
    }
    
</script>



        <style>

                               .overlay  
        {
    	    position: fixed;
    	    z-index: 98;
    	    top: 0px;
    	    left: 0px;
    	    right: 0px;
    	    bottom: 0px;
            background-color: #aaa; 
            filter: alpha(opacity=80); 
            opacity: 0.8; 
        }
        .overlayContent
        {
    	    z-index: 99;
    	    margin: 250px auto;
    	    width: 80px;
    	    height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
    	    width: 80px;
    	    height: 80px;
        }
         .container {
    position: relative;

    height: 587px;
}
          .mGrid {  
    width: 100%;  
    background-color: #fff !important;  
    margin: 5px 0 10px 0 !important;  
    border: solid 1px #525252 !important;  
    border-collapse: collapse;  
}  
       .mGrid td {  
        padding: 2px !important;  
        border: solid 1px #c1c1c1 !important;  
        color: #717171;  
    }  
       .mGrid th {  
        padding: 4px 2px !important;  
        color: #fff !important;  
        background: #424242 url(grd_head.png) repeat-x top;  
        border-left: solid 1px #525252 !important;  
        font-size: 0.9em;  
    }  
      .mGrid .alt {  
        background: #fcfcfc url(grd_alt.png) repeat-x top !important;  
    }  
      .mGrid .pgr {  
        background: #424242 url(grd_pgr.png) repeat-x top;  
    }  
      .mGrid .pgr table {  
            margin: 5px 0;  
        }  
      .mGrid .pgr td {  
            border-width: 0;  
            padding: 0 6px;  
            border-left: solid 1px #666;  
            font-weight: bold;  
            color: #fff;  
            line-height: 12px;  
        }  
       .mGrid .pgr a {  
            color: #666;  
            text-decoration: none;  
        }  
       .mGrid .pgr a:hover {  
                color: #000;  
                text-decoration: none;  
            } 
      .login-form {
		width: 340px;
    	margin: 50px auto;
	}
    .login-form form {
    	margin-bottom: 15px;
        background: #f7f7f7;
        box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        padding: 30px;
    }
    .login-form h2 {
        margin: 0 0 15px;
    }
    .form-control, .btn {
        min-height: 38px;
        border-radius: 2px;
    }
    .btn {        
        font-size: 15px;
        font-weight: bold;
        background:#00e3d1;
        text-align:center;
    }  
    </style>
</head>
<body>

 <header>

	<div class="menu_bar" style="float:left;">
              <a href="frmmenu.aspx"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="image/logo_ARSH.png" style="margin-right:1500px;margin-top:-20px;height:83px;margin-bottom:-17px;"></a>
			<a href="#" class="menu_bar-bt-menu"  style="margin-left: 10px;margin-top: -50px;"  ><img border="0" height="40" width="30" src="image/icon-list2.png" style="margin-left: 230px;margin-top: -50px;" /></a>

      <div class="menu_bar_2" style="float:left;">
          </div>
        </div>
 
		<nav>
			<ul>
				<li><a href="frmmenu.aspx"><img hspace="5" vspace="5" style="float: left;" height="40" width="30" src="image/inicio.png" /><label style="margin-left:10px;" >Inicio</label></a></li>
				



                <li runat="server" id="proces" class="submenu" >
                    <a href="#"><img hspace="5" vspace="5" style="float: left;" height="40" width="30" src="image/procesos.png" /><span class="icon-rocket"></span>Procesos<span class="caret icon-arrow-down6"></span></a>
					<ul class="children">
						<li ><a href="frmConsultaArchivo.aspx?usuario='<%# Eval("RNC") %>'">Proceso Masivo</a></li>
                        <li ><a href="FrmConsultaCliente.aspx">Proceso Masivo Póliza</a></li>
						<li><a href="FrmArchivosEspecificos.aspx">Proceso Especifica</a></li>
					</ul>
		           </li>
                 <li runat="server" id="rept" class="submenu" >
                      <a href="#"><img hspace="5" vspace="5" style="float: left;" height="40" alt="Notebook" width="30" src="image/Reportes.png" /><span class="icon-rocket"></span>Reportes<span class="caret icon-arrow-down6"></span></a>
                      <ul class="children">
						<li><a href="frmRepote.aspx" >Validación Documentos</a></li>

					</ul>
                </li> 
                
                
               
				<li runat="server" id="adm" class="submenu">
					<a href="#"><img hspace="5" vspace="5" style="float: left;" height="40" width="30" src="image/Usuarios.png" /><span class="icon-rocket"><span class="icon-rocket"></span>Adm. de ususarios<span class="caret icon-arrow-down6"></span></a>
					<ul class="children">
						<li><a href="frmCrearUsuario.aspx">Creación de usuarios<span class="icon-dot"></span></a></li>
        <li><a href="frmConsultaUsuarios.aspx">Consulta de usuarios<span class="icon-dot"></span></a></li>
					</ul>
				</li>
				
			</ul>
		</nav>
	</header>
        <br />
    

    <form id="form1" runat="server">
           <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <Triggers>
 <asp:PostBackTrigger  ControlID="btncargar"  />
      <asp:PostBackTrigger  ControlID="btnRegresar"  />
      <asp:PostBackTrigger  ControlID="btnmarcar"  />
       <asp:PostBackTrigger  ControlID="btndescargar"  />
 </Triggers>
 <ContentTemplate>        
      <div  class="portada" >  
           <div class="container" id="contenedor" style="margin-left:18%;">


             <div style="float:left;">

                    <table>
                 <tr>
                     <td>
         <table >

             <tr>
                 <td>
                    <asp:Button ID="btnmarcar" CssClass="btn btn-primary btn-block" width="180px" runat="server" Text="Seleccionar Todos" OnClick="btnmarcar_Click"  />
                 </td>
                  <td>
                      <asp:Button ID="btndescargar" style="margin-left:5px;" CssClass="btn btn-primary btn-block" width="180px"   runat="server" Text="Desmarcar Todo" OnClick="btndescargar_Click"  />
                 </td>
             </tr>
         </table>

                     </td>
                 </tr>
             </table>
             </div>
                
 <div style="float: left;margin-left: 740px;width:400px;" >
          
          
          </div>
                
	<div style="float: left;width:700px;height: 570px; overflow: scroll"">
       
       <asp:GridView ID="Gvaudit"   runat="server" Width="100%" AutoGenerateColumns="False" 
                        EmptyDataText="No se han agregado registros!!"  
                         ForeColor="#0063ab" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
                        <Columns>
                         <asp:TemplateField HeaderStyle-BackColor="#0063ab" HeaderText="Marcar" >
                                <ItemTemplate >
                                     <asp:Literal ID="ltr_doc" runat="server" Visible="false" Text='<%# Eval("ITEMTYPENUM") %>'></asp:Literal>
                                      <asp:CheckBox ID="chkAccept" runat="server" EnableViewState="true" Checked="false"/>
                                </ItemTemplate>
                             <ItemStyle HorizontalAlign="center" ForeColor="#004683" Width="50px" />
                         </asp:TemplateField>

                            <asp:BoundField DataField="ITEMTYPENAME" ControlStyle-CssClass="Desc" HeaderText="Documentos"  HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White" >
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683" Width="240px" />
                            </asp:BoundField>

                        </Columns>

                    </asp:GridView>

	</div>   
          
        <div style="float: left;width:100px;">

                             <table style="float: left;margin-left:40px;width:400px;">

                                 <tr>
                                     <td>
                                         <strong style="color:#0063ab">Busqueda por :</strong>
                                     </td>
                                 </tr>
                                    <tr>
                                     <td>
                                            <asp:CheckBox ID="ckCedula" ValidationGroup="ckgroup" runat="server" Visible="false" AutoPostBack="true"  /> <strong>Cédula: </strong>
                                     </td>
                                         <td>
                                              <input type="text" id="txtcedula" runat="server" style="width:100%" autocomplete="off" tabindex="1" />
                                         </td>
                                     </tr>
                                 

                                 </tr>


                                       <tr>
                                     <td>
                                            <asp:CheckBox ID="ckNss" ValidationGroup="ckgroup" runat="server" Visible="false" AutoPostBack="true"  /> <strong>Nss :</strong>
                                     </td>
                                      <td>
                                         <input type="text" id="txtnss" runat="server" style="width:100%" autocomplete="off"  tabindex="1" />
                                      </td>
                                     </tr>

                                 <tr>
                                     <td>
                                            <asp:CheckBox ID="ckafiliado" ValidationGroup="ckgroup" runat="server" Visible="false" AutoPostBack="true" /> <strong>No. de Afiliado :</strong>
                                     </td>
                                      <td>
                                              <input type="text" id="txtafiliado" runat="server" style="width:100%" autocomplete="off"  tabindex="1" />
                                         </td>
                                     </tr>
                                  <tr>
                                        <td>
                                            <asp:CheckBox ID="ckradicacion" ValidationGroup="ckgroup" runat="server" Visible="false" AutoPostBack="true"  /> <strong>No. de Radicación :</strong>
                                     </td>
                                     <td>
                                              <input type="text" id="txtradicacion" runat="server"  style="width:100%" autocomplete="off" tabindex="1"  />
                                         </td>
                                      </tr>
                                 <tr>
                                      <td>
                                         
                                            <asp:CheckBox ID="ckpoliza" ValidationGroup="ckgroup" Visible="false" runat="server" AutoPostBack="true"  /> <strong>No. de póliza :</strong>
                                     </td>
                                                                          <td>
                                              <input type="text" id="txtpoliza" runat="server" autocomplete="off" style="width:100%"  tabindex="1"  />
                                         </td>
                                      </tr>
            
                             </table>

            </div>
              
             <br />


      <div style="float:left;margin-left:72px;margin-top:15px;">

                    <table>
                 <tr>
                     <td>
         <table >

             <tr>
                 <td>
                    <asp:Button ID="btncargar" CssClass="btn btn-primary btn-block" width="180px" runat="server" Text="Procesar" MaintainScrollPositionOnPostback="false" OnClick="btncargar_Click"  OnClientClick="javascript:showWait();"   />
                 </td>
                  <td>
                      <asp:Button ID="btnRegresar" style="margin-left:5px;" CssClass="btn btn-primary btn-block" width="180px"  MaintainScrollPositionOnPostback="false" runat="server" Text="Regresar" OnClick=" btnRegresar_Click"  />
                 </td>
             </tr>
         </table>

                     </td>
                 </tr>
             </table>
             </div>



                </div>
          </div>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" dynamiclayout="true">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">


                     <h1 style="color:#004683">Procesando Datos...</h1>
                 <img id="procesando_gif" border="1" height="100%" width="100%" src="" /> 
                </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
     </ContentTemplate>
             
 </asp:UpdatePanel>

        </form>

      <br />
        	<footer>

         <div class="footer_bar" style="float:left;">
          </div>


	</footer>
</body>
</html>