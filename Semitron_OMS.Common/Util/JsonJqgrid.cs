using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Semitron_OMS.Common
{
    public class JsonJqgrid
    {
        /// <summary>
        /// 针对Jqgrid的JSON转换类。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string JsonForJqgrid(DataTable dt, int page, int total, Dictionary<string, string> dic)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");

            jsonBuilder.Append("\"page\":" + page.ToString() + ",\"total\":" + total.ToString() + ",\"rows\":[");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    if (j == 0)
                    {
                        jsonBuilder.Append("\"id\":\"");
                        jsonBuilder.Append(dt.Rows[i][j].ToString());
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(",\"cell\":[");
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\"", "“").Replace("\\", "|").Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace("'", "“").Replace("\0", ""));
                        jsonBuilder.Append("\",");
                    }
                    else
                    {
                        if (dt.Columns[j].DataType == DateTime.Now.GetType())
                        {
                            if (dt.Rows[i][j].ToString() != "")
                            {
                                DateTime nowTime = (DateTime)(dt.Rows[i][j]);
                                jsonBuilder.Append("\"");
                                jsonBuilder.Append(nowTime.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                jsonBuilder.Append("\",");
                            }
                            else
                            {
                                jsonBuilder.Append("\"");
                                jsonBuilder.Append("");
                                jsonBuilder.Append("\",");
                            }
                        }
                        else
                        {
                            jsonBuilder.Append("\"");
                            jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\"", "”").Replace("\\", "|").Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace("'", "“").Replace("\0", ""));
                            jsonBuilder.Append("\",");
                        }


                    }

                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("],");
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }

            jsonBuilder.Append("]");
            if (dic != null && dic.Count > 0)
            {
                foreach (var item in dic)
                {
                    jsonBuilder.Append(",\"" + item.Key + "\":" + item.Value + "");
                }
            }

            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 针对Jqgrid的JSON转换类。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string JsonForJqgrid(DataTable dt, int page, int total)
        {
            return JsonForJqgrid(dt, page, total, null);
        }

    }
}
