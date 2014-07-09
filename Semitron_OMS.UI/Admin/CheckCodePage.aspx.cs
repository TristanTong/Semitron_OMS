using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Semitron_OMS.Common;
using Semitron_OMS.UI;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI
{
    public partial class CheckCodePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            CreateCheckCodeImage(GenerateCheckCode());
        }

        private string GenerateCheckCode()
        {
            //定义验证码长度
            int CODELENGTH = 4;
            if (Request.QueryString["Codelength"] != null)
            {
                CODELENGTH = PublicFunction.ParseToInt(Request.QueryString["Codelength"].ToString(), 4);
            }
            int number;
            string RandomCode = string.Empty;
            Random r = new Random();
            for (int i = 0; i < CODELENGTH; i++)
            {
                number = r.Next();
                //字符从0~9, A~Z中随机产生,对应的ASCII码分别为48~57, 65~90 a-z 97~122
                number = number % 36;
                if (number < 10)
                    number += 48;
                else
                    number += 55;
                RandomCode += ((char)number).ToString();
            }
            //在Cookie中保存验证码
            Response.Cookies.Add(new HttpCookie("CheckCode", RandomCode));
            Session["CheckCode"] = RandomCode;
            return RandomCode;
        }

        private void CreateCheckCodeImage(string checkCode)
        {
            //若验证码为空，则直接返回
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return;
            //根据验证码的长度确定输出图片的宽度
            int iWidth = (int)Math.Ceiling(checkCode.Length * 15m);
            int iHeight = 20;
            //创建图像
            Bitmap image = new Bitmap(iWidth, iHeight);
            //从图像获取一个绘图面
            Graphics g = Graphics.FromImage(image);

            try
            {
                Random r = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线10条
                for (int i = 0; i < 10; i++)
                {
                    int x1 = r.Next(image.Width);
                    int x2 = r.Next(image.Width);
                    int y1 = r.Next(image.Height);
                    int y2 = r.Next(image.Height);
                    //用银色画出噪音线
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                //画图片的前景噪音点50个
                for (int i = 0; i < 50; i++)
                {
                    int x = r.Next(image.Width);
                    int y = r.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(r.Next()));
                }
                //画图片的框线
                g.DrawRectangle(new Pen(Color.SaddleBrown), 0, 0, image.Width - 1, image.Height - 1);
                //定义绘制文字的字体
                Font f = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                //线性渐变画刷
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Purple, 1.2f, true);
                g.DrawString(checkCode, f, brush, 2, 2);
                //创建内存流用于输出图片
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    //图片格式制定为png
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    //清除缓冲区流中的所有输出
                    Response.ClearContent();
                    //输出流的HTTP MIME类型设置为"image/Png"
                    Response.ContentType = "image/Png";
                    //输出图片的二进制流
                    Response.BinaryWrite(ms.ToArray());
                }
            }
            finally
            {
                //释放Bitmap对象和Graphics对象
                g.Dispose();
                image.Dispose();
            }
        }
    }
}