<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLoguin.aspx.cs" Inherits="AuditoriaTSS.FrmLoguin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=10" />
    <meta charset="utf-8"/>
	<title>Loguin </title>
      <link rel="shortcut icon" href="image/favicon.png"/>
<link rel="apple-touch-icon" href="image/apple-touch-icon.png"/>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
     <link href="css/EstiloHumano.css" rel="stylesheet" />
     <script src="js/sweetalert.min.js"></script>


        <style>
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


<script type="text/javascript">


    $(document).ready(function () {

        var iduser;
        var idpass;
       
        $('#txtcontrasena').on('keypress', function (e) {
            if (e.which == 13) {
              

                iduser = $('#txtusuario').val();
                idpass = $('#txtcontrasena').val();
                if (iduser != "" && idpass != "") {
                    $.ajax({
                        type: "POST",
                        url: "FrmLoguin.aspx/validausuario",
                        data: '{user: "' + iduser + '",pass: "' + idpass + '"}',
                        contentType: "application/json; charset=utf-8?",

                        dataType: "json",
                        success: function (response) {
                            var validar = response.d;

                            if (validar != '') {
                                window.location.href = "frmMenu.aspx?id=" + validar + "";

                            }
                            else {

                                    swal("Auditoria TSS","Usuario de Dominio/Perfil invalido", "warning");
                                    return;
                             }

                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                            alert(errorThrown);
                        }
                    })
                }

                else {

                    swal("Auditoria TSS", "Favor digitar usuario y contraseña!!", "info");
                    return;
                }


            }
        });

        $('#btn_acceder').click(function () {

            iduser = $('#txtusuario').val();
            idpass = $('#txtcontrasena').val();
            if (iduser != "" && idpass != "") {
                $.ajax({
                    type: "POST",
                    url: "FrmLoguin.aspx/validausuario",
                    data: '{user: "' + iduser + '",pass: "' + idpass + '"}',
                    contentType: "application/json; charset=utf-8?",

                    dataType: "json",
                    success: function (response) {
                        var validar = response.d;

                        if (validar != '') {
                            window.location.href = "frmMenu.aspx?id=" + validar + "";

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
                })
            }

            else {

                swal("Auditoria TSS", "Favor digitar usuario y contraseña!!", "info");
                return;
            }
        })
   
     });

    </script>

<body>
	

       <header>

	<div class="menu_bar" style="float:left;">
              <a href="www.humano.com.do"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="image/logo_ARSH.png" style="margin-right:1500px;margin-top:-20px;height:83px;margin-bottom:-17px;"></a>
        	<a href="#" class="menu_bar-bt-menu"  style="margin-left: 10px;margin-top: -50px;"  ></a>
      <div class="menu_bar_2" style="float:left;">
          </div>
        </div>
 
	</header>
  

    <div class="login-form">
    <form action="/examples/actions/confirmation.php" method="post">
        <h4 class="text-center" style="color:#808080;" >Inicio de Sección</h4>       
        <div class="form-group">
            <input type="text" id="txtusuario" runat="server" class="form-control" placeholder="Usuario" required="required">
        </div>
        <div class="form-group">
            <input type="password" id="txtcontrasena"   runat="server" class="form-control" placeholder="Pasword" required="required">
        </div>
        <div class="form-group">
            <button id="btn_acceder" type="button"  class="btn btn-primary btn-block">Login</button>
        </div>
        <div class="clearfix">
            <label  style="color:#808080;">©Soporte - Tel.(809) 476-3696</label>
           
        </div>        
    </form>

</div>

    
    	<footer class="footer_loguin" >
   
	</footer>

</body>
</html>
