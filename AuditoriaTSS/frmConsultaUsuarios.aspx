<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConsultaUsuarios.aspx.cs" Inherits="AuditoriaTSS.frmConsultaUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>
    <title>Consulta de prfiles</title>
      <link rel="shortcut icon" href="image/favicon.png">
<link rel="apple-touch-icon" href="image/apple-touch-icon.png">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
  <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-1.11.2.min.js"></script>
     <link href="css/EstiloHumano.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <link href="js/jquery-ui.min.css" rel="stylesheet" />
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

           $('.submenu').click(function () {
               $(this).children('.children').slideToggle();
           });
       }
       function showWait() {
           var max = 9 * 60 * 9999999;
           $get('UpdateProgress1').style.display = 'block';
           setTimeout(document.images['procesando_gif'].src = 'image/giphy.gif', max);

       }

       $(document).ready(function () {
           $('.btn_dialog').click(function () {
               var usuario = $(this).closest("tr").find('td:eq(0)').text();
               var selectperfil = document.getElementById('idperfil');
               var selectestatus = document.getElementById('idestatus');


               $.ajax({
                   type: "POST",
                   url: "/frmConsultaUsuarios.aspx/infoUsuario",
                   data: "{'id': '" + usuario + "'}",
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       $('#txt_usuario').val(response.d[0]);
                       selectperfil.selectedIndex = response.d[1];
                       selectestatus.selectedIndex =response.d[2];
                       $("#dialog").dialog({
                           modal: true,
                           height: "auto",
                           width: 800,
                           buttons: {

                               "Guardar": function () {
                                   if (selectperfil.selectedIndex >= 0) {
                                       var perfil = selectperfil.selectedIndex;
                                   }

                                   if (selectestatus.selectedIndex >= 0) {
                                       var estatus = selectestatus.selectedIndex;
                                   }
                                   var usuario = $('#txt_usuario').val();
                           
       
                                   //Actualizar Información
                                   $.ajax({
                                       type: "POST",
                                       contentType: "application/json; charset=utf-8?",
                                       data: '{usuario:"' + usuario + '",perfil: "' + perfil + '",estatus: "' + estatus + '"}',
                                       url: "/frmConsultaUsuarios.aspx/actualiza_datos_usuarios",
                                       dataType: "json",
                                       success: function (response) {

                                           swal("Auditoria TSS", "Datos actualizados satisfactoriamente!", "success");
                                           $("#dialog").dialog("close");

                                       },
                                       error: function (XMLHttpRequest, textStatus, errorThrown) {
                                           alert(textStatus);
                                           alert(errorThrown);
                                       }
                                   });

                               },

                               "Salir": function () {
                                   $("#dialog").dialog("close");
                               }
                           }
                       });
                   } 
               });
        
           })
       })
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
              <a href="frmloguin.aspx"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="image/logo_ARSH.png" style="margin-right:1500px;margin-top:-20px;height:83px;margin-bottom:-17px;"></a>
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

    <div class="login-form"  id="dialog"   style="display:none;width:500px;">
    <form action="/examples/actions/confirmation.php" method="post">
        <h4 class="text-center" style="color:#808080;" >Consulta de usuarios</h4>       
        <div class="form-group">
            <strong style="color:#0063ab">Usuario </strong>
            <input type="text" id="txt_usuario" class="form-control" placeholder="Usuario" />
        </div>
        <div class="form-group">
             <strong style="color:#0063ab">Perfil </strong>
            <strong style="color:#0063ab">Perfil </strong>
                   <select name="idperfil" id="idperfil" class="form-control" tabindex="2">
	                <option value="0">Administrador</option>
	                <option value="1">Operador</option>
	                <option value="2">Consultor</option>
                </select>
        </div>
        <div class="form-group">
             <strong style="color:#0063ab">Estatus </strong>
             <select name="idestatus" id="idestatus" class="form-control" tabindex="2">
	                <option value="0">Activo</option>
	                <option value="1">Inactivo</option>
                </select>
        </div>
    </form>

</div>
    <form id="form1" runat="server">
         <div class="portada">
       <div  class="container" id="contenedor" style="margin-left:20%;" >

        <asp:GridView ID="Gvaudit"    runat="server" Width="700px" AutoGenerateColumns="False" 
                        EmptyDataText="No se han agregado registros!!"  
                         ForeColor="#0063ab" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>

                            <asp:BoundField DataField="usuario" ControlStyle-CssClass="Desc" HeaderText="USUARIO" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White">
                                <ItemStyle HorizontalAlign="Left" ForeColor="#004683" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PERFIL" ControlStyle-CssClass="Desc" HeaderText="PERFIL" HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White"   >
                                <ItemStyle HorizontalAlign="Left"  ForeColor="#004683"/>
                            </asp:BoundField>

                             <asp:TemplateField HeaderStyle-BackColor="#0063ab" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <input id="btnGetTime"  class="btn_dialog"  type="button" style="font-size:12px; border:none;color:#4169E1;font:bold;background:transparent;margin-top:-5px;" value="Ver"   /> 
                          
                                </ItemTemplate>
                         </asp:TemplateField>

                        </Columns>
               
                    </asp:GridView>

           </div>
      </div>
    </form>
    <footer>
     <div class="footer_bar" style="float:left;">
          </div>
        </footer>
</body>
</html>
