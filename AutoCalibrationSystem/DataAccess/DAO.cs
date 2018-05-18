using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem.DataAccess
{
    public class DAO
    {
        private static readonly string connString = @"Server=localhost;DataBase=auto_calibration_system;User ID=root;Password=ctseu20;Port=3306;Character Set=utf8";
        public static void AddRow2TableCali(DataTable dt, List<CaliItem> list, string mode,int instruId) 
        {
            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["mode"] = mode;
                row["instrument_id"] = instruId;
                row["source"] = list[i].Source;
                row["stand_out"] = list[i].StandOut;
                row["test_out"] = list[i].TestOut;                
                dt.Rows.Add(row);
            }
        }
        public static void AddRow2TableDivider(DataTable dt, List<DividerItem> list, int dividerId)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["divider_id"] = dividerId;
                row["source_volt"] = list[i].Source;
                row["source_freq"] = list[i].Frequency;
                row["stand_divider_volt"] = list[i].StandOut;
                row["test_divider_volt"] = list[i].TestOut;
                row["temperature"] = list[i].Temperature;
                row["humidity"] = list[i].Humidity;
                dt.Rows.Add(row);
            }
        }
        //批量插入校准数据到cali_data表
        public static bool SaveCaliDataAll(CaliData caliData) 
        {
            DataBase db = new DataBase();
            DataTable sourceTable = new DataTable();
            sourceTable.TableName = "cali_data";
            //sourceTable.Columns.Add("id",typeof(int));
            sourceTable.Columns.Add("instrument_id", typeof(int));
            sourceTable.Columns.Add("mode", typeof(string));
            sourceTable.Columns.Add("source", typeof(float));
            sourceTable.Columns.Add("stand_out", typeof(float));
            sourceTable.Columns.Add("test_out", typeof(float));

            int instruId = 1;
            List<CaliItem> list = null;            
            list = caliData.iaciData;
            AddRow2TableCali(sourceTable, list, "IACI", instruId);
            
            list = caliData.iacfData;
            AddRow2TableCali(sourceTable, list, "IACF", instruId);
            
            list = caliData.idcData;
            AddRow2TableCali(sourceTable, list, "IDC", instruId);
            
            list = caliData.vacfData;
            AddRow2TableCali(sourceTable, list, "VACF", instruId);
            
            list = caliData.vacvData;
            AddRow2TableCali(sourceTable, list, "VACV", instruId);
            
            list = caliData.vdcData;
            AddRow2TableCali(sourceTable, list, "VDC", instruId);
            
            try {
                BulkInsert(sourceTable);
            }
            catch (Exception)
            {
                return false;
            }
            return true;          
        }
        //根据测量模式批量插入分压器数据到divider_data表
        public static bool SaveDividerDataByMode(DividerData dividerData,EnumMode mode)
        {
            DataBase db = new DataBase();
//            string sql = "ALTER TABLE `divider_data` AUTO_INCREMENT = MAX(id) + 1";
//            db.ExecuteQuery(sql);
            DataTable sourceTable = new DataTable();
            sourceTable.TableName = "divider_data";
            sourceTable.Columns.Add("divider_id", typeof(int));
            sourceTable.Columns.Add("source_volt", typeof(float));
            sourceTable.Columns.Add("source_freq", typeof(float));
            sourceTable.Columns.Add("stand_divider_volt", typeof(float));
            sourceTable.Columns.Add("test_divider_volt", typeof(float));
            sourceTable.Columns.Add("temperature", typeof(float));
            sourceTable.Columns.Add("humidity", typeof(float));

            int dividerId = 1;
            List<DividerItem> list = null;
            switch (mode)
            {
                case EnumMode.Divider_V_DCP:
                    list = dividerData.voltageDCPData;
                    break;
                case EnumMode.Divider_V_DCN:
                    list = dividerData.voltageDCNData;
                    break;
                case EnumMode.Divider_V_AC:
                    list = dividerData.voltageACData;
                    break;
                case EnumMode.Divider_F:
                    list = dividerData.frequencyData;
                    break;
            }
            AddRow2TableDivider(sourceTable, list, dividerId);
            try
            {
                BulkInsert(sourceTable);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        //所有模式批量插入分压器数据到divider_data表
        public static bool SaveDividerDataAll(DividerData dividerData)
        {
            DataBase db = new DataBase();
            DataTable sourceTable = new DataTable();
            sourceTable.TableName = "divider_data";
            sourceTable.Columns.Add("divider_id", typeof(int));
            sourceTable.Columns.Add("source_volt", typeof(float));
            sourceTable.Columns.Add("source_freq", typeof(float));
            sourceTable.Columns.Add("stand_divider_volt", typeof(float));
            sourceTable.Columns.Add("test_divider_volt", typeof(float));
            sourceTable.Columns.Add("temperature", typeof(float));
            sourceTable.Columns.Add("humidity", typeof(float));

            int dividerId = 1;
            List<DividerItem> list = null;
            list = dividerData.voltageDCPData;
            AddRow2TableDivider(sourceTable, list, dividerId);

            list = dividerData.voltageDCNData;
            AddRow2TableDivider(sourceTable, list, dividerId);

            list = dividerData.voltageACData;
            AddRow2TableDivider(sourceTable, list, dividerId);

            list = dividerData.frequencyData;
            AddRow2TableDivider(sourceTable, list, dividerId);

            try
            {
                BulkInsert(sourceTable);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///大批量数据插入,返回成功插入行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        /// <returns>返回成功插入行数</returns>
        public static int BulkInsert(DataTable table)
        {
            if (string.IsNullOrEmpty(table.TableName)) 
                throw new Exception("请给DataTable的TableName属性附上表名称");
            if (table.Rows.Count == 0) 
                return 0;
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = table.TableName,
                    };
                    bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList());
                    insertCount = bulk.Load();
                    tran.Commit();
                }
                catch (MySqlException ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
            File.Delete(tmpPath);
            return insertCount;
        }

        /// <summary>
        ///将DataTable转换为标准的CSV
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回标准的CSV</returns>
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        //根据时间范围获取历史数据
        public static DataTable SelectDividerDataByTime(DateTime start,DateTime end,int dividerId) {
            //字符串前加@可以随意换行
            string sql = @"select source_volt,source_freq,stand_divider_volt,test_divider_volt,temperature,humidity,save_time from divider_data 
                                 where divider_id = ?divider_id and save_time >= ?start_time and save_time <= ?end_time";
            MySqlDateTime startTime = new MySqlDateTime(start);
            MySqlDateTime endTime = new MySqlDateTime(end);

            MySqlParameter[] Params = new MySqlParameter[]
            {
                new MySqlParameter("?divider_id",dividerId),                
                new MySqlParameter("?start_time",startTime),
                new MySqlParameter("?end_time",endTime)
            };
            DataBase db = new DataBase();
            DataTable dt = db.ExecuteQuery(sql,Params);
            if (dt != null)
            {
                Console.WriteLine(dt.Rows[0][0].ToString());
            }
            return dt;
        }
    }
}
