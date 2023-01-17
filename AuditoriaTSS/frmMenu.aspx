<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMenu.aspx.cs" Inherits="AuditoriaTSS.frmMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>
     <title>Menu</title>
    <link rel="shortcut icon" href="image/favicon.png"/>
    <link rel="apple-touch-icon" href="image/apple-touch-icon.png"/>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
     <link href="css/EstiloHumano.css" rel="stylesheet" />


<script type="text/javascript">
    $(document).ready(main);

    var contador = 1;

    function main() {
        $('.menu_bar').click(function () {
            if (contador == 1) {
                $('nav').animate({
                    left: '0'
                });
                contador = 0;
            } else {
                contador = 1;
                $('nav').animate({
                    left: '-100%'
                });
            }
        });

        // Mostramos y ocultamos submenus
        $('.submenu').click(function () {
            $(this).children('.children').slideToggle();
        });
    }
</script>




        <style>

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
<body  >

    <header>

           
		<div class="menu_bar" style="float:left;">
              <a href="frmmenu.aspx"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="image/logo_ARSH.png" style="margin-right:1500px;margin-top:-20px;height:83px;margin-bottom:-17px;"></a>
			<a href="#" class="menu_bar-bt-menu" ><img border="0" height="40" width="30" src="image/icon-list2.png" style="margin-left: 230px;margin-top: -50px;" /></a>
		         <br />
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
     
    <form id="form1" runat="server" >
       
    <div  class="portada" >
	    <div  class="container" id="contenedor" style="margin-left:20%;">
	    </div>
    </div>
  </form>
     <div class="footer_menu" style="float:left;">
          </div>
</body>
</html>