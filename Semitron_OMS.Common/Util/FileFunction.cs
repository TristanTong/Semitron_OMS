using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace Semitron_OMS.Common
{
    public class FileFunction
    {
        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>object</returns>
        public static object DeserializeFile(string path)
        {
            FileStream fs = null;
            object o = null;
            try
            {
                if (File.Exists(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    string Name = string.Empty;
                    o = (object)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogFatal("反序列化文件夹时出现异常。路径：" + path, ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }

            }
            return o;
        }



        /// <summary>
        /// 序列化文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="um">对象</param>
        public static void SerializeFile1(string path, object um)
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf("\\")));
                }
                BinaryFormatter bf = new BinaryFormatter();
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                bf.Serialize(fs, um);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogFatal("序列化文件夹时出现异常。路径：" + path, ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 序列化文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="um">对象</param>
        public static void SerializeFile(string path, object um)
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf("\\")));
                }
                if (!File.Exists(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    bf.Serialize(fs, um);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogFatal("序列化文件夹时出现异常。路径：" + path, ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
            }
        }

        //读取EXCEL的方法    (用范围区域读取数据)  
        private void OpenExcel(string strFileName)
        {
            object missing = System.Reflection.Missing.Value; Application excel = new Application();
            //lauch excel application      
            if (excel == null) { }
            else
            {
                excel.Visible = false; excel.UserControl = true;
                // 以只读的形式打开EXCEL文件           
                Workbook wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing, missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄          
                Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
                //取得总记录行数    (包括标题列)          
                int rowsint = ws.UsedRange.Cells.Rows.Count;
                //得到行数          
                //int columnsint = mySheet.UsedRange.Cells.Columns.Count;
                //得到列数           
                //取得数据范围区域   (不包括标题列)              
                Range rng1 = ws.Cells.get_Range("B2", "B" + rowsint);
                //item           
                Range rng2 = ws.Cells.get_Range("K2", "K" + rowsint);
                //Customer          
                object[,] arryItem = (object[,])rng1.Value2;
                //get range's value          
                object[,] arryCus = (object[,])rng2.Value2;
                //将新值赋给一个数组      
                string[,] arry = new string[rowsint - 1, 2];
                for (int i = 1; i <= rowsint - 1; i++)
                {
                    //Item_Code列            
                    arry[i - 1, 0] = arryItem[i, 1].ToString();
                    //Customer_Name列             
                    arry[i - 1, 1] = arryCus[i, 1].ToString();
                }
                //Response.Write(arry[0, 0] + "   /   " + arry[0, 1] + "#" + arry[rowsint - 2, 0] + "   /   " + arry[rowsint - 2, 1]);
            }
            excel.Quit();
            excel = null;
            Process[] procs = Process.GetProcessesByName("excel");
            foreach (Process pro in procs)
            {
                pro.Kill();
                //没有更好的方法,只有杀掉进程     
            }
            GC.Collect();
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogFatal("创建目录时出现异常。路径：" + path, ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }

        }
    }


    public class ExcelClass
    {
        public static List<string> ReadStringTable(Stream input)
        {

            List<string> stringTable = new List<string>();


            using (XmlReader reader = XmlReader.Create(input))
            {
                for (reader.MoveToContent(); reader.Read(); )
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "t")
                    {

                        stringTable.Add(reader.ReadElementContentAsString());
                    }

                }

            }

            return stringTable;
        }

        public static List<string> getWorkbookNames(Stream workbook, int count)
        {

            List<string> workBookTable = new List<string>();
            int Count = count;


            using (XmlReader reader = XmlReader.Create(workbook))
            {
                for (reader.MoveToContent(); reader.Read(); )
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "sheets")
                    {

                        reader.Read();

                        if (reader.Name == "sheet")
                        {

                            for (int i = 0; i < Count; i++)
                            {


                                workBookTable.Add(reader.GetAttribute("name"));

                                reader.Read();

                            }
                        }
                    }


            }

            return workBookTable;
        }

        public static object[] ReadWorksheet(Stream input, List<string> stringTable, Stream style, int rowID)
        {

            List<List<string>> workbook = new List<List<string>>();
            List<List<string>> headers = new List<List<string>>();
            List<string> cHeader = new List<string>();
            List<string> cValue = new List<string>();
            List<string> row = new List<string>();

            Stream StyleStream = style;
            String type;
            String STRFinder;
            Int32 StyleIndex = 0;
            Int32 StyleInt = 0;
            String nullFinder;
            Int32 val = -1;
            String firstLook = null;
            int headerCount = 0;
            bool runRow = false;
            List<string> styleValues = new List<string>();
            int getRowID = rowID;


            string newstr = string.Empty;
            object[] objects = new object[2];

            if (getRowID == 0)
            {
                getRowID = 1;
            }


            StyleStream.Seek(0, SeekOrigin.Begin);
            XmlReader Stylereader = Stylereader = XmlReader.Create(StyleStream);
            Stylereader.MoveToContent();

            while (Stylereader.Read())
            {

                if (Stylereader.NodeType == XmlNodeType.Element)
                {
                    switch (Stylereader.Name)
                    {

                        case "cellXfs":

                            Int16 Count = Convert.ToInt16(Stylereader.GetAttribute("count"));

                            Stylereader.Read();

                            for (int i = 0; i < Count; i++)
                            {

                                if (Stylereader.Name == "xf")
                                {

                                    styleValues.Add(Stylereader.GetAttribute("numFmtId"));
                                    Stylereader.Skip();

                                }
                            }
                            break;
                    }


                }
            }


            using (XmlReader reader = XmlReader.Create(input))
            {
                int currentPostion = 0;
                int postionFound = 0;

                for (reader.MoveToContent(); reader.Read(); )

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {

                            case "row":

                                int getRowVale = Convert.ToInt32(reader.GetAttribute("r"));

                                if (getRowVale >= getRowID)
                                {
                                    runRow = true;
                                }
                                else
                                {
                                    runRow = false;

                                }
                                break;

                            case "c":

                                if (runRow == true)
                                {

                                    type = reader.GetAttribute("t");
                                    nullFinder = reader.GetAttribute("r");
                                    if (nullFinder == "K3")
                                    {

                                    }
                                    StyleIndex = Convert.ToInt16(reader.GetAttribute("s"));
                                    STRFinder = reader.GetAttribute("t");

                                    if (StyleIndex > 0)
                                    {

                                        StyleInt = Convert.ToInt32(styleValues[StyleIndex]);
                                    }
                                    newstr = string.Empty;


                                    postionFound = 0;

                                    if (nullFinder != null)
                                    {


                                        foreach (char c in nullFinder)
                                        {
                                            try
                                            {
                                                int i = (int)c;
                                                if ((i >= 48) && (i <= 57))
                                                {
                                                    continue;
                                                }
                                            }
                                            catch (Exception)
                                            {

                                            }

                                            newstr += c.ToString();
                                        }
                                        foreach (char c in newstr)
                                        {
                                            postionFound += ((int)c);
                                        }

                                        if (newstr.Length == 1)
                                        {
                                            postionFound = postionFound - 65;
                                        }
                                        else if (newstr.Length == 2)
                                        {
                                            postionFound = postionFound - 104;
                                        }
                                        else if (newstr.Length == 3)
                                        {
                                            postionFound = postionFound - 119;
                                        }


                                        if (currentPostion != postionFound)
                                        {
                                            for (int i = currentPostion; i < postionFound; i++)
                                            {

                                                row.Add(Convert.ToString(null));

                                            }

                                            currentPostion = postionFound + 1;
                                        }
                                        else
                                        {
                                            currentPostion = postionFound + 1;
                                        }

                                    }


                                    try
                                    {
                                        //skips the formula element to the value



                                        //only allows "c" with elements skips empty elemets
                                        if ((bool)reader.IsEmptyElement == false)
                                        {
                                            reader.Read();


                                            if (reader.Name.Equals("f"))
                                            {
                                                reader.Skip();
                                                firstLook = reader.ReadElementContentAsString().ToString();
                                                row.Add(firstLook);
                                                break;
                                            }
                                            else
                                            {

                                                firstLook = reader.ReadElementContentAsString().ToString();


                                                if (firstLook.Contains("."))
                                                {
                                                    row.Add(firstLook);
                                                    break;
                                                }

                                                else
                                                {


                                                    val = int.Parse(firstLook);

                                                }

                                            }


                                        }
                                        else
                                        {

                                            break;

                                        }
                                    }
                                    catch (FormatException)
                                    {

                                        //MessageBox.Show("invaild data type " + reader.ReadElementContentAsString());

                                    }
                                }
                                else
                                {
                                    break;
                                }


                                if (type == "s")
                                {
                                    if (headerCount == 0)
                                    {


                                        cHeader.Add(newstr);
                                        cValue.Add(stringTable[val]);

                                        newstr = string.Empty;

                                    }
                                    else
                                    {

                                        row.Add(stringTable[val]);
                                    }

                                    break;
                                }


                                else if (StyleInt > 13 && StyleInt < 23)
                                {
                                    DateTime dt = DateTime.FromOADate(val);


                                    String DateandTime = Convert.ToString(dt.ToShortDateString());


                                    row.Add(DateandTime);

                                    StyleInt = 0;
                                    StyleIndex = 0;

                                }
                                //else if (STRFinder == "str")
                                //{
                                //    //string readSTR = reader.ReadElementContentAsString().ToString();
                                //    row.Add(Convert.ToString(readSTR));
                                //    break;
                                //}
                                else
                                {

                                    row.Add(Convert.ToString(val));

                                }

                                break;
                        }

                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                        if (reader.Name == "row")
                        {

                            if (headerCount == 0 && cValue.Count != 0)
                            {
                                headers.Add(cHeader);
                                headers.Add(cValue);
                                headerCount += 1;
                                currentPostion = 0;
                                postionFound = 0;


                            }
                            else if (row.Count != 0)
                            {

                                if (row.Count != cHeader.Count)
                                {
                                    for (int i = row.Count; i < cHeader.Count; i++)
                                    {
                                        row.Add(Convert.ToString(null));
                                    }
                                }

                                List<string> newRow = new List<string>();


                                foreach (string item in row)

                                    newRow.Add(item);
                                workbook.Add(newRow);

                                currentPostion = 0;
                                postionFound = 0;
                                row.Clear();
                            }
                        }


                objects[0] = workbook;
                objects[1] = headers;
                return objects;
            }
        }
    }

    public class UnZipper
    {
        private Stream stream;



        public UnZipper(Stream zipFileStream)
        {
            this.stream = zipFileStream;
        }


        //public Stream GetFileStream(string filename)
        //{
        //    Uri fileUri = new Uri(filename, UriKind.Relative);
        //    StreamResourceInfo info = new StreamResourceInfo(this.stream, null);
        //    if (this.stream is System.IO.FileStream)
        //        this.stream.Seek(0, SeekOrigin.Begin);
        //    StreamResourceInfo stream = System.Windows.Application.GetResourceStream(info, fileUri);
        //    if (stream != null)
        //        return stream.Stream;
        //    return null;


        //}


        public IEnumerable<string> GetFileNamesInZip()
        {
            BinaryReader reader = new BinaryReader(stream);
            stream.Seek(0, SeekOrigin.Begin);
            string name = null;
            List<string> names = new List<string>();
            while (ParseFileHeader(reader, out name))
            {
                names.Add(name);
            }
            return names;
        }


        private static bool ParseFileHeader(BinaryReader reader, out string filename)
        {
            filename = null;
            if (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int headerSignature = reader.ReadInt32();
                if (headerSignature == 67324752)
                {
                    reader.BaseStream.Seek(2, SeekOrigin.Current);

                    short genPurposeFlag = reader.ReadInt16();
                    if (((((int)genPurposeFlag) & 0x08) != 0))
                        return false;
                    reader.BaseStream.Seek(10, SeekOrigin.Current);
                    int compressedSize = reader.ReadInt32();
                    int unCompressedSize = reader.ReadInt32();
                    short fileNameLenght = reader.ReadInt16();
                    short extraFieldLenght = reader.ReadInt16();
                    filename = new string(reader.ReadChars(fileNameLenght));
                    if (string.IsNullOrEmpty(filename))
                        return false;

                    reader.BaseStream.Seek(extraFieldLenght + compressedSize, SeekOrigin.Current);
                    if (unCompressedSize == 0)
                        return ParseFileHeader(reader, out filename);
                    else
                        return true;
                }
            }
            return false;
        }
    }
}
