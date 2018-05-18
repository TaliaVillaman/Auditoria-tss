<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPerfilUsuario.aspx.cs" Inherits="AuditoriaTSS.frmPerfilUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
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
