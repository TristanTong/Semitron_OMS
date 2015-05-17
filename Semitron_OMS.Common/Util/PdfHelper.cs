using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI;
using System.IO;
using System.Collections;
using iTextSharp.text.html.simpleparser;

namespace Semitron_OMS.Common.Util
{
    public class PdfHelper
    {
        private Document document = null;

        public void CreateInstance(Rectangle rec, int left, int right, int top, int buttom)
        {
            document = new Document(rec, left, right, top, buttom);
        }

        public void CreateInstance()
        {
            CreateInstance(PageSize.A4, 80, 50, 30, 65);
        }

        public void ConvertHtmlToPdf(string strHTMLpath, string strTarPDFpath)
        {
            StringBuilder strData = new StringBuilder(string.Empty);

            //Call the data from the HTML file and render the file:
            StringWriter sw = new StringWriter();
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine(Environment.NewLine);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            StreamWriter strWriter = new StreamWriter(strHTMLpath, false, Encoding.UTF8);
            strWriter.Write("<html><head></head><body>" + htw.InnerWriter.ToString() + "</body></html>");
            strWriter.Close();
            strWriter.Dispose();
            //Use the parser to convert the HTML content to a PDF:
            iTextSharp.text.html.simpleparser.StyleSheet styles
                = new iTextSharp.text.html.simpleparser.StyleSheet();
            styles.LoadTagStyle("ol", "leading", "16,0");
            //Set the font styles for the elements on page and add the page items:
            styles.LoadTagStyle("li", "face", "garamond");
            styles.LoadTagStyle("span", "size", "8px");
            styles.LoadTagStyle("body", "font-family", "times new roman");
            styles.LoadTagStyle("body", "font-size", "12px");

            PdfWriter.GetInstance(document, new FileStream(strTarPDFpath, FileMode.Create));
            document.Open();
            document.NewPage();

            List<IElement> objects = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StreamReader(strHTMLpath, Encoding.Default), styles);

            for (int k = 0; k < objects.Count; k++)
            {
                document.Add((IElement)objects[k]);
            }
        }

        public void ReleaseInstance()
        {
            //Clear all the variables used from memory and close:
            document.Close();
        }

        private MemoryStream createPDF(string html)
        {
            MemoryStream msOutput = new MemoryStream();
            TextReader reader = new StringReader(html);

            // step 2:
            // we create a writer that listens to the document
            // and directs a XML-stream to a file
            PdfWriter writer = PdfWriter.GetInstance(document, msOutput);

            // step 3: we create a worker parse the document
            HTMLWorker worker = new HTMLWorker(document);

            // step 4: we open document and start the worker on the document
            document.Open();
            worker.StartDocument();

            // step 5: parse the html into the document
            worker.Parse(reader);

            // step 6: close the document and the worker
            worker.EndDocument();
            worker.Close();
            document.Close();

            return msOutput;
        }
    }
}
