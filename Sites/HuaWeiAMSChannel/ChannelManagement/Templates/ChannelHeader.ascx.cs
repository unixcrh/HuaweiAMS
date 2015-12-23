using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChannelManagement.Templates
{
    public partial class ChannelHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false && this.Page.IsCallback == false)
            {
                ControllerHelper.ExecuteMethodByRequest(this);
            }
        }

        [ControllerMethod]
        protected void InitByChannelID(string channelID, string id)
        {
            if (channelID.IsNotEmpty())
                this.Channel = AMSChannelSqlAdapter.Instance.LoadByID(channelID);

            if (id.IsNotEmpty())
                this.Event = AMSEventSqlAdapter.Instance.LoadByID(id);
        }

        public string CurrentName
        {
            get
            {
                return this.ViewState.GetViewStateValue("CurrentName", string.Empty);
            }
            set
            {
                this.ViewState["CurrentName"] = value;
            }
        }

        public AMSChannel Channel
        {
            get
            {
                return (AMSChannel)this.ViewState["Channel"];
            }
            set
            {
                this.ViewState["Channel"] = value;
            }
        }

        public AMSEvent Event
        {
            get
            {
                return (AMSEvent)this.ViewState["Event"];
            }
            set
            {
                this.ViewState["Event"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RenderBreadcrumbTrail();

            base.OnPreRender(e);
        }

        private void RenderBreadcrumbTrail()
        {
            this.header.Controls.Clear();

            if (this.Channel != null)
            {
                AddItem(this.header, this.Channel.Name, this.ResolveUrl("~/list/AllChannels.aspx"));

                if (this.Event != null)
                    AddItem(this.header, "事件列表", this.ResolveUrl(string.Format("~/list/EventsInChannel.aspx?channelID={0}", this.Channel.ID)));
            }


            if (this.CurrentName.IsNotEmpty())
                AddItem(this.header, this.CurrentName, string.Empty);
        }

        private static void AddItem(Control parent, string name, string link)
        {
            HtmlGenericControl item = new HtmlGenericControl("li");

            HtmlAnchor anchor = new HtmlAnchor();
            anchor.HRef = link;
            anchor.InnerText = name;

            item.Controls.Add(anchor);
            parent.Controls.Add(item);
        }
    }
}