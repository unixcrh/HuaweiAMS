﻿using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Library.Script;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.forms
{
    public partial class EventPlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ControllerHelper.ExecuteMethodByRequest(this);
        }

        [ControllerMethod]
        protected void InitByEventID(string id)
        {
            if (id.IsNotEmpty())
            {
                AMSEvent eventData = AMSEventSqlAdapter.Instance.LoadByID(id);

                if (eventData != null)
                {
                    if (eventData.PosterUrl.IsNullOrEmpty())
                        eventData.PosterUrl = UriHelper.MakeAbsolute(new Uri(this.ResolveUrl("~/images/amsPoster1.png"), UriKind.RelativeOrAbsolute), this.Request.Url).ToString();

                    this.pageEventData.Value = DataHelper.GetSingleEventJson(eventData);
                }
            }
        }
    }
}