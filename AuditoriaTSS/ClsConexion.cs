using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;
using Oracle.Web;
using System.IO;
using System.Threading.Tasks;


namespace AuditoriaTSS
{
    public class ClsConexion
    {
        public DataTable GetDatatable(string strSql)
        {
            DataTable dt = new DataTable("Data");
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter();

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbOnbase"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetDatatable_ars(string strSql)
        {
            DataTable dt = new DataTable("Data");
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter();

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbArs"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                cn.Open();
                da.Fill(dt);
                cn.Close();
            }
            return dt;
        }
        public int Insert_update_Data(string sql)
        {
            int resultado = 0;

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbOnbase"]))
            {
                OracleCommand cmd = new OracleCommand(sql, cn);
                cn.Open();
                cmd.CommandType = CommandType.Text;
                resultado = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return resultado;
        }
        public int Insert_update_Data_Ars(string sql)
        {
            int resultado = 0;

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbArs"]))
            {
                OracleCommand cmd = new OracleCommand(sql, cn);
                cn.Open();
                cmd.CommandType = CommandType.Text;
                resultado = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return resultado;
        }
        public DataSet GetDataSet(string strSql)
        {
            DataSet dt = new DataSet("Data");
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter();

            using (OracleConnection cn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["DbOnbase"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetDatatableSql(string strSql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DbSql"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }

        public void BulkInsert(DataTable dt, string nombre_tabla)
        {

           
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DbSql"]))
            {

                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    cn,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );
                bulkCopy.DestinationTableName = nombre_tabla;
                cn.Open();
                bulkCopy.BulkCopyTimeout = 999999999;
                bulkCopy.WriteToServer(dt);
                cn.Close();
            }
        }
        public int Insert_update_Data_Sql(string sql)
        {
            int resultado = 0;

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DbSql"]))
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                cmd.CommandType = CommandType.Text;
                resultado = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return resultado;
        }
        public DataSet GetDataSet_Sql(string strSql)
        {
            DataSet dt = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DbSql"]))
            {
                cmd.Connection = cn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000000;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }


    }
}
