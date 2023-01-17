using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml;

using System.Runtime.CompilerServices;
using System.Collections;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace AuditoriaTSS
{
    public class ClsConsulta
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetUserNameA", ExactSpelling = true, SetLastError = true)]
        public static extern int GetUserName([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpBuffer, ref int nSize);

        public string GetUserName()
        {
            string text = new string(' ', 50);
            int num = 50;
            int userName = GetUserName(ref text, ref num);
            return text.Substring(0, text.IndexOf('\0'));
        }


        public void ProcesaBusquedaAfiliados_Ws_Bs(int codigoauditoria)
        {
            try
            {
                ClsConexion cncon = new ClsConexion();
                string queryString = string.Empty;
                queryString = "BEGIN P_BUSCA_PARMETROS_INFOPLAN(" + codigoauditoria + "); END;";
                cncon.Insert_update_Data_Ars(queryString);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void ProcesaBusquedaAfiliados_Adicional_Ws_Bs(int codigoauditoria)
        {
            try
            {
                ClsConexion cncon = new ClsConexion();
                string queryString = string.Empty;
                queryString = "BEGIN P_BUSCA_INFO_POL_RADIC(" + codigoauditoria + "); END;";
                cncon.Insert_update_Data_Ars(queryString);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public int ProcesaBusquedaImagenes_Ws_Bs(int CodigoAuditoria, string app)
        {
            ClsConexion cncon = new ClsConexion();

            string strqry = "";


            int result = 0;
            if (app == "RAD_POL")
            {
                strqry = "BEGIN HSI.P_BUSCA_IMAGENES_RADICACION (" + CodigoAuditoria + "); END;";
            }
            else if (app == "NSS_RAD")
            {
                strqry = "BEGIN HSI.P_BUSCA_IMAGENES_ADICIONAL (" + CodigoAuditoria + "); END;";
            }
            else
            {
                strqry = "BEGIN HSI.P_BUSCA_IMAGENES (" + CodigoAuditoria + "); END;";
            }

            try
            {
                result = cncon.Insert_update_Data(strqry);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
        public void transferir_sql_Ws_Bs(int codAuditoria, string app, string tipo)
        {

            if (app == "RAD_POL")
            {
                Transferir_data_sql_reporte_rad(codAuditoria, tipo);
            }
            else if (app == "NSS_RAD")
            {
                Transferir_data_sql_doc(codAuditoria, tipo);
                Transferir_data_sql_reporte(codAuditoria, tipo);
            }
            else
            {
                Transferir_data_sql_doc(codAuditoria, "");
                Transferir_data_sql_reporte(codAuditoria, "");
            }

          

        }
        public int CreaNuevaAuditoria_Ws_Bs(int TipoBusqueda, string FormaBusqueda)
        {
            int CodAuditoria;
            ClsConexion cncon = new ClsConexion();
            string queryString = "SELECT COD_AUDITORIA FROM hsi.AUDITORIA_TSS ORDER BY COD_AUDITORIA  DESC";
            DataTable dataTable = cncon.GetDatatable(queryString);
            if (dataTable.Rows.Count == 0)
            {
                CodAuditoria = 1;
            }
            else
            {
                CodAuditoria = Convert.ToInt32(dataTable.Rows[0]["COD_AUDITORIA"].ToString()) + 1;
            }
            string userName = GetUserName();
            insert_auditoria(CodAuditoria.ToString(), userName, TipoBusqueda, FormaBusqueda);

            return CodAuditoria;
        }
        public int validaExitimagenes_Ws_Bs(int cod_auditoria)
        {
            int vaida = 0;
            ClsConexion cncon = new ClsConexion();
            string queryString = "SELECT * FROM AUDITORIA_VAL_DOC  where COD_AUDITORIA=" + cod_auditoria + "";
            DataTable dataTable = cncon.GetDatatableSql(queryString);
            if (dataTable.Rows.Count == 0)
            {
                vaida = 0;
            }
            else
            {
                vaida = 1;
            }
            return vaida;
        }
        public int validaExitimagenesRad_Ws_Bs(int cod_auditoria)
        {
            int vaida = 0;
            ClsConexion cncon = new ClsConexion();
            string queryString = "SELECT * FROM AUDITORIA_REPORTE_RAD_POL where COD_AUDITORIA=" + cod_auditoria + "";
            DataTable dataTable = cncon.GetDatatableSql(queryString);
            if (dataTable.Rows.Count == 0)
            {
                vaida = 0;
            }
            else
            {
                vaida = 1;
            }
            return vaida;
        }
        public void ActualizarAuditoria_Ws_Bs(int codigoauditoria)
        {
            try
            {
                ClsConexion cncon = new ClsConexion();
                string queryString = string.Empty;
                string queryString2 = string.Empty;

                queryString = "UPDATE hsi.AUDITORIA_TSS SET FECHA_FIN = SYSDATE WHERE COD_AUDITORIA = " + codigoauditoria + "";
                cncon.Insert_update_Data(queryString);
                queryString2 = "UPDATE AUDITORIA SET FECHA_FIN = '" + DateTime.Now.ToString("yyyyMMdd") + "' WHERE COD_AUDITORIA = " + codigoauditoria + "";
                cncon.Insert_update_Data_Sql(queryString2);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void InsertDataDocs(DataTable dtResultDoc, string codigoauditoria)
        {
            ClsConexion cncon = new ClsConexion();
            foreach (DataRow row in dtResultDoc.Rows)
            {
                string queryString = "INSERT INTO hsi.AUDITORIA_DOC_REQ_TSS(COD_AUDITORIA, ITEMTYPENUM, ITEMTYPENAME) VALUES (" + codigoauditoria + ", " + row["ITEMTYPENUM"].ToString() + ", '" + row["ITEMTYPENAME"].ToString() + "')";
                cncon.Insert_update_Data(queryString);
            }
        }
        public DataTable getdocumentos()
        {
            DataTable dt = new DataTable();
            string queryString = "select * from doctype  ORDER BY ITEMTYPENAME";
            ClsConexion cncon = new ClsConexion();
            dt = cncon.GetDatatableSql(queryString);
            return dt;
        }
        public DataTable Validar_carpeta(int NumAuditoria)
        {
            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("Data");
            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append(" SELECT distinct  NSS_TITULAR,	NSS_DEPENDIENTE, NSS_TITULAR  NSSINDIVIDUAL,	AFILIADO_NO,IDENTIFICACION_TITULAR,POLIZA,RADICACION,ITEMNUM,ITENTYPENUM,	RUTA");
            stringBuilder.Append(" FROM AUDITORIA_VAL_DOC where SECUENCIA_DEP=0 and NSS_DEPENDIENTE=0 AND COD_AUDITORIA=" + NumAuditoria.ToString() + "");
            stringBuilder.Append(" union all");
            stringBuilder.Append(" SELECT distinct  NSS_TITULAR,	NSS_DEPENDIENTE, NSS_DEPENDIENTE NSSINDIVIDUAL,	AFILIADO_NO,IDENTIFICACION_TITULAR,POLIZA,RADICACION,ITEMNUM,ITENTYPENUM,	RUTA");
            stringBuilder.Append(" FROM AUDITORIA_VAL_DOC where NSS_DEPENDIENTE!=0 and SECUENCIA_DEP>0 AND COD_AUDITORIA=" + NumAuditoria.ToString() + " ");
            dt = cncon.GetDatatableSql(stringBuilder.ToString());
            return dt;
        }
        public DataTable Validar_carpeta_adi(int NumAuditoria)
        {
            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("Data");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" exec p_valida_carpeta_adicional " + NumAuditoria.ToString());
            dt = cncon.GetDatatableSql(stringBuilder.ToString());


            return dt;
        }
        private void Transferir_data_sql_doc(int cod_auditoria, string _tipoQry)
        {
            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("Data");
            StringBuilder stringBuilder = new StringBuilder();

            if (_tipoQry == "NSS_RAD")
            {
                stringBuilder.Append("   SELECT distinct  b.cod_auditoria,NVL(b.NSS,0) NSS_TITULAR,0 NSS_DEPENDIENTE,NVL(B.AFILIADO_NO,0) AFILIADO_NO,B.SECUENCIA_DEP,");
                stringBuilder.Append("   A.CED_ACT IDENTIFICACION_TITULAR, ");
                stringBuilder.Append("   B.POLIZA,B.RADICACION,NVL(d.ITEMNUM,0) ITEMNUM, NVL(D.itemtypenum,0) itemtypenum,d.RUTA  ");
                stringBuilder.Append("   FROM  hsi.AUDITORIA_BUSQUEDA_ADIC_TSS B left outer Join hsi.AUDITORIA_DOC_RESULT_TSS D   ");
                stringBuilder.Append("   on   B.COD_AUDITORIA = D.COD_AUDITORIA  ");
                stringBuilder.Append("   AND  B.nss =D.nss    ");
                stringBuilder.Append("   AND  B.SEcUENCIA_DEP= D.SECUENCIA  AND  B.NSS=D.NSS left outer join asegurado@onbase_per a on B.NSS = a.nss   ");
                stringBuilder.Append("   where  b.cod_auditoria=" + cod_auditoria.ToString());
            }
            else
            {
                stringBuilder.Append("  SELECT distinct B.COD_AUDITORIA,NVL(B.NSS_TITULAR,0) NSS_TITULAR,NVL(B.NSS_DEPENDIENTE,0) NSS_DEPENDIENTE ,  NVL(B.AFILIADO_NO,0) AFILIADO_NO,NVL(B.SECUENCIA_DEP,0) SECUENCIA_DEP,");
                stringBuilder.Append("  B.IDENTIFICACION_TITULAR,nvl((select nvl(valor,'') from hsi.auditoria_nss_tss where  cod_auditoria=b.cod_auditoria and nss_titular=b.nss_titular and cedula_tit=b.identificacion_titular and campo='POLIZA' and rownum<=1 ),'') POLIZA,");
                stringBuilder.Append("  nvl((select nvl(to_number(valor),0) from hsi.auditoria_nss_tss where  cod_auditoria=b.cod_auditoria and nss_titular=b.nss_titular and cedula_tit=b.identificacion_titular and campo='RADICACION' and rownum<=1 ),'') RADICACION,");
                stringBuilder.Append("  NVL(d.ITEMNUM,0) ITEMNUM, NVL(D.itemtypenum,0) itemtypenum,d.RUTA ");
                stringBuilder.Append("  FROM  hsi.AUDITORIA_BUSQUEDA_TSS B left  Join hsi.AUDITORIA_DOC_RESULT_TSS D  ");
                stringBuilder.Append("  on   B.COD_AUDITORIA = D.COD_AUDITORIA ");
                stringBuilder.Append("  AND  B.AFILIADO_NO =D.AFILIADO_NO   ");
                stringBuilder.Append("  AND  B.SEcUENCIA_DEP= D.SECUENCIA  AND  B.NSS_TITULAR=D.NSS  ");
                stringBuilder.Append("  where b.cod_auditoria=" + cod_auditoria.ToString());

            }

      
            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbOnbase"]))
            {
                OracleCommand command = new OracleCommand(stringBuilder.ToString(), cn);
                cn.Open();

                OracleDataReader row = command.ExecuteReader();

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        string qry = " INSERT INTO AUDITORIA_VAL_DOC  VALUES (" + row["cod_auditoria"].ToString() + ", " + row["NSS_TITULAR"].ToString() + ", " + row["NSS_DEPENDIENTE"].ToString() + " ," + row["AFILIADO_NO"].ToString() + ", " + row["SECUENCIA_DEP"].ToString() + ", '" + row["IDENTIFICACION_TITULAR"].ToString() + "', '" + row["POLIZA"].ToString() + "', '" + row["RADICACION"].ToString() + "', " + row["ITEMNUM"].ToString() + ", " + row["itemtypenum"].ToString() + ",'" + row["RUTA"].ToString() + "')";
                        cncon.Insert_update_Data_Sql(qry);
                    }
                }
                cn.Close();

            }

        }

        private void Transferir_data_sql_reporte(int cod_auditoria, string _sqlTipo)
        {

            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("DataReport");
            StringBuilder stringBuilder = new StringBuilder();
            if (_sqlTipo == "NSS_RAD")
            {

                stringBuilder.Append(" SELECT B.COD_AUDITORIA,NVL(B.NSS,0) NSS_TITULAR,a.ced_act IDENTIFICACION_TITULAR,(a.pri_nom || ' ' || a.Pri_ape) TITULAR,");
                stringBuilder.Append(" NVL(dep.NSS,0) NSS_DEPENDIENTE ,dep.ced_act IDENTIFICACION_DEP,(dep.pri_nom || ' ' || dep.Pri_ape) DEPENDIENTE,p.descripcion PARENTEZCO,");
                stringBuilder.Append(" ROUND(TO_NUMBER(MONTHS_BETWEEN(dep.fec_nac,SYSDATE)*-1/12),0) EDAD,");
                stringBuilder.Append(" ' ' GENERALES, '' COMENTARIOS, CASE a.ESTATUS WHEN 5 THEN 'VIGENTE' WHEN 6 THEN 'CANCELADO'  END AS ESTATUS, '' TRASPASO_UNIFICACION,'' TRASPASO_ORDINARIO,");
                stringBuilder.Append("  b.DIRECTOR, b.COD_GERENTE , B.GERENTE,b.CLIENTE_NOMBRE,");
                stringBuilder.Append(" b.RNC_CLIENTE ,b.CLIENTE_NOMBRE RNC_NOMBRE,B.ORIGEN_AFILIACION, NVL(itemtypenum,0)  AS ITEMTYPENUM ,user USUARIO,SYSDATE FECHA ,NVL(B.AFILIADO_NO,0) AFILIADO_NO  ");
                stringBuilder.Append(" FROM     hsi.AUDITORIA_BUSQUEDA_ADIC_TSS B LEFT OUTER JOIN hsi.AUDITORIA_DOC_RESULT_TSS d ");
                stringBuilder.Append(" ON      B.nss=D.nss  ");
                stringBuilder.Append(" AND     B.COD_AUDITORIA=D.COD_AUDITORIA  ");
                stringBuilder.Append(" AND     B.SEcUENCIA_DEP= D.SECUENCIA inner  JOIN Asegurado@onbase_per A  ");
                stringBuilder.Append(" ON      B.AFILIADO_NO=A.CODIGO inner join dependiente@onbase_per dep on a.codigo=dep.asegurado   inner join Parentezco@onbase_per p ");
                stringBuilder.Append(" on      dep.parentezco=p.codigo   ");
                stringBuilder.Append(" where   B.COD_AUDITORIA=" + cod_auditoria.ToString());
            }
            else
            {

                stringBuilder.Append(" SELECT B.COD_AUDITORIA,NVL(B.NSS_TITULAR,0) NSS_TITULAR,B.IDENTIFICACION_TITULAR,(B.NOMBRES_TITULAR || ' ' || B.APELLIDOS_TITULAr) TITULAR,");
                stringBuilder.Append(" NVL(B.NSS_DEPENDIENTE,0) NSS_DEPENDIENTE,B.IDENTIFICACION_DEP,(B.NOMBRES_DEP || ' ' || B.APELLIDOS_DEP) DEPENDIENTE,B.PARENTEZCO,ROUND(TO_NUMBER(MONTHS_BETWEEN(b.fecha_nacimiento_dep,SYSDATE)*-1/12),0) EDAD,");
                stringBuilder.Append(" ' ' GENERALES, '' COMENTARIOS, CASE a.ESTATUS WHEN 5 THEN 'VIGENTE' WHEN 6 THEN 'CANCELADO'  END AS ESTATUS, '' TRASPASO_UNIFICACION,'' TRASPASO_ORDINARIO,");
                stringBuilder.Append(" b.DIRECTOR, NVL(B.COD_GERENTE,0) COD_GERENTE, B.GERENTE,b.CLIENTE_NOMBRE,");
                stringBuilder.Append(" b.RNC_CLIENTE ,b.CLIENTE_NOMBRE RNC_NOMBRE,B.ORIGEN_AFILIACION, NVL(itemtypenum,0)  AS ITEMTYPENUM ,user USUARIO,SYSDATE FECHA ,NVL(B.AFILIADO_NO,0) AFILIADO_NO");
                stringBuilder.Append(" FROM hsi.AUDITORIA_BUSQUEDA_TSS B LEFT OUTER JOIN hsi.AUDITORIA_DOC_RESULT_TSS d");
                stringBuilder.Append(" ON     B.AFILIADO_NO=D.AFILIADO_NO ");
                stringBuilder.Append(" AND    B.COD_AUDITORIA=D.COD_AUDITORIA ");
                stringBuilder.Append(" AND    B.SEcUENCIA_DEP= D.SECUENCIA inner  JOIN Asegurado@onbase_per A ");
                stringBuilder.Append(" ON     B.AFILIADO_NO=A.CODIGO ");
                stringBuilder.Append(" WHERE    NVL(B.NSS_TITULAR,0) <> 0 and B.COD_AUDITORIA=" + cod_auditoria.ToString());
            }

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["Dbonbase"]))
            {
                OracleCommand command = new OracleCommand(stringBuilder.ToString(), cn);
                cn.Open();

                OracleDataReader row = command.ExecuteReader();

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        {
                            string qry = " INSERT INTO AUDITORIA_REPORTE  VALUES (" + cod_auditoria + ", " + row["NSS_TITULAR"].ToString() + ", '" + row["IDENTIFICACION_TITULAR"].ToString() + "' ,'" + row["TITULAR"].ToString() + "', " + row["NSS_DEPENDIENTE"].ToString() + ", '" + row["IDENTIFICACION_DEP"].ToString() + "', '" + row["DEPENDIENTE"].ToString() + "', '" + row["PARENTEZCO"].ToString() + "', '" + row["EDAD"].ToString() + "', '" + row["GENERALES"].ToString() + "','" + row["COMENTARIOS"].ToString() + "', '" + row["ESTATUS"].ToString() + "', '" + row["TRASPASO_UNIFICACION"].ToString() + "', '" + row["TRASPASO_ORDINARIO"].ToString() + "', '" + row["DIRECTOR"].ToString() + "', '" + row["COD_GERENTE"].ToString() + "', '" + row["GERENTE"].ToString() + "','" + row["CLIENTE_NOMBRE"].ToString() + "', '" + row["RNC_CLIENTE"].ToString() + "', '" + row["CLIENTE_NOMBRE"].ToString() + "', '" + row["ORIGEN_AFILIACION"].ToString() + "', " + row["ITEMTYPENUM"].ToString() + " ,'" + row["USUARIO"].ToString() + "', '" +  DateTime.Now.ToString("yyyyMMdd") + "'," + row["AFILIADO_NO"].ToString() + ")";
                            cncon.Insert_update_Data_Sql(qry);
                        }
                    }
                    cn.Close();

                }
            }

        }

        private void Transferir_data_sql_reporte_rad(int cod_auditoria, string _tipo)
        {

            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("DataReport");

            if (_tipo == "RADICACION")
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" Select distinct  cod_auditoria ,radicacion,POLIZA,CLIENTE_NOMBRE,RNC_CLIENTE,INTERMEDIARIO,Nombre_Intermediario,COD_GERENTE,NOMBRE_AGE,COD_DIRECTOR,NOMBRE_DIR,ITEMNUM,RUTA");
                stringBuilder.Append(" From ( Select distinct audi.cod_auditoria ,p.radicacion,concat(audi.compania,concat(audi.ramo,audi.secuencial)) POLIZA,");
                stringBuilder.Append("                 Decode(C.Tipo,'C', REPLACE(C.NOM_EMP,'''',''),C.Pri_Nom||' '||C.Pri_Ape ) CLIENTE_NOMBRE,");
                stringBuilder.Append("                 Decode(C.Tipo,'C',C.RNC,C.CED_ACT )RNC_CLIENTE,ET.INTERMEDIARIO,INTE.NOMBRE Nombre_Intermediario,ET.COD_GERENTE,ET.NOMBRE_AGE,ET.COD_DIRECTOR,ET.NOMBRE_DIR,doc.ITEMNUM,doc.RUTA");
                stringBuilder.Append("                 from AUDITORIA_RADI_POL_TSS@onbase.world audi inner join radicacion_oper_det p");
                stringBuilder.Append("                 on audi.compania = p.compania And audi.ramo = p.ramo And audi.secuencial = p.secuencial and audi.radicacion_poliza = p.radicacion left outer join AUDITORIA_DOC_RESULT_TSS@onbase.world doc");
                stringBuilder.Append("                 on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no inner join pol_int01_v pv");
                stringBuilder.Append("                 on  p.compania = pv.compania And p.ramo = pv.ramo And p.secuencial = pv.secuencial inner join ESTRUCTURA_VENTAS01_V et");
                stringBuilder.Append("     on pv.intermediario = et.intermediario  inner join Intermediario01_v inte");
                stringBuilder.Append("                 on ET.INTERMEDIARIO = INTE.CODIGO inner join CLIENTE01_v C  on  p.CLIENTE = C.CODIGO");
                stringBuilder.Append(" UNION All");
                stringBuilder.Append(" Select distinct audi.cod_auditoria ,p.radicacion,concat(audi.compania, concat(audi.ramo, audi.secuencial)) POLIZA,");
                stringBuilder.Append("                 Decode(C.Tipo,'C', REPLACE(C.NOM_EMP,'''',''),C.Pri_Nom||' '||C.Pri_Ape ) CLIENTE_NOMBRE,");
                stringBuilder.Append("                 Decode(C.Tipo,'C',C.RNC,C.CED_ACT )RNC_CLIENTE,ET.INTERMEDIARIO,INTE.NOMBRE Nombre_Intermediario,ET.COD_GERENTE,ET.NOMBRE_AGE,ET.COD_DIRECTOR,ET.NOMBRE_DIR,doc.ITEMNUM,doc.RUTA");
                stringBuilder.Append("                 from AUDITORIA_RADI_POL_TSS@onbase.world audi inner join radicacion_oper_det p");
                stringBuilder.Append("                 on audi.compania = p.compania And audi.ramo = p.ramo And audi.secuencial = p.secuencial and audi.radicacion_poliza = p.radicacion left outer join AUDITORIA_DOC_RESULT_TSS@onbase.world doc");
                stringBuilder.Append("                 on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no inner join pol_int01_v pv");
                stringBuilder.Append("                 on  p.compania = pv.compania And p.ramo = pv.ramo And p.secuencial = pv.secuencial inner join ESTRUCTURA_VENTAS01_V et");
                stringBuilder.Append("     on pv.intermediario = et.intermediario  inner join Intermediario01_v inte");
                stringBuilder.Append("                 on ET.INTERMEDIARIO = INTE.CODIGO inner join CLIENTE01_v C  on  p.CLIENTE = C.CODIGO");
                stringBuilder.Append(" UNION All");
                stringBuilder.Append(" Select distinct audi.cod_auditoria ,p.radicacion,concat(audi.compania,concat(audi.ramo,audi.secuencial)) POLIZA,");
                stringBuilder.Append("                   SUBSTR(pr.razonsocial,1,50) CLIENTE_NOMBRE,");
                stringBuilder.Append("                 r.rnc RNC_CLIENTE, e.cod_promotor INTERMEDIARIO,e.nombre_promotor Nombre_Intermediario,e.cod_supervisor COD_GERENTE, e.nombre_supervisor NOMBRE_AGE,e.cod_gerente COD_DIRECTOR,e.nombre_gerente NOMBRE_DIR,doc.ITEMNUM,doc.RUTA");
                stringBuilder.Append(" from AUDITORIA_RADI_POL_TSS@onbase.world  audi inner join radicacion_oper_det p ");
                stringBuilder.Append(" on audi.compania = p.compania And audi.ramo = p.ramo and Decode(audi.ramo, 94, 0, audi.secuencial)= nvl(p.secuencial, 0) and audi.radicacion_poliza = p.radicacion   left outer join AUDITORIA_DOC_RESULT_TSS@onbase.world doc ");
                stringBuilder.Append(" on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no  inner join Asegurado a ");
                stringBuilder.Append(" on  p.cliente=a.cliente  inner  JOIN   rec_1_pbs r ");
                stringBuilder.Append(" on a.nss=r.nss_tit inner  JOIN  rnc_intermediarios p_i on lpad(p_i.rnc_num,11,'0') = lpad(r.rnc,11,'0') inner  JOIN e_intermediario_v  e");
                stringBuilder.Append(" on     e.cod_promotor = p_i.cod_int  inner  JOIN padron_rnc         pr");
                stringBuilder.Append("             on     (lpad(pr.rnc,11,'0')) = lpad(p_i.rnc_num,11,'0')");
                stringBuilder.Append(" WHERE    e.estatus=29 and  a.estatus=5 and pr.estatus='ACTIVO' and p.ramo=94 and p.secuencial is null");
                stringBuilder.Append(" and trunc(r.fecha) = (select max(trunc(l.fecha))");
                stringBuilder.Append("                                 from rec_1_pbs l");
                stringBuilder.Append("                                where l.nss_tit  = r.nss_tit)");
                stringBuilder.Append(" and trunc(p_i.fec_ver)   = (select max(trunc(j.fec_ver))");
                stringBuilder.Append("                                     from rnc_intermediarios j");
                stringBuilder.Append("                                    where j.rnc_num  = p_i.rnc_num))  a");
                stringBuilder.Append(" where  a.cod_auditoria=" + cod_auditoria.ToString());

                using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbArs"]))
                {
                    OracleCommand command = new OracleCommand(stringBuilder.ToString(), cn);
                    cn.Open();

                    OracleDataReader row = command.ExecuteReader();
              

                    if (row.HasRows)
                    {
                       

                        while (row.Read())
                        {
                            {
                                string qry = " INSERT INTO AUDITORIA_REPORTE_RAD_POL  VALUES (" + cod_auditoria + ", '" + _tipo + "','" + row["RADICACION"].ToString() + "', '" + row["POLIZA"].ToString() + "' ,'" + row["CLIENTE_NOMBRE"].ToString() + "','" + row["RNC_CLIENTE"].ToString() + "', '" + row["INTERMEDIARIO"].ToString() + "', '" + row["Nombre_Intermediario"].ToString() + "', '" + row["COD_GERENTE"].ToString() + "', '" + row["NOMBRE_AGE"].ToString() + "', '" + row["COD_DIRECTOR"].ToString() + "', '" + row["NOMBRE_DIR"].ToString() + "','" + row["ITEMNUM"].ToString() + "','" + row["RUTA"].ToString() + "')";
                                cncon.Insert_update_Data_Sql(qry);
                            }
                        }
                        cn.Close();

                    }
                }

            }
            else if (_tipo == "POLIZA")
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" Select distinct cod_auditoria ,radicacion,POLIZA,CLIENTE_NOMBRE,RNC_CLIENTE,INTERMEDIARIO,Nombre_Intermediario,COD_GERENTE,NOMBRE_AGE,COD_DIRECTOR,NOMBRE_DIR,ITEMNUM,RUTA ");
                stringBuilder.Append("From ( Select distinct audi.cod_auditoria ,0 radicacion,concat(audi.compania,concat(audi.ramo,audi.secuencial)) POLIZA, ");
                stringBuilder.Append("                 Decode(C.Tipo,'C', REPLACE(C.NOM_EMP,'''',''),C.Pri_Nom||' '||C.Pri_Ape ) CLIENTE_NOMBRE,");
                stringBuilder.Append("                 Decode(C.Tipo,'C',C.RNC,C.CED_ACT )RNC_CLIENTE,ET.INTERMEDIARIO,INTE.NOMBRE Nombre_Intermediario,ET.COD_GERENTE,ET.NOMBRE_AGE,ET.COD_DIRECTOR,ET.NOMBRE_DIR,doc.ITEMNUM,doc.RUTA ");
                stringBuilder.Append("                 from AUDITORIA_RADI_POL_TSS@onbase.world audi inner join poliza p");
                stringBuilder.Append("                 on audi.compania = p.compania And audi.ramo = p.ramo And audi.secuencial = p.secuencial  left outer join hsi.AUDITORIA_DOC_RESULT_TSS@onbase.world doc ");
                stringBuilder.Append("                 on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no inner join pol_int01_v pv");
                stringBuilder.Append("                 on  p.compania = pv.compania And p.ramo = pv.ramo And p.secuencial = pv.secuencial inner join ESTRUCTURA_VENTAS01_V et ");
                stringBuilder.Append("                on pv.intermediario = et.intermediario  inner join Intermediario01_v inte");
                stringBuilder.Append("                 on ET.INTERMEDIARIO = INTE.CODIGO inner join CLIENTE01_v C  on  p.CLIENTE = C.CODIGO ");
                stringBuilder.Append(" UNION All");
                stringBuilder.Append(" Select distinct audi.cod_auditoria ,0 radicacion,concat(audi.compania, concat(audi.ramo, audi.secuencial)) POLIZA, ");
                stringBuilder.Append("                 Decode(C.Tipo,'C', REPLACE(C.NOM_EMP,'''',''),C.Pri_Nom||' '||C.Pri_Ape ) CLIENTE_NOMBRE,");
                stringBuilder.Append("                 Decode(C.Tipo,'C',C.RNC,C.CED_ACT )RNC_CLIENTE,ET.INTERMEDIARIO,INTE.NOMBRE Nombre_Intermediario,ET.COD_GERENTE,ET.NOMBRE_AGE,ET.COD_DIRECTOR,ET.NOMBRE_DIR,doc.ITEMNUM,doc.RUTA ");
                stringBuilder.Append("                 from AUDITORIA_RADI_POL_TSS@onbase.world audi inner join poliza p");
                stringBuilder.Append("                 on audi.compania = p.compania And audi.ramo = p.ramo And audi.secuencial = p.secuencial  left outer join hsi.AUDITORIA_DOC_RESULT_TSS@onbase.world doc ");
                stringBuilder.Append("                 on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no inner join pol_int01_v pv");
                stringBuilder.Append("                 on  p.compania = pv.compania And p.ramo = pv.ramo And p.secuencial = pv.secuencial inner join ESTRUCTURA_VENTAS01_V et ");
                stringBuilder.Append("                on pv.intermediario = et.intermediario  inner join Intermediario01_v  inte");
                stringBuilder.Append("                 on ET.INTERMEDIARIO = INTE.CODIGO inner join CLIENTE01_v C  on  p.CLIENTE = C.CODIGO ");
                stringBuilder.Append(" UNION All");
                stringBuilder.Append("  Select distinct audi.cod_auditoria ,0 radicacion,concat(audi.compania,concat(audi.ramo,audi.secuencial)) POLIZA,");
                stringBuilder.Append("                  SUBSTR(pr.razonsocial,1,50) CLIENTE_NOMBRE,");
                stringBuilder.Append("                r.rnc RNC_CLIENTE, e.cod_promotor INTERMEDIARIO,e.nombre_promotor Nombre_Intermediario,e.cod_supervisor COD_GERENTE, e.nombre_supervisor NOMBRE_AGE,e.cod_gerente COD_DIRECTOR,e.nombre_gerente NOMBRE_DIR,doc.ITEMNUM,doc.RUTA ");
                stringBuilder.Append(" from AUDITORIA_RADI_POL_TSS@onbase.world  audi inner join poliza p ");
                stringBuilder.Append(" on audi.compania = p.compania And audi.ramo = p.ramo and audi.secuencial= p.secuencial  left outer join AUDITORIA_DOC_RESULT_TSS@onbase.world doc ");
                stringBuilder.Append(" on audi.cod_auditoria = DOC.COD_AUDITORIA and audi.radicacion_poliza = doc.afiliado_no  inner join Asegurado a ");
                stringBuilder.Append(" on  p.cliente=a.cliente  inner  JOIN   rec_1_pbs r ");
                stringBuilder.Append(" on     a.nss=r.nss_tit inner  JOIN  rnc_intermediarios p_i"); 
                stringBuilder.Append("  on     lpad(p_i.rnc_num,11,'0') = lpad(r.rnc,11,'0') inner  JOIN e_intermediario_v  e ");
                stringBuilder.Append("            on     e.cod_promotor = p_i.cod_int  inner  JOIN padron_rnc         pr ");
                stringBuilder.Append("            on     (lpad(pr.rnc,11,'0')) = lpad(p_i.rnc_num,11,'0') ");
                stringBuilder.Append(" WHERE           e.estatus=29");
                stringBuilder.Append(" and  a.estatus=5");
                stringBuilder.Append(" and pr.estatus='ACTIVO'");
                stringBuilder.Append(" and p.ramo=94");
                stringBuilder.Append(" and p.secuencial is null ");
                stringBuilder.Append(" and trunc(r.fecha) = (select max(trunc(l.fecha)) ");
                stringBuilder.Append("       from rec_1_pbs l");
                stringBuilder.Append("       where l.nss_tit  = r.nss_tit) ");
                stringBuilder.Append(" and trunc(p_i.fec_ver)   = (select max(trunc(j.fec_ver)) ");
                stringBuilder.Append("                                    from rnc_intermediarios j ");
                stringBuilder.Append("                                   where j.rnc_num  = p_i.rnc_num))  a ");
                stringBuilder.Append(" where  a.cod_auditoria=" + cod_auditoria.ToString());

                using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbArs"]))
                {
                    OracleCommand command = new OracleCommand(stringBuilder.ToString(), cn);
                    cn.Open();

                    OracleDataReader row = command.ExecuteReader();

                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            {
                                string qry = " INSERT INTO AUDITORIA_REPORTE_RAD_POL  VALUES (" + cod_auditoria + ", '" + _tipo + "','" + row["RADICACION"].ToString() + "', '" + row["POLIZA"].ToString() + "' ,'" + row["CLIENTE_NOMBRE"].ToString() + "','" + row["RNC_CLIENTE"].ToString() + "', '" + row["INTERMEDIARIO"].ToString() + "', '" + row["Nombre_Intermediario"].ToString() + "', '" + row["COD_GERENTE"].ToString() + "', '" + row["NOMBRE_AGE"].ToString() + "', '" + row["COD_DIRECTOR"].ToString() + "', '" + row["NOMBRE_DIR"].ToString() + "','" + row["ITEMNUM"].ToString() + "','" + row["RUTA"].ToString() + "')";
                                cncon.Insert_update_Data_Sql(qry);
                            }
                        }
                        cn.Close();

                    }
                }

            }
        }

        public DataTable dtbus_especifica_Ws_Bs(string tipo, string Valor1, string Valor2, string Valor3, string Valor4, string Valor5)
        {
            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("Data");


            if (tipo == "POLIZA")
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" Select  a.nss NSS ,b.nss NSSAfiliado ,a.ced_act CedulaTitular ,b.ced_act CedulaDep ");
                stringBuilder.Append(" From asegurado a inner join dependiente b");
                stringBuilder.Append(" on a.codigo = b.asegurado inner join Ase_pol01_v p");
                stringBuilder.Append(" on   b.asegurado = p.asegurado  and a.nss > 0 and b.nss > 0 and p.secuencial>0");
                stringBuilder.Append(" where   concat(p.compania,concat(p.ramo,p.secuencial))='" + Valor5.Trim() + "'");
                stringBuilder.Append("union all ");
                stringBuilder.Append(" Select  a.nss NSS ,b.nss NSSAfiliado ,a.ced_act CedulaTitular ,b.ced_act CedulaDep ");
                stringBuilder.Append(" From asegurado a inner join dependiente b");
                stringBuilder.Append(" on a.codigo = b.asegurado inner join Ase_pol01_v p");
                stringBuilder.Append(" on   b.asegurado = p.asegurado  and a.nss > 0 and b.nss > 0 and p.secuencial>0");
                stringBuilder.Append(" where   concat(p.compania,concat(p.ramo,p.secuencial))='" + Valor5.Trim() + "'");

                dt = cncon.GetDatatable_ars(stringBuilder.ToString());
            }
            else if (tipo == "RADICACION")
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" Select  a.nss_titular ,a.nss_dep  ,a.ced_titular ,a.Ced_dep from ");
                stringBuilder.Append(" (Select det.radicacion,a.nss nss_titular,dep.nss as nss_dep, a.ced_act as ced_titular, dep.ced_act Ced_dep");
                stringBuilder.Append("          from  radicacion_oper_det det Inner join     movimiento_mensual m");
                stringBuilder.Append("                on             DET.COMPANIA=m.compania");
                stringBuilder.Append("                and            det.ramo=m.ramo");
                stringBuilder.Append("                and            det.secuencial=m.secuencial Inner Join    Asegurado a ");
                stringBuilder.Append("                on             m.asegurado=a.codigo inner join dependiente dep");
                stringBuilder.Append("                on             a.codigo=dep.asegurado");
                stringBuilder.Append("          where (TO_DATE(m.fec_mov, 'YYYY-MM-DD') - TO_DATE(SYSDATE, 'YYYY-MM-DD')) <=10 ");
                stringBuilder.Append("         and a.nss > 0 and dep.nss >0 ");
                stringBuilder.Append("          UNION ALL ");
                stringBuilder.Append("          Select det.radicacion, a.nss nss_titular,dep.nss as nss_dep, a.ced_act as ced_titular, dep.ced_act Ced_dep");
                stringBuilder.Append("          from   radicacion_oper_det det inner join      movimiento_mensual m");
                stringBuilder.Append("                 on              DET.COMPANIA=m.compania");
                stringBuilder.Append("                 and           det.ramo=m.ramo");
                stringBuilder.Append("                 and           det.secuencial=m.secuencial  ");
                stringBuilder.Append("                 and          det.cliente=m.cliente inner join Asegurado a ");
                stringBuilder.Append("                 on            m.asegurado=a.codigo  inner join dependiente dep");
                stringBuilder.Append("                 on            a.codigo=dep.asegurado ");
                stringBuilder.Append("          where  a.nss > 0 ");
                stringBuilder.Append("          and (TO_DATE(m.fec_mov, 'YYYY-MM-DD') - TO_DATE(SYSDATE, 'YYYY-MM-DD')) <=10   ) a ");
                stringBuilder.Append("  where a.radicacion='" + Valor4.Trim() + "' ");
                dt = cncon.GetDatatable_ars(stringBuilder.ToString());
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" Select  a.nss NSS ,b.nss NSSAfiliado ,a.ced_act CedulaTitular ,b.ced_act CedulaDep ");
                stringBuilder.Append(" From asegurado a,dependiente b ");
                stringBuilder.Append(" where a.codigo = b.asegurado");
                stringBuilder.Append(" And   ((a.ced_act='" + Valor1.Trim() + "' or b.ced_act='" + Valor1.Trim() + "') or (a.nss='" + Valor2.Trim() + "' or b.nss='" + Valor2.Trim() + "')  or (a.codigo='" + Valor3.Trim() + "' or b.codigo='" + Valor3.Trim() + "')) and a.nss > 0   and a.ced_act is not null ");
                stringBuilder.Append(" union all ");
                stringBuilder.Append(" Select  a.nss NSS ,b.nss NSSAfiliado ,a.ced_act CedulaTitular ,b.ced_act CedulaDep ");
                stringBuilder.Append(" From asegurado a,dependiente  b ");
                stringBuilder.Append(" where a.codigo = b.asegurado");
                stringBuilder.Append(" And   ((a.ced_act='" + Valor1.Trim() + "' or b.ced_act='" + Valor1.Trim() + "') or (a.nss='" + Valor2.Trim() + "' or b.nss='" + Valor2.Trim() + "')  or (a.codigo='" + Valor3.Trim() + "' or b.codigo='" + Valor3.Trim() + "')) and a.nss > 0   and a.ced_act is not null ");

                dt = cncon.GetDatatable_ars(stringBuilder.ToString());
            }
            return dt;

        }
        public void dtbus_pol_radic_Ws_Bs(string tipoBusqueda, int codauditoria, DataTable dt)
        {
            ClsConexion cncon = new ClsConexion();


            if (tipoBusqueda == "POLIZA")
            {
                foreach (DataRow row in dt.Rows)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(" begin insert into AUDITORIA_RADI_POL_TSS@onbase.world");
                    stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria ,   concat(compania, concat(ramo, secuencial)) ,compania,ramo,secuencial");
                    stringBuilder.Append(" from poliza where secuencial > 0");
                    stringBuilder.Append(" and concat(compania, concat(ramo, secuencial))= '" + row[0].ToString() + "'");
                    stringBuilder.Append(" union all ");
                    stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria ,   concat(compania, concat(ramo, secuencial)) ,compania,ramo,secuencial");
                    stringBuilder.Append(" from poliza  where secuencial > 0");
                    stringBuilder.Append(" and concat(compania, concat(ramo, secuencial))= '" + row[0].ToString() + "' ; commit; end; ");
                    cncon.Insert_update_Data_Ars(stringBuilder.ToString());
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("  begin insert into AUDITORIA_RADI_POL_TSS@onbase.world");
                    stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria ,radicacion,  max(compania),max(ramo),max(Case Ramo when 94 then 29981 else  secuencial end)");
                    stringBuilder.Append(" from radicacion_oper_det");
                    stringBuilder.Append(" where radicacion = '" + row[0].ToString() + "'");
                    stringBuilder.Append(" group by radicacion");
                    stringBuilder.Append(" union all");
                    stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria ,radicacion,  max(compania),max(ramo),max(Case Ramo when 94 then 29981 else  secuencial end)");
                    stringBuilder.Append(" from radicacion_oper_det ");
                    stringBuilder.Append(" where radicacion = '" + row[0].ToString() + "'");
                    stringBuilder.Append(" group by radicacion ; commit; end;");
                    cncon.Insert_update_Data_Ars(stringBuilder.ToString());
                }
            }
        }
        public void dtbus_pol_radic_especific_Ws_Bs(string tipoBusqueda, int codauditoria, string valor)
        {
            ClsConexion cncon = new ClsConexion();


            if (tipoBusqueda == "POLIZA")
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" begin insert into HSI.AUDITORIA_RADI_POL_TSS");
                stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria , concat(compania,concat(ramo,(Case Ramo when 94 then 29981 else  secuencial end))) ,compania,ramo,(Case Ramo when 94 then 29981 else  secuencial end) from poliza@onbase_per where secuencial> 0 and  concat(compania,concat(ramo,secuencial)) ='" + valor + "'; commit; end; ");
                cncon.Insert_update_Data(stringBuilder.ToString());

            }
            else
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" begin insert into HSI.AUDITORIA_RADI_POL_TSS");
                stringBuilder.Append(" select distinct " + codauditoria + " as cod_auditoria , radicacion,max(compania),max(ramo),max((Case Ramo when 94 then 29981 else  secuencial end)) from radicacion_oper_det@onbase_per where  radicacion ='" + valor + "' group by radicacion ; commit; end;");
                cncon.Insert_update_Data(stringBuilder.ToString());

            }
        }
        private void insert_auditoria(string cod_auditoria, string userName, int TipoBusqueda, string FormaBusqueda)
        {
            ClsConexion cncon = new ClsConexion();
            string queryString2 = "INSERT INTO hsi.AUDITORIA_TSS(COD_AUDITORIA, FECHA_INICIO, USUARIO, TIPO_BUSQUEDA, FORMA_BUSQUEDA) VALUES (" + cod_auditoria + ", SYSDATE, '" + userName + "'," + TipoBusqueda + ",'" + FormaBusqueda + "')";
            cncon.Insert_update_Data(queryString2);

            string queryString = "INSERT INTO AUDITORIA(COD_AUDITORIA, FECHA_INICIO, USUARIO, TIPO_BUSQUEDA, FORMA_BUSQUEDA) VALUES (" + cod_auditoria + ", '" + DateTime.Now.ToString("yyyyMMdd")+"', '" + userName + "'," + TipoBusqueda + ",'" + FormaBusqueda + "')";
            cncon.Insert_update_Data_Sql(queryString);
        }
        public DataTable GetReporteAudit(int codigoAuditoria, string desde, string hasta)
        {

            DataTable dt = new DataTable("Data");
            ClsConexion cncon = new ClsConexion();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(" Select Distinct cod_auditoria NoAuditoria,Usuario,fecha_inicio FechaPorceso");
            stringBuilder.Append(" from AUDITORIA ");
            stringBuilder.Append(" where  FECHA_INICIO BETWEEN '" + desde + "' AND  '" + hasta + "' AND fecha_fin is not null");
            if (codigoAuditoria > 0)
            {
                stringBuilder.Append(" AND  cod_auditoria = " + codigoAuditoria + "");
            }
            stringBuilder.Append(" Order by cod_auditoria ,fecha_inicio asc");

            dt = cncon.GetDatatableSql(stringBuilder.ToString());

            return dt;
        }
        public DataTable Validar_carpeta_rad(int NumAuditoria)
        {
            ClsConexion cncon = new ClsConexion();
            DataTable dt = new DataTable("Data");
            StringBuilder stringBuilder = new StringBuilder();
             stringBuilder.Append(" exec P_VALIDAR_CARPETA_RAD " + NumAuditoria.ToString() + " ");

            dt = cncon.GetDatatableSql(stringBuilder.ToString());
            return dt;
        }
        public void InsertDataReport(DataTable dtResultDoc,int auditoria)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ClsConexion cncon = new ClsConexion();
            try { 
            foreach (DataRow row in dtResultDoc.Rows)
            {
               string qry= " INSERT INTO AUDITORIA_REPORTE  VALUES (" + auditoria + ", " + row["NSS_TITULAR"].ToString() + ", '" + row["IDENTIFICACION_TITULAR"].ToString() + "' ,'" + row["TITULAR"].ToString() + "', " + row["NSS_DEPENDIENTE"].ToString() + ", '" + row["IDENTIFICACION_DEP"].ToString() + "', '" + row["DEPENDIENTE"].ToString() + "', '" + row["PARENTEZCO"].ToString() + "', '" + row["EDAD"].ToString() + "', '" + row["GENERALES"].ToString() + "','" + row["COMENTARIOS"].ToString() + "', '" + row["ESTATUS"].ToString() + "', '" + row["TRASPASO_UNIFICACION"].ToString() + "', '" + row["TRASPASO_ORDINARIO"].ToString() + "', '" + row["DIRECTOR"].ToString() + "', '" + row["COD_GERENTE"].ToString() + "', '" + row["GERENTE"].ToString() + "','" + row["CLIENTE_NOMBRE"].ToString() + "', '" + row["RNC_CLIENTE"].ToString() + "', '" + row["RNC_NOMBRE"].ToString() + "', '" + row["ORIGEN_AFILIACION"].ToString() + "', " + row["ITEMTYPENUM"].ToString() + " ,'" + Environment.UserName.ToString() + "', '" + DateTime.Now.ToString("yyyyMMdd") + "')";
                cncon.Insert_update_Data_Sql(qry);
            }
        }

            catch (Exception ex)
            {
                ex.ToString();
            }
}
        public void InsertaDataBusqueda(DataTable dt, int codigoauditoria)
        {
            try
            {
                ClsConexion cncon = new ClsConexion();
                string queryString = string.Empty;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string cedula_dep = "";
                    string cedula_ti = "";
                    string NSS_TITULAR = dataRow[0].ToString();
                    string NSS_DEPENDIENTE = dataRow[1].ToString();
                    int longitud_cedula = dataRow[2].ToString().Length;

                    int longitud_cedula_dep = dataRow[3].ToString().Length;
                    if (NSS_TITULAR == "") { NSS_TITULAR = "0"; }
                    if (NSS_DEPENDIENTE == "") { NSS_DEPENDIENTE = "0"; }

                        if (longitud_cedula >= 9)
                        {
                                cedula_ti = String.Format("{0:00000000000}", Convert.ToInt64(dataRow[2].ToString()));
                        }
   
                        else
                        {
                                 cedula_ti = "";
                        }
                        if (longitud_cedula_dep >= 9)
                        { 
                             cedula_dep = String.Format("{0:00000000000}", Convert.ToInt64(dataRow[3].ToString()));
                        }
              
                        else
                        {
                                 cedula_dep = "";
                        }
                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '','','','" + cedula_ti + "','" + cedula_dep + "')";
                            cncon.Insert_update_Data(queryString);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
         public DataTable getDataDocsFinales(int NumAuditoria)
        {
            ClsConexion cncon = new ClsConexion();
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = new DataTable("DataReport");
            stringBuilder.Append(" exec P_REPORTE_BUSQUEDA " + NumAuditoria.ToString() + "");
            dt = cncon.GetDatatableSql(stringBuilder.ToString());
            return dt;
        }
        public DataTable getDataDocsFinalesRadic_poliza(int NumAuditoria,string tipo)
        {
            ClsConexion cncon = new ClsConexion();
           
            DataTable dt = new DataTable("DataReport");

            if (tipo == "RADICACION")
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" SELECT distinct [COD_AUDITORIA] ,[RADICACION],[POLIZA],[CLIENTE_NOMBRE],[RNC_CLIENTE],[INTERMEDIARIO],[NOMBRE_INTERMEDIARIO],[COD_GERENTE],[GERENTE],[COD_DIRECTOR],[DIRECTOR]");
                stringBuilder.Append("FROM[dbo].[AUDITORIA_REPORTE_RAD_POL]  WHERE  TIPO='" + tipo + "' AND COD_AUDITORIA=" + NumAuditoria.ToString());
                dt = cncon.GetDatatableSql(stringBuilder.ToString());
            }
            else

            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" SELECT distinct [COD_AUDITORIA] ,[POLIZA],[CLIENTE_NOMBRE],[RNC_CLIENTE],[INTERMEDIARIO],[NOMBRE_INTERMEDIARIO],[COD_GERENTE],[GERENTE],[COD_DIRECTOR],[DIRECTOR]");
                stringBuilder.Append("FROM[dbo].[AUDITORIA_REPORTE_RAD_POL]  WHERE  TIPO= '" + tipo + "' AND COD_AUDITORIA=" + NumAuditoria.ToString());
                dt = cncon.GetDatatableSql(stringBuilder.ToString());
            }
            return dt;
        }
        public DataTable getDataDocsFinalesADIC(int NumAuditoria)
        {
            ClsConexion cncon = new ClsConexion();

            DataTable dt = new DataTable("DataReport");


                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("P_REPORTE_BUSQUEDA_NSS_RAD " + NumAuditoria.ToString());
                dt = cncon.GetDatatableSql(stringBuilder.ToString());
              return dt;
        }
        public DataTable getDataDocsFinalesIndividual(int NumAuditoria,string tipo_parametro,string valor)
        {
            ClsConexion cncon = new ClsConexion();
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = new DataTable("DataReport");
            stringBuilder.Append(" SELECT DISTINCT COD_AUDITORIA,NSS_TITULAR,IDENTIFICACION_TITULAR,TITULAR,NSS_DEPENDIENTE,IDENTIFICACION_DEP,DEPENDIENTE,R.PARENTEZCO,EDAD, DOC_ENCONTRADOS=ISNULL(STUFF((SELECT ',' + ISNULL(LTRIM(RTRIM(ITEMTYPENAME)), '') FROM DOCTYPE D,AUDITORIA_VAL_DOC V WHERE D.ITEMTYPENUM=V.ITENTYPENUM AND (NSS_DEPENDIENTE=R.NSS_DEPENDIENTE) AND V.RUTA IS NOT NULL FOR XML PATH('')), 1, 1, '' ),'') ,");
            stringBuilder.Append(" DOCUMENTOS_REQUERIDOS= dbo.GET_DOC(EDAD,R.PARENTEZCO),ESTATUS,TRASPASO_UNIFICACION,TRASPASO_ORDINARIO,DIRECTOR");
            stringBuilder.Append("  COD_GERENTE,GERENTE,CLIENTE_NOMBRE,RNC_CLIENTE,RNC_NOMBRE,ORIGEN_AFILIACION");
            stringBuilder.Append("  FROM  AUDITORIA_REPORTE R ");
            stringBuilder.Append("  WHERE R.COD_AUDITORIA=" + NumAuditoria.ToString());

            switch (tipo_parametro)
            {
                case "POLIZA": 
                    //stringBuilder.Append(" AND ");

                    break;
                case "RADICACION":
                   // stringBuilder.Append("");

                    break;
                case "CEDULA":
                    stringBuilder.Append(" AND (IDENTIFICACION_TITULAR='"+ valor + "' or IDENTIFICACION_DEP='" + valor + "' )");

                    break;
                case "NSS":
                    stringBuilder.Append("AND (NSS_TITULAR='" + valor + "' or NSS_DEPENDIENTE='" + valor + "' )");

                    break;
                case "CODIGO AFILIADO":
                    stringBuilder.Append(" AND AFILIADO_NO='"+ valor+"'");

                    break;
            }





            dt = cncon.GetDatatableSql(stringBuilder.ToString());
            return dt;
        }
        public DataTable Getusuario(string consulta)
        {
            DataTable dt = new DataTable("Data");
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DbSql"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = consulta;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }
        public void InsertaDataBusquedaIndicidual(string filtro, string valor, DataTable dt, int codigoauditoria)
        {
            try
            {
                ClsConexion cncon = new ClsConexion();
                foreach (DataRow dataRow in dt.Rows)
                {
                    string queryString = "";
                    string NSS_TITULAR = dataRow[0].ToString();
                    string NSS_DEPENDIENTE = dataRow[0].ToString();
                    if (NSS_TITULAR == "") { NSS_TITULAR = "0"; }
                    if (NSS_DEPENDIENTE == "") { NSS_DEPENDIENTE = "0"; }


                    switch (filtro)
                    {
                        case "CEDULA":


                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '" + filtro + "','" + valor + "','','" + dataRow[2].ToString() + "','" + dataRow[3].ToString() + "')";

                            break;
                        case "NSS":


                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '" + filtro + "','" + valor + "','','" + dataRow[2].ToString() + "','" + dataRow[3].ToString() + "')";

                            break;
                        case "CODIGO AFILIADO":

                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '" + filtro + "','" + valor + "','','" + dataRow[2].ToString() + "','" + dataRow[3].ToString() + "')";

                            break;
                        case "POLIZA":

                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '','','','" + dataRow[2].ToString() + "','" + dataRow[3].ToString() + "')";

                            break;
                        case "RADICACION":

                            queryString = "INSERT INTO hsi.AUDITORIA_NSS_TSS(COD_AUDITORIA,NSS_TITULAR, NSS_DEPENDIENTE,CAMPO,VALOR,ESTATUS,CEDULA_TIT,CEDULA_DEP) values (" + codigoauditoria + ", " + Convert.ToInt32(NSS_TITULAR) + ", " + Convert.ToInt32(NSS_DEPENDIENTE) + ", '','','','" + dataRow[2].ToString() + "','" + dataRow[3].ToString() + "')";

                            break;
                    }

                    cncon.Insert_update_Data(queryString);

                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void CreaCarpetas(int codigoAuditoria, string rutaseleccionada, string ParametroCarpeta)
        {
      
            DataTable data_row = new DataTable("Data");
            data_row = Validar_carpeta(codigoAuditoria);
            string text = "";
            foreach (DataRow data in data_row.Rows)
            {
                if (ParametroCarpeta == "1")
                {
                    if (string.Compare(data["NSS_TITULAR"].ToString(), "", false) != 0)
                    {
                        text ="Auditoria No_" + codigoAuditoria.ToString()   + "\\" + (data["NSS_DEPENDIENTE"].ToString() == "0" ? (data["NSS_TITULAR"].ToString() + "\\" + data["NSS_TITULAR"].ToString()) : (data["NSS_TITULAR"].ToString() + "\\" + data["NSS_DEPENDIENTE"].ToString()));
                    }

                }
                else
                {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["NSSINDIVIDUAL"].ToString();
                }

                string text3 = "";
                if (!Directory.Exists(rutaseleccionada + "\\" + text))
                {
                    Directory.CreateDirectory(rutaseleccionada + "\\" + text);
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }
                else
                {
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }

                if (data["RUTA"].ToString() != string.Empty)
                {
                    if (File.Exists(Convert.ToString(data["RUTA"])))
                    {
                        if (!File.Exists(text3.Trim() + Path.GetFileName(Convert.ToString(data["RUTA"]))))
                        {
                            string extension = Path.GetExtension(Convert.ToString(data["RUTA"]));
                            if (extension.Trim() == ".tmp" || extension.Trim() == ".TMP")
                            {
                                if (!File.Exists(text3.Trim() + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF"))
                                {
                                    File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF");
                                }
                            }
                            else
                            {
                                File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileName(Convert.ToString(data["RUTA"])));
                            }

                        }

                    }

                }

            }


        }

        public void CreaCarpetasPolRadicacion(int codigoAuditoria, string rutaseleccionada,  string tipoCarpeta)
        {
            DataTable data_row = new DataTable("Data");
            data_row = Validar_carpeta_rad(codigoAuditoria);
            DataTable dataTable = new DataTable("Data");
            DataTable drGral = new DataTable("Data");
            string text = "";
            foreach (DataRow data in data_row.Rows)
            {
                if (tipoCarpeta == "POLIZA")
                {
                    if (data["POLIZA"].ToString()!= "")
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["POLIZA"].ToString() ;
                    }
                }
                else
                {
                    if (data["RADICACION"].ToString() != "0")
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["RADICACION"].ToString() ; 
                    }
                }
                string text3 = "";
                if (!Directory.Exists(rutaseleccionada + "\\" + text))
                {
                    Directory.CreateDirectory(rutaseleccionada + "\\" + text);
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }
                else
                {
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }

                if (data["RUTA"].ToString() != string.Empty)
                {
                    if (File.Exists(Convert.ToString(data["RUTA"])))
                    {
                        if (!File.Exists(text3 + Path.GetFileName(Convert.ToString(data["RUTA"]))))
                        {
                            string extension = Path.GetExtension(Convert.ToString(data["RUTA"]));
                            if (extension.Trim() == ".tmp" || extension.Trim() == ".TMP")
                            {
                                if (!File.Exists(text3.Trim() + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF"))
                                {
                                    File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF");
                                }
                            }
                            else
                            {
                                File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileName(Convert.ToString(data["RUTA"])));
                            }
                        }
                    }
                }

            }

        }
        public void CreaCarpetasadic(int codigoAuditoria, string rutaseleccionada,string tipocarpeta)
        {
            DataTable data_row = new DataTable("Data");
            data_row = Validar_carpeta_adi(codigoAuditoria);
            DataTable dataTable = new DataTable("Data");
            DataTable drGral = new DataTable("Data");
            string text = "";
            foreach (DataRow data in data_row.Rows)
            {


                if (tipocarpeta == "RADICACION")
                {
                    if (data["radicacion"].ToString() != "")
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["radicacion"].ToString() + "\\" + data["nss_titular"].ToString();
                    }
                }
                else if (tipocarpeta == "POLIZA")
                {
                    if (data["poliza"].ToString() != "")
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["poliza"].ToString() + "\\" + data["nss_titular"].ToString();
                    }
                }
                else
                {
                    text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + data["poliza"].ToString() + "\\" + data["radicacion"].ToString() + "\\" + data["nss_titular"].ToString();
                }

                string text3 = "";
                if (!Directory.Exists(rutaseleccionada + "\\" + text))
                {
                    Directory.CreateDirectory(rutaseleccionada + "\\" + text);
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }
                else
                {
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }

                if (data["RUTA"].ToString() != string.Empty)
                {
                    if (File.Exists(Convert.ToString(data["RUTA"])))
                    {
                        if (!File.Exists(text3 + Path.GetFileName(Convert.ToString(data["RUTA"]))))
                        {
                            string extension = Path.GetExtension(Convert.ToString(data["RUTA"]));
                            if (extension.Trim() == ".tmp" || extension.Trim() == ".TMP")
                            {
                                if (!File.Exists(text3.Trim() + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF"))
                                {
                                    File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF");
                                }
                            }
                            else
                            {
                                File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileName(Convert.ToString(data["RUTA"])));
                            }
                        }
                    }
                }

            }

        }
        public void CreaCarpetasIndividual(int codigoAuditoria, string rutaseleccionada,  string tipoCarpeta,string valor)
        {
            DataTable data_row = new DataTable("Data");
            data_row = Validar_carpeta(codigoAuditoria);
            DataTable dataTable = new DataTable("Data");
            DataTable drGral = new DataTable("Data");
            string text = "";
           
                if (tipoCarpeta == "CEDULA")
                {
                    if (string.Compare(valor, "", false) != 0)
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + valor;
                    }
                }
                else if (tipoCarpeta == "NSS")
                {
                    if (string.Compare(valor, "", false) != 0)
                    {
                        text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + valor;
                    }
                }
                else if (tipoCarpeta == "CODIGO AFILIADO")
                {
                    if (string.Compare(valor, "", false) != 0)
                    {
                        text =   "Auditoria No_" + codigoAuditoria.ToString() + "\\" + valor;
                    }
                }
 
                string text3 = "";
            foreach (DataRow data in data_row.Rows)
            {

                if (tipoCarpeta == "RADICACION") { text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + valor + "\\" + data["NSS_TITULAR"].ToString(); }
                if (tipoCarpeta == "POLIZA") { text = "Auditoria No_" + codigoAuditoria.ToString() + "\\" + valor + "\\" + data["NSS_TITULAR"].ToString(); }

                if (!Directory.Exists(rutaseleccionada + "\\" + text))
                {
                    Directory.CreateDirectory(rutaseleccionada + "\\" + text);
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }
                else
                {
                    text3 = rutaseleccionada + "\\" + text + "\\";
                }

                if (data["ITEMNUM"].ToString() != string.Empty)
                {
                    if (File.Exists(Convert.ToString(data["RUTA"])))
                    {
                        if (!File.Exists(text3.Trim() + Path.GetFileName(Convert.ToString(data["RUTA"]))))
                        {
                            string extension = Path.GetExtension(Convert.ToString(data["RUTA"]));
                            if (extension.Trim() == ".tmp" || extension.Trim() == ".TMP")
                            {
                                if (!File.Exists(text3.Trim() + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF"))
                                {
                                    File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileNameWithoutExtension(Convert.ToString(data["RUTA"])) + ".TIF");
                                }
                            }
                            else
                            {
                                File.Copy(Convert.ToString(data["RUTA"]), text3 + Path.GetFileName(Convert.ToString(data["RUTA"])));
                            }
                        }
                    }
                }

            }



        }


    }



}

 





