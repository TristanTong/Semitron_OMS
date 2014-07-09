using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
namespace Semitron_OMS.DAL.Common
{
    public enum ExportFileType
    {
        Xls,
        Text,
        Csv,
        Xlsx
    }

    public class FileDAL
    {
        public const string EXCELCONTENTTYPE = "application/vnd.ms-excel";
        public const string TXTCONTENTTYPE = "text/plain";
        public const string CSVCONTENTTYPE = "application/vnd.ms-excel";

        public string FileExtension { get; private set; }
        public string FilePath { get; private set; }
        public ExportFileType ExportType { get; private set; }
        StringBuilder sb = new StringBuilder();
        public string ContentType { get; set; }
        public string FullFileName { get; set; }

        public FileDAL(string fileName, int exportFileType)
        {


            switch (exportFileType)
            {
                case (int)ExportFileType.Xls:
                    this.ExportType = ExportFileType.Xls;
                    ContentType = FileDAL.EXCELCONTENTTYPE;
                    FullFileName = fileName + ".xls";
                    this.FileExtension = ".xls";
                    break;
                case (int)ExportFileType.Text:
                    this.ExportType = ExportFileType.Text;
                    ContentType = FileDAL.TXTCONTENTTYPE;
                    FullFileName = fileName + ".txt";
                    this.FileExtension = ".txt";
                    break;
                case (int)ExportFileType.Csv:
                    this.ExportType = ExportFileType.Csv;
                    ContentType = FileDAL.CSVCONTENTTYPE;
                    FullFileName = fileName + ".csv";
                    this.FileExtension = ".csv";
                    //Separator = ",";
                    break;
                case (int)ExportFileType.Xlsx:
                    this.ExportType = ExportFileType.Xlsx;
                    ContentType = FileDAL.EXCELCONTENTTYPE;
                    FullFileName = fileName + ".xlsx";
                    this.FileExtension = ".xlsx";
                    break;
                default:
                    break;
            }
        }
    }
}
