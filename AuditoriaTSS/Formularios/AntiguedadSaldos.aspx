<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntiguedadSaldos.aspx.cs" Inherits="AimgosWeb.Formularios.AntiguedadSaldos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Antiguedad Saldos</title>

            <link href="/css/cssloguin.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.2.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <link href="/css/bootstrap.css" rel="stylesheet" />
    <script src="/js/jquery-ui.js"></script>
    <script src="/js/jquery.js"></script>

    <script src="js/canvasjs.min.js"></script>

      <script type="text/javascript">
          $(document).ready(function () {

              ocultarSidebar();
              $('#btn_regresar').click(function () {
                  window.location.href = "/Pages/Mis-Herramientas.aspx";
              });
              Date.toString();

              var fdesde = $('#ctl00_PlaceHolderMain_dtfechainicial_dtfechainicialDate').val();
              var fhasta = $('#ctl00_PlaceHolderMain_dtfechafinal_dtfechafinalDate').val();

              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8?",
                  data: '{desde:"' + fdesde + '",hasta: "' + fhasta + '"}',
                  url: "/_layouts/15/ConsultaCheque/ConsultaHistorico.aspx/buscar_datos",
                  dataType: "json",
                  success: function (response) {
                      $('#cuerpo').html('');
                      $('#cuerpo').append(response.d);

                      $('#example').DataTable({
                          "language": {
                              "sProcessing": "Procesando...",
                              "sLengthMenu": "Mostrar _MENU_ registros",
                              "sZeroRecords": "No se encontraron resultados",
                              "sEmptyTable": "Ningún dato disponible en esta tabla",
                              "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                              "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                              "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                              "sInfoPostFix": "",
                              "sSearch": "Criterio de busqueda por cheque o beneficiario:",
                              "sUrl": "",
                              "sInfoThousands": ",",
                              "sLoadingRecords": "Cargando...",
                              "oPaginate": {
                                  "sFirst": "Primero",
                                  "sLast": "Último",
                                  "sNext": "Siguiente",
                                  "sPrevious": "Anterior"
                              },
                              "oAria": {
                                  "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                                  "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                              }
                          }
                      });
                  },
                  error: function (XMLHttpRequest, textStatus, errorThrown) {
                      alert(textStatus);
                      alert(errorThrown);
                  }
              });



              $('.btn_dialog_busqueda').click(function () {



                  $.ajax({
                      type: "POST",
                      contentType: "application/json; charset=utf-8?",
                      data: '{desde:"' + fdesde + '",hasta: "' + fhasta + '"}',
                      url: "/_layouts/15/ConsultaCheque/ConsultaHistorico.aspx/buscar_datos",
                      dataType: "json",
                      success: function (response) {
                          $('#cuerpo').html('');
                          $('#cuerpo').append(response.d);

                          $('#example').DataTable();

                      },
                      error: function (XMLHttpRequest, textStatus, errorThrown) {
                          alert(textStatus);
                          alert(errorThrown);
                      }
                  });
              });


          });

    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    


          <div class="container tablaAprobacion">

            <table id="example" class="table table-striped" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Cheque</th>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Beneficiario</th>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Moneda</th>
                        <th style="text-align: right; padding-right:22px !important; vertical-align:middle;">Monto</th>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Fecha emisión </th>
                        <th style="text-align: left; padding-right:22px !important; max-width:83px;">Fecha entrega caja</th>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Usuario</th>
                        <th style="text-align: left; padding-right:22px !important; max-width:108px; vertical-align:middle;">Fecha de entrega al suplidor</th>
                        <th style="text-align: left; padding-right:22px !important; vertical-align:middle;">Usuario caja</th>

                    </tr>
                </thead>

                <tbody id="cuerpo">
                </tbody>

            </table>


        </div>
    </div>
    </form>
</body>
</html>
