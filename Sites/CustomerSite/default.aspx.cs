using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eastAsiaSample.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://cdn-zhshenstudy.streaming.mediaservices.windows.net/e2fd9dc9-c247-48d8-bbec-0ddd1ec06fdc/c7392fac-3ced-47d4-87ac-c4cf40a0d984.ism/manifest");
            northEuropeSample.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://cdn-endpoint-zhshenneurope.streaming.mediaservices.windows.net/194de0a1-88c0-44e5-b47c-8940d8121b09/Windows10CommercialDemo.ism/manifest");

            eastAsianSalesTraining.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://cdn-zhshenstudy.streaming.mediaservices.windows.net/cd640c21-01d5-4461-a74f-029d3d05c86d/120c73a9-a9de-4417-a11a-e0b73d14609a.ism/manifest");
            eastAsianSalesTrainingWithoutCDN.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://zhshenstudy.streaming.mediaservices.windows.net/cd640c21-01d5-4461-a74f-029d3d05c86d/120c73a9-a9de-4417-a11a-e0b73d14609a.ism/manifest");

            europeSalesTraining.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://cdn-endpoint-zhshenneurope.streaming.mediaservices.windows.net/3b23c7c2-37d1-49d9-b74c-0902ef7ddaf0/ec0fc7f6-3d7e-49e3-b9aa-5625c8957051.ism/manifest");
            europeSalesTrainingWithoutCDN.HRef = string.Format("../samples/simpleVideo.aspx?url={0}",
                "http://endpoint-zhshenneurope.streaming.mediaservices.windows.net/3b23c7c2-37d1-49d9-b74c-0902ef7ddaf0/ec0fc7f6-3d7e-49e3-b9aa-5625c8957051.ism/manifest");
        }
    }
}