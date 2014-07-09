using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Collections;
using System.IO;
using System.Data;

namespace Semitron_OMS.DAL.Common
{ /// <summary>
    /// 统一操作excel。
    /// </summary>
    public class Excel
    {
        Workbook workbook = null;
        Worksheet worksheet = null;
        public Excel()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExcelName"></param>
        public Excel(string SheetName)
        {
            workbook = new Workbook();
            workbook.Worksheets.Clear();
            worksheet = workbook.Worksheets.Add(SheetName);
        }
        public void creatfilePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="CellIndex"></param>
        /// <param name="CellName"></param>
        /// 
        public void CreatFile(string filePath, string[] headers, IEnumerable<string[]> itemValues)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            try
            {
                if (headers != null)
                {
                    worksheet.Cells.ImportObjectArray(headers, worksheet.Cells.Rows.Count, 0, false);
                }
                if (itemValues != null)
                {
                    foreach (var values in itemValues)
                    {
                        if (values == null) continue;

                        worksheet.Cells.ImportObjectArray(values, worksheet.Cells.Rows.Count, 0, false);
                    }
                }
                workbook.Save(filePath, FileFormatType.Default);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="CellIndex"></param>
        /// <param name="CellName"></param>
        public void CreatFile(string filePath,System.Data.DataTable dt)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            try
            {
                if (dt != null)
                {
                    worksheet.Cells.ImportDataTable(dt, true, 0, 0, dt.Rows.Count, dt.Columns.Count, false, "yyyy-mm-dd hh:mm:ss");
                    worksheet.AutoFitColumns();
                }
                workbook.Save(filePath, FileFormatType.Default);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class FileExcelDAl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "file_system/ExportExcelFile/";
        string UploadFilepath = AppDomain.CurrentDomain.BaseDirectory + "file_system/Upload/";
        /// <summary>
        /// 创建excel文件
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="filepath"></param>
        /// <param name="headers"></param>
        /// <param name="itemValues"></param>
        public void CreateFile(string sheetname, string filepath, string[] headers, IEnumerable<string[]> itemValues)
        {
            Excel cel = new Excel(sheetname);
            cel.CreatFile(filepath, headers, itemValues);
        }
        /// <summary>
        /// 创建excel文件
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="filepath"></param>
        /// <param name="headers"></param>
        /// <param name="itemValues"></param>
        public void CreateFile(string sheetname, string filename, System.Data.DataTable dt)
        {
            Excel cel = new Excel(sheetname);
            cel.creatfilePath(path);
            cel.CreatFile(path + filename, dt);
        }
        /// <summary>
        /// 创建excel文件
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="filepath"></param>
        /// <param name="headers"></param>
        /// <param name="itemValues"></param>
        public void CreateFilePath(string sheetname, string filePath, System.Data.DataTable dt)
        {
            Excel cel = new Excel(sheetname);
            cel.creatfilePath(UploadFilepath);
            cel.CreatFile(filePath, dt);
        }
        Aspose.Cells.Workbook workbook = null;
        Aspose.Cells.Worksheet sheet = null;
        Aspose.Cells.Cells cells = null;
        /// <summary>
        /// 列头列表
        /// </summary>
        public List<string> columns = new List<string>();
        public void ReadFile(string filepath)
        {
            int colCount = 0;
            int rowCount = 0;
            IEnumerator itor = null;

            try
            {
                workbook = new Aspose.Cells.Workbook(File.OpenRead(filepath));
                sheet = workbook.Worksheets[0];
                cells = sheet.Cells;
                rowCount = cells.Rows.Count;
                if (rowCount <= 0)
                {
                    throw new Exception("导入文件为空！");
                }
                Aspose.Cells.Row row = cells.Rows[0];
                itor = row.GetEnumerator();
                int i = 0;
                while (itor.MoveNext())
                {
                    Cell cell = row[i];
                    columns.Add(cell.StringValue.TrimStart(' ').Trim());
                    i++;
                }
            }
            catch
            { }
        }
        public DataTable ReadFileSingle(string filepath)
        {
            int colCount = 0;
            int rowCount = 0;
            try
            {
                ReadFile(filepath);
                workbook = new Aspose.Cells.Workbook(File.OpenRead(filepath));
                sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;
                rowCount = cells.Rows.Count;
                colCount =columns.Count;
                DataTable dt = cells.ExportDataTableAsString(0, 0, rowCount, colCount, true);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        int i = 1;
        public bool ReadNext()
        {
            return i < cells.Rows.Count;
        }
        /// <summary>
        /// 读取当前行
        /// </summary>
        /// <returns></returns>
        public List<string> GetCurrentRow()
        {
            try
            {
                Aspose.Cells.Row row = cells.Rows[i];
                List<string> stslists = new List<string>();
                for (int index = 0; index < columns.Count; index++)
                {
                    if (row[index].Value == null)
                    { stslists.Add(string.Empty); continue; }
                    stslists.Add(row[index].Value.ToString());
                }
                i++;
                return stslists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }
    }
}
