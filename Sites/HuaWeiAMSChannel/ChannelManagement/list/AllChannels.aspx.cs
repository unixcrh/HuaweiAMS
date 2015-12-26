using MCS.Library.Cloud.AMSHelper.Mechanism;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.Resources;
using Microsoft.IdentityModel.Claims;
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
            timeOffset.InnerText = TimeZoneContext.Current.CurrentTimeZone.BaseUtcOffset.ToString();

            WebUtility.RequiredScript(typeof(ClientMsgResources));

            this.dataGrid.DataSource = LiveChannelManager.GetAllChannels(false);
            this.dataGrid.DataBind();
        }
    }
}