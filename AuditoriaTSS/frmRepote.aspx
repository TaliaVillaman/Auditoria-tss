<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRepote.aspx.cs" Inherits="AuditoriaTSS.frmRepote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>
    <title>Reportes</title>
    <link rel="shortcut icon" href="image/favicon.png"/>
    <link rel="apple-touch-icon" href="image/apple-touch-icon.png"/>
     <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
      <link href="css/EstiloHumano.css" rel="stylesheet" />

    <link href="js/jqry.css" rel="stylesheet" />
        <script type="text/javascript" src="js/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
     <script src="js/sweetalert.min.js"></script>

    <script src="/js/jquery-ui.js"></script>



      <script type="text/javascript" >

          var contador = 1;

          function mesaggeExito(mensaje) {
              swal("Auditoria TSS", mensaje, "success");
          }
          function mesaggeinfo(mensaje) {
              swal("Auditoria TSS", mensaje, "info");
          }

          function mesaggealerta(mensaje) {
              swal("Auditoria TSS", mensaje, "warning");
          }


          $(function () {
              var object = document.getElementById("contenedor");
              $('.menu_bar').click(function () {
                  if (contador == 1) {
                      $('nav').animate({
                          left: '0'

                      });
                      object.style.marginLeft = '20%';
                      contador = 0;
                  } else {
                      contador = 1;
                      $('nav').animate({
                          left: '-100%'
                      });

                      object.style.marginLeft = '0%';

                  }
              });

              // Mostramos y ocultamos submenus
              $('.submenu').click(function () {
                  $(this).children('.children').slideToggle();
              });
          });

          $(function () {
            

              $("#dtfechainicial").datepicker({
                  language: 'en',
                  pick12HourFormat: true,
                  dateFormat: 'dd-mm-yy'
              });

              $("#dtfechafinal").datepicker({
                  language: 'en',
                  pick12HourFormat: true,
                  dateFormat: 'dd-mm-yy'
              });
          });


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
				<li><a href="frmmenu.aspx"><img hspace="5" vspace="5" style="float: left;opacity: 0.9;" height="40" width="30" src="image/inicio.png" /><label style="margin-left:10px;" >Inicio</label></a></li>
				
                <li runat="server" id="proces" class="submenu" >
                    <a href="#"><img hspace="5" vspace="5" style="float: left;opacity: 0.9;" height="40" width="30" src="image/procesos.png" /><span class="icon-rocket"></span>Procesos<span class="caret icon-arrow-down6"></span></a>
					<ul class="children">
						<li ><a href="frmConsultaArchivo.aspx">Proceso Masivo</a></li>
                        <li ><a href="FrmConsultaCliente.aspx">Proceso Masivo Póliza</a></li>
						<li><a href="FrmArchivosEspecificos.aspx">Proceso Específico</a></li>
					</ul>
		           </li>
                 <li runat="server" id="rept" class="submenu" >
                      <a href="#"><img hspace="5" vspace="5" style="float: left; opacity: 0.9;" height="40" alt="Notebook" width="30" src="image/Reportes.png" /><span class="icon-rocket"></span>Reportes<span class="caret icon-arrow-down6"></span></a>
                      <ul class="children">
						<li><a href="frmRepote.aspx" >Validación Documentos</a></li>
					</ul>
                </li> 
                <li runat="server" id="adm" class="submenu">
					<a href="#"><img hspace="5" vspace="5" style="float: left;opacity: 0.9;" height="40" width="30" src="image/Usuarios.png" /><span class="icon-rocket"><span class="icon-rocket"></span>Adm. de ususarios<span class="caret icon-arrow-down6"></span></a>
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
          <div  class="portada" > 

    <div class="container" id="contenedor" style="margin-left:20%;">
       <h4>Parametros de busqueda:</h4>
                    <table>
                         <tr>
                         <td  >
                            <p>Fecha desde:</p>
                         <input type="text" id="dtfechainicial" class="form-control"  runat="server"   autocomplete="off" tabindex="1" >
                         </td>
                          <td >
                            <p>Fecha hasta:</p>
                              <input type="text" id="dtfechafinal" class="form-control"  runat="server"   autocomplete="off" tabindex="2" >
                         </td>
                             </tr>
                        <tr>

                                     <td >
                            <p>No Audioria:</p>
                                         <input type="text" id="txtaditoria" class="form-control"  runat="server" autocomplete="off"   tabindex="3" >
                    
                         </td>
                             </tr>
                     
                        <tr  style="margin-left:1251px; ">
      
 
                            <td>
                                <p></p>
                                <asp:Button ID="btnbuscar"  CssClass="btn btn-primary btn-block" width="180px" OnClick="btnbuscar_Click" AutoPostback="false"  runat="server" Text="Buscar"/>
                            </td>
                            <br />
                             <td>
                                <p></p>
                                <asp:Button ID="btnGenerar"  CssClass="btn btn-primary btn-block" width="180px" OnClick="btnGenerar_Click" AutoPostback="false" runat="server"  Text="Generar Reporte"/>
                            </td>
                        </tr>

                    </table>

        <asp:GridView ID="Gvaudit"   runat="server" Width="100%" AutoGenerateColumns="False" 
                        EmptyDataText="No se han agregado registros!!"   OnPageIndexChanging="Gvaudit_PageIndexChanging"  PageSize="10"
                        AllowPaging="true"  CellPadding="4" ForeColor="#0063ab" GridLines="None" OnRowCommand="Gvaudit_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  >
                        <Columns>

                            <asp:BoundField DataField="NoAuditoria" ControlStyle-CssClass="Desc"  HeaderText="NO. AUDITORIA" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White">
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683"  Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Usuario" ControlStyle-CssClass="Desc" HeaderText="USUARIO" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"    >
                                <ItemStyle HorizontalAlign="Left" Width="300px" ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaPorceso" ControlStyle-CssClass="Desc" HeaderText="FECHA PROCESO" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"    DataFormatString="{0:dd-MM-yyyy}" >
                                <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="#004683"/>
                            </asp:BoundField>
                            
                       
                              <asp:buttonfield buttontype="Link" ControlStyle-Width="120px" HeaderStyle-BackColor="#0063ab" commandname="Add" text="Generar Reporte"/>
               
                        </Columns>
                 <FooterStyle BackColor="#FFFFCC" ForeColor="#0063ab" />

                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    </asp:GridView>




         <asp:GridView ID="gvdetalle"   runat="server" Width="100%" AutoGenerateColumns="false" 
                        EmptyDataText="No se han agregado registros!!"   OnPageIndexChanging="gvdetalle_PageIndexChanging"  PageSize="10"
                        AllowPaging="true"  CellPadding="4" ForeColor="#0063ab" GridLines="None"  CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  >
                        
              <Columns>

                            <asp:BoundField DataField="NSS_TITULAR" ControlStyle-CssClass="Desc"  HeaderText="NSS TITULAR" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White">
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683"  Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Titular" ControlStyle-CssClass="Desc" HeaderText="TITULAR" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"    >
                                <ItemStyle HorizontalAlign="Left" Width="300px" ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion_titular" ControlStyle-CssClass="Desc" HeaderText="CEDULA TITULAR" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   >
                                <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="#004683"/>
                            </asp:BoundField>
                            
                              <asp:BoundField DataField="NSS_DEPENDIENTE" ControlStyle-CssClass="Desc" HeaderText="NSS DEPENDIENTE" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"  >
                                <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="#004683"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPENDIENTE" ControlStyle-CssClass="Desc" HeaderText="DEPENDIENTE" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   >
                                <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="#004683"/>
                            </asp:BoundField>

                   <asp:BoundField DataField="IDENTIFICACION_DEP" ControlStyle-CssClass="Desc" HeaderText="CEDULA DEPENDIENTE" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   >
                                <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="#004683"/>
                            </asp:BoundField>
                       
                        </Columns>
                 <FooterStyle BackColor="#FFFFCC" ForeColor="#0063ab" />

                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    </asp:GridView>

                  </div>
</div>
     
    </form>

<footer>

         <div class="footer_reporte" style="float:left;">
          </div>


	</footer>
</body>
</html>
