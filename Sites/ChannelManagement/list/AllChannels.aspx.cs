using MCS.Library.Cloud.AMSHelper.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.list
{
    public partial class AllChannels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.dataGrid.DataSource = LiveChannelManager.GetAllChannels(true);
            this.dataGrid.DataBind();
        }
    }
}