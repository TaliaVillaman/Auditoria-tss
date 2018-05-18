<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCrearUsuario.aspx.cs" Inherits="AuditoriaTSS.frmCrearUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  <title>Humanos</title>
    <link href="css/Style.css" rel="stylesheet" />
    <link href="css/switchery.min.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.2.min.js"></script>
    <script src="/js/jquery-ui.js"></script>
    <script src="/js/jquery.js"></script>


</head>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#btn_regresar').click(function () {
                window.location.href = "Frmmenu.aspx";
            });
           
            $('#btnenviar').click(function () {


                var nombre = $('#txtnombre').val();
                var empresa = $('#txtempresa').val();
                var contacto = $('#txtcontacto').val();
                var correo = $('#txtcorreo').val();
                var usuario = $('#txtuser').val();
                var pass = $('#txtpass').val();
                var selectperfil = document.getElementById('idperfil');
                if (selectperfil.selectedIndex >= 0) {
                    var idperfil = selectperfil.selectedIndex;
                }
                if (nombre != "" && empresa != "" ) {
                    $.ajax({
                        type: "POST",
                        url: "frmCrearUsuario.aspx/InsertarRegistro",
                        data: '{nombre: "' + nombre + '",empresa: "' + empresa + '",contacto: "' + contacto + '",correo: "' + correo + '",idperfi: "' + idperfil + '",usuario: "' + usuario + '",pass: "' + pass + '"}',
                        contentType: "application/json; charset=utf-8?",
                        dataType: "json",
                        success: function (response) {

                            alert('Registro insertado satisfactoriamente!')
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                            alert(errorThrown);
                        }
                    });
                }
                else {
                    alert('Favor de completar los datos requeridos')
                }

                });

                });
    </script>

<body>



    <form id="form1" runat="server">

        	<div class="container-login100-form-btn">
        
                <input id="btn_regresar" class="login100-form-btn" type="button" value="Menú anterior" />
		
					</div>
            
        <br />
        <div class="col-md-3" id="logo">
            <a href="/"> <img id="ctl00_x63829de2201a4365a3904788f682d0a3" alt="Volver al sitio" src="/image/logo_ARSH.png"></a>
        </div>
        </br>
        <div class="col-2">
            <label>Nombre
                <input type="text" id="txtnombre" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducior Nombre" tabindex="1" />
            </label>
        </div>
        <div class="col-2">
            <label>Departamento
                 <input type="text" id="txtempresa" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducir empresa" tabindex="2" />
            </label>
        </div>
  
  <div class="col-3">
    <label>
     Conctacto
     <input type="text" id="txtcontacto" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducir conctacto" tabindex="3" />

    </label>
  </div>
  <div class="col-3">
    <label>
      Correo Electronico
      <input type="text" id="txtcorreo" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducir correo" tabindex="4" />
    <span id="emailOK"></span>
    </label>
  </div>
  <div class="col-3">
    <label>
      Perfil

<select name="idperfil" id="idperfil" tabindex="5">
	<option value="0">Administrador</option>
	<option value="1">Operador</option>
	<option value="2">Consultor</option>
</select>

      
    </label>
  </div>


       <div class="col-3">
    <label>
      Asignacion de usuario

      <input  id="txtuser" type="text" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducir usuario" tabindex="6" />

    </label>
  </div>

<div class="col-3">
    <label>
      Asignacion de contrasena
      <input  id="txtpass" type="password" style="border: none; width: 100%; height: 17px; display: inline; background: #eee;" placeholder="Introducir contrasena" tabindex="7" />
    </label>
  </div>


  <div class="col-submit">
    <button class="submitbtn" id="btnenviar"  >Crear</button>
  </div>

    </form>

</body>
</html>
