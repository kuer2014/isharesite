using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetterSite.Common
{
  public  class ExcelHelper
    {
        #region 导出Excel
        //使用示例
        //IList<ViewUserInfo> userList = viewUserInfoService.GetUserInfoListAll();
        //DataTable dt = IListOut(userList);
        //WriteExcel(dt, "d:\\a.xls");
     static   public void WriteExcel(DataTable ds, string path)
        {
            long totalCount = ds.Rows.Count;
            Thread.Sleep(1000);
            long rowRead = 0;
            float percent = 0;

            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < ds.Columns.Count; k++)
            {
                sb.Append(ds.Columns[k].ColumnName.ToString() + "\t");
            }
            sb.Append(Environment.NewLine);
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                //rowRead++;
                //percent = ((float)(100 * rowRead)) / totalCount;
               /// this.labPercent.Text = "<a href='UserInfo.xls' target='_blank'>此处下载</a>";
                for (int j = 0; j < ds.Columns.Count; j++)
                {
                    sb.Append(ds.Rows[i][j].ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
            }
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
        }
       static public DataTable IListOut<T>(IList<T> ResList)
        {
            DataTable TempDT = new DataTable();

            //此处遍历IList的结构并建立同样的DataTable
            System.Reflection.PropertyInfo[] p = ResList[0].GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in p)
            {
                //TempDT.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
                TempDT.Columns.Add(pi.Name, System.Type.GetType("System.String"));
                
            }

            for (int i = 0; i < ResList.Count; i++)
            {
                ArrayList TempList = new ArrayList();
                //将IList中的一条记录写入ArrayList
                foreach (System.Reflection.PropertyInfo pi in p)
                {
                    object oo = pi.GetValue(ResList[i], null);
                    TempList.Add(oo);
                }

                object[] itm = new object[p.Length];
                //遍历ArrayList向object[]里放数据
                for (int j = 0; j < TempList.Count; j++)
                {
                    itm.SetValue(TempList[j], j);
                }
                //将object[]的内容放入DataTable
                TempDT.LoadDataRow(itm, true);
            }
            //返回DataTable
            return TempDT;
        }


        #endregion
    }
}
