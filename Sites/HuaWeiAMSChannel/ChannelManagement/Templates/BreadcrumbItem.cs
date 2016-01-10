using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data.DataObjects;

namespace ChannelManagement.Templates
{
    [Serializable]
    public class BreadcrumbItem
    {
        public string Name
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }
    }

    [Serializable]
    public class BreadcrumbItemCollection : EditableDataObjectCollectionBase<BreadcrumbItem>
    {
    }
}