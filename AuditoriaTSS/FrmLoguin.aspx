<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLoguin.aspx.cs" Inherits="AuditoriaTSS.FrmLoguin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
	<title>Humanos</title>

    <link href="css/animsition.min.css" rel="stylesheet" />
    <link href="css/daterangepicker.css" rel="stylesheet" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/util.css" rel="stylesheet" />
	<link href="css/main.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.2.min.js"></script>
    <script src="/js/jquery-ui.js"></script>
    <script src="/js/jquery.js"></script>
    
</head>

    

<script type="text/javascript">
    $(document).ready(function () {

        var iduser;
        var idpass;
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
                            alert('Usuario o Contraseña invalidos');
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
                alert('Favor digitar usuario y contrasena!!');
                return;
            }
        })
   
     });

    </script>

<body>
	
	<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<div class="login100-form-title" style="background-image: url(css/bg-01.jpg);">
	
                <span class="login100-form-title-1">
						Control de Acceso
					</span>
				</div>

				<form class="login100-form validate-form">
					<div class="wrap-input100 validate-input m-b-26" data-validate="Usuario requerido">
						<span class="label-input100">Usuario</span>
						<input class="input100" type="text" id="txtusuario" placeholder="Entrar usuario">
						<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input m-b-18" data-validate = "Contraseña requerida">
						<span class="label-input100">Contraseña</span>
						<input class="input100" type="password" id="txtcontrasena" placeholder="Entrar contrasena">
						<span class="focus-input100"></span>
					</div>
					<div class="container-login100-form-btn">
                        <input id="btn_acceder" class="login100-form-btn" type="button" value="Acceder" />
		
					</div>
				</form>
			</div>
		</div>
	</div>
	

</body>
</html>
