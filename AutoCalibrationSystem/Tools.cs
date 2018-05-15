using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class Tools
    {
        /// <summary>
        /// 数据库中的一个cell转换成为整型
        /// 采用的方法是先进性强制转换，在进行字符串中间变量
        /// -------------------------------
        /// NEW SHW1216 00.12 无异常
        /// </summary>
        /// <param name="obj">数据库中的一个CELL的内容</param>
        /// <returns></returns>
        public static int DB2INT(object obj)
        {
            int ret = 0;
            try
            {
                ///如果不是NULL ，那么先强制转换，不行的话在用字符串作为中间变量
                if (!(obj is DBNull))
                {
                    try
                    {
                        ret = (int)obj;
                    }
                    catch
                    {
                        ret = int.Parse(obj.ToString());
                    }
                }
            }
            catch
            {
            }
            return ret;
        }
    }
}
