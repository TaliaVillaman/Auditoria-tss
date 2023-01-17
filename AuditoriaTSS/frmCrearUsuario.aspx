<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCrearUsuario.aspx.cs" Inherits="AuditoriaTSS.frmCrearUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>
  <title>Perfiles</title>
      <link rel="shortcut icon" href="image/favicon.png"/>
<link rel="apple-touch-icon" href="image/apple-touch-icon.png"/>
     <link href="css/EstiloHumano.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/sweetalert.min.js"></script>


    <style>
    .container {
        position: relative !important;

        height: 587px !important;
    }
</style>
       <script type="text/javascript">




        $(document).ready(function () {
            var object = document.getElementById("contenedor");
        var contador = 1;
            $('.menu_bar').click(function () {
                if (contador == 1) {
                    $('nav').animate({
                        left: '0'
                    });
                    //object.style.marginLeft = '20%';
                    contador = 0;
                } else {
                    contador = 1;
                    $('nav').animate({
                        left: '-100%'
                    });
                    //object.style.marginLeft = '0%';
                }
            });

            // Mostramos y ocultamos submenus
            $('.submenu').click(function () {
                $(this).children('.children').slideToggle();
            });
            $('.btn_regresar').click(function () {
                window.location.href = "Frmmenu.aspx";
            });


            $('#btn_acceder').click(function () {

                var usuario = $('#txtuser').val();
                var selectperfil = document.getElementById('idperfil');
                var pass = '' ;
                if (selectperfil.selectedIndex >= 0) {
                    var idperfil = selectperfil.selectedIndex;
                }
                if (usuario != "" ) {
                    $.ajax({
                        type: "POST",
                        url: "frmCrearUsuario.aspx/InsertarRegistro",
                        data: '{idperfil: "' + idperfil + '",usuario: "' + usuario + '",pass:"' + pass + '"}',
                        contentType: "application/json; charset=utf-8?",
                        dataType: "json",
                        success: function (response) {
                            var validar = response.d;

                            if (validar != '') {
                                swal("Auditoria TSS", "Registro insertado satisfactoriamente!", "success");

                            }
                            else {
                                swal("Auditoria TSS", "Usuario invalido", "warning");
                                return;
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                            alert(errorThrown);
                        }
                    });
                }
                else {
                    swal("Favor de completar los datos requeridos", "info");
           
                }

                });

                });
    </script>
</head>

 

<body>

  <header>

	<div class="menu_bar" style="float:left;">
              <a href="frmmenu"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="image/logo_ARSH.png" style="margin-right:1500px;margin-top:-20px;height:83px;margin-bottom:-17px;"></a>
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
    <div  class="portada" > 
   
     <div  class="container" id="contenedor" >
        <div class="login-form">
           
    <form action="/examples/actions/confirmation.php" method="post">

        <div class="form-group">
             <strong style="color:#0063ab">Usuario </strong>
            <input type="text" id="txtuser" class="form-control"  required="required"/>
        </div>

        <div class="form-group">
             <strong style="color:#0063ab">Perfil </strong>
                            <select name="idperfil" id="idperfil" class="form-control" tabindex="2">
	                <option value="0">Administrador</option>
	                <option value="1">Operador</option>
	                <option value="2">Consultor</option>
                </select>
        </div>
        <div class="form-group">
            <button id="btn_acceder" type="button"  class="btn btn-primary btn-block">Crear </button>
        </div>
        <div class="clearfix">
           
           
        </div>        
    </form>

</div>
         </div>
        </div>
        	<footer>

         <div class="footer_usuario" style="float:left;">
          </div>


	</footer>
</body>
</html>
