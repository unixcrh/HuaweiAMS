using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Core;
using System.Text;

namespace MCS.Library.Cloud.AMSHelper.Test.AMS
{
    [TestClass]
    public class StorageTest
    {
        [TestMethod]
        public void WriteBinaryData()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageSettings.GetConfig().ConnectionStrings.CheckAndGet("amsImages").ConnectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("amsimages");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob); 

            string blobID = UuidHelper.NewUuidString();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobID);

            string originalData = "Hello world!";
            byte[] data = Encoding.UTF8.GetBytes(originalData);
            blockBlob.UploadFromByteArray(data, 0, data.Length);

            string dataLoaded = blockBlob.DownloadText(Encoding.UTF8);

            Assert.AreEqual(originalData, dataLoaded);
        }
    }
}
