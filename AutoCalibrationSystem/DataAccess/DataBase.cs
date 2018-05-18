using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace AutoCalibrationSystem.DataAccess
{
    public class DataBase
    {
        private static readonly string connString = @"Server=localhost;DataBase=auto_calibration_system;User ID=root;Password=ctseu20;Port=3306;Character Set=utf8";
        public MySqlConnection conn;
        //打开数据库连接
        public void Open(){
            if (connString != null)
            {
                conn = new MySqlConnection(connString);
            }
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }   
        }
        #region  关闭连接
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void Close()
        {
            if (conn != null)
                conn.Close();
        }
        #endregion

        #region 释放数据库连接资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 确认连接是否已经关闭
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }
        #endregion
        public DataTable ExecuteQuery(string sql)
        {
            Open();
            DataSet ds = new DataSet();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            Close();
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        public DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            Open();
            DataSet ds = new DataSet();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (MySqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            Close();
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        #region insert 部分


        /// <summary>
        /// 向数据库中插入一条字符串
        /// 这个子函数主要用于那些关键字时UserID的表
        /// -----------------------------------------
        /// </summary>
        /// <param name="toEditCols">需要插入的字段的列名称</param>
        /// <param name="toEditParams">需要插入的额字段的值</param>
        /// <param name="tablename">表名</param>
        /// <returns>执行顺利</returns>
        public int TableFromKeyInt_Insert_Identity(List<string> toEditCols,
                    List<object> toEditParams,
                    string tablename)
        {
            int ret = 0;
            try
            {
                //插入一条
                Open();
                string sqls = string.Empty;
                string strpp = string.Empty;
                string parmparms = string.Empty;

                MySqlParameter[] parm = new MySqlParameter[toEditCols.Count];
                for (int i = 0; i < toEditCols.Count; i++)
                {
                    MySqlParameter temp = new MySqlParameter("@parm" + i.ToString(), toEditParams[i]);
                    parm[i] = (temp);
                    strpp += "," + toEditCols[i];
                    parmparms += "," + parm[i].ParameterName;
                }
                if (!string.IsNullOrEmpty(strpp))
                {
                    strpp = strpp.Substring(1);
                    parmparms = parmparms.Substring(1);
                }

                sqls = "Insert into " + tablename + " ( " + strpp + " ) values (" + parmparms + "); select @@IDENTITY ";

                ret = Tools.DB2INT(ExecuteQuery(sqls, parm)
                                  .Rows[0][0]);
                Close();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                ret = 0;
            }
            return ret;
        }
        #endregion


    }
}
