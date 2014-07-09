using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Semitron_OMS.Common
{
    public class CommonFilter
    {

        /// <summary>
        /// 过滤黑名单
        /// </summary>
        /// <param name="linkDT">联系人表</param>
        /// <param name="blcakDT">黑名单表</param>
        /// <returns></returns>
        public static DataTable FilterPhone(DataTable linkDT, DataTable blcakDT, out int filterNum)
        {
            //1.求交集，得到相同的数据。
            //2.求差集，得到不属于交集结果集中的数据。
            //3.转换差集为table，即最终的结果。
            DataTable resultDT = null;
            EnumerableRowCollection<DataRow> linkCollection = linkDT.AsEnumerable();


            IEnumerable<DataRow> IntersectEnumerable = linkCollection.Intersect(blcakDT.AsEnumerable(), new ProductRowComparer());
            filterNum = IntersectEnumerable.Count();
            IEnumerable<DataRow> resultEnumerable = null;
            if (filterNum > 0)
            {
                resultEnumerable = linkCollection.Except(IntersectEnumerable, new ProductRowComparer());
                if (resultEnumerable.Count() > 0)
                {
                    resultDT = resultEnumerable.CopyToDataTable();
                }
                
            }
            return resultDT;
        }
    }

    //自定义行比较器
    public class ProductRowComparer : IEqualityComparer<DataRow>
    {
        public bool Equals(DataRow row1, DataRow row2)
        {
            return (row1[0].ToString() == row2[0].ToString());
        }
        public int GetHashCode(DataRow t)
        {
            return t.ToString().GetHashCode();
        }

    }
}
