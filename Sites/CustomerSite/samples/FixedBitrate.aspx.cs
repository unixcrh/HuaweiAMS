using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.samples
{
    public partial class FixedBitrate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                if (this.Request.QueryString["url"] != null)
                    this.videoUrl.Text = this.Request.QueryString["url"];
                else
                    this.videoUrl.Text = "http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/94706d4f-ea13-4eb4-a938-a58c9b9a0897/XBox Video.ism/manifest";

            }
        }

        protected void changeVideo_Click(object sender, EventArgs e)
        {
        }
    }
}