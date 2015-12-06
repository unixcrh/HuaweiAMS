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
                    this.videoUrl.Text = "http://cdn-zhshenstudy.streaming.mediaservices.windows.net/e194ceb9-d744-47c5-a7e7-5fbe93dd6942/Robotica_720.ism/manifest";

            }
        }

        protected void changeVideo_Click(object sender, EventArgs e)
        {
        }
    }
}