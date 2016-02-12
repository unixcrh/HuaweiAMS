using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.samples
{
    public partial class simpleVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                if (this.Request.QueryString["url"] != null)
                    this.videoUrl.Text = this.Request.QueryString["url"];
                else
                    this.videoUrl.Text = "http://huaweiamschn.streaming.mediaservices.chinacloudapi.cn/b3c814b9-dfef-4897-9478-7d47213ff815/30195156-3eaa-414b-b82f-ce4b3ccb5b14.ism/manifest";

            }
        }

        protected void changeVideo_Click(object sender, EventArgs e)
        {
        }
    }
}