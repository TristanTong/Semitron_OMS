using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI.Admin
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] msgs = { "左键点击弹窗标题，按Esc键可关闭窗体。", "左键点击表格列头，可表格数据进行排序。", "左侧的功能导航有个收缩和展开的箭头哦，试试吧。", "如果表格中的数据有遮盖，可以试着往右边拖动边界线。", "列头可以互换位置，试试鼠标按住列头不放进行拖动。", "列头右边的下拉箭头能够让你去掉不想看到的数据哦。", "亲，使用IE 9.0，您的体验会更佳。", "当遇到了Bug，请不要着急，请联系管理员。", "使用【安全退出】，安全有保障。" };
            Random r = new Random();
            lblMsgShow.Text = msgs[r.Next(0, msgs.Length)];
            if (!this.IsPostBack)
            {
                HttpCookie Cookie = GetCookie("UserInfo");
                if (Cookie != null)
                {
                    this.txtUserName.Text = Cookie.Values["uName"];
                    this.txtPassWord.Attributes.Add("value", Cookie.Values["uPassword"]);
                }

                if (Request.Params["UserName"] != null && Request.Params["UserName"].ToString() != string.Empty)
                {
                    this.txtUserName.Text = Request.Params["UserName"].ToString();
                }
                if (Request.Params["Password"] != null && Request.Params["Password"].ToString() != string.Empty)
                {
                    txtPassWord.Attributes.Add("value", DESEncrypt.Decrypt(Request.Params["Password"].ToString()));
                }
            }
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {

        }


        private HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
                return request.Cookies[cookieName];
            return null;
        }


    }
}
