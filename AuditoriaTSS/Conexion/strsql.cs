using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace AimgosWeb.Conexion
{
        class strsql
        {
            public DataTable GetDatatable(string strSql)
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();

                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
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
            public int Insert_update_Data(string sql)
            {
                int resultado = 0;

                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    resultado = cmd.ExecuteNonQuery();
                    cn.Close();
                }
                return resultado;
            }
            public DataSet GetDataSet(string strSql)
            {
                DataSet dt = new DataSet();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();

                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStringPrueba"].ConnectionString))
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

