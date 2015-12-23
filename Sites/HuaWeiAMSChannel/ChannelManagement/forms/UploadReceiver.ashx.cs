using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Core;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Res = MCS.Web.Responsive.Library;

namespace ChannelManagement.forms
{
    /// <summary>
    /// Summary description for UploadReceiver
    /// </summary>
    public class UploadReceiver : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            try
            {
                string storageKey = "amsImages";

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageSettings.GetConfig().ConnectionStrings.CheckAndGet(storageKey).ConnectionString);

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(storageKey.ToLower());
                container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

                string blobID = string.Format("{0}-{1}",
                    //Res.Request.GetRequestFormString("fileID", string.Empty),
                    UuidHelper.NewUuidString(),
                    Res.Request.GetRequestFormString("fileType", string.Empty));

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobID);

                blockBlob.UploadFromStream(HttpContext.Current.Request.Files[0].InputStream);

                string script = string.Format("<script>parent.document.getElementById(\"responseUrl\").value = \"{0}\"; parent.document.getElementById(\"responseButton\").click();</script>",
                    blockBlob.StorageUri.PrimaryUri);

                context.Response.Write(script);
            }
            catch (System.Exception ex)
            {
                string script = string.Format("<script>parent.document.getElementById(\"responseUrl\").value = \"{0}\"; parent.document.getElementById(\"responseButton\").click();</script>",
                    "Error:" + ex.Message);

                context.Response.Write(script);
            }
            finally
            {
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}