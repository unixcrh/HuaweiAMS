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

        public BreadcrumbItemCollection Items
        {
            get
            {
                BreadcrumbItemCollection result = (BreadcrumbItemCollection)this.ViewState["Items"];

                if (result == null)
                {
                    result = new BreadcrumbItemCollection();
                    this.ViewState["Items"] = result;
                }

                return result;
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

            BreadcrumbItemCollection items = this.Items;

            if (items.Count == 0)
                items = BuildDefaultItems();

            items.ForEach(item => AddItem(this.header, item.Name, this.ResolveUrl(item.Url)));
        }

        private BreadcrumbItemCollection BuildDefaultItems()
        {
            BreadcrumbItemCollection result = new BreadcrumbItemCollection();

            if (this.Channel != null)
            {
                result.Add(new BreadcrumbItem() { Name = this.Channel.Name, Url = "~/list/AllChannels.aspx" });

                if (this.Event != null)
                    result.Add(new BreadcrumbItem() { Name = "事件列表", Url = string.Format("~/list/EventsInChannel.aspx?channelID={0}", this.Channel.ID) });
            }

            if (this.CurrentName.IsNotEmpty())
                result.Add(new BreadcrumbItem() { Name = this.CurrentName, Url = string.Empty });

            return result;
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