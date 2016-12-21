using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobAPI.Providers {
    public class ResourceProvider
    {
        internal static class BlobHelper {
            public static CloudBlobContainer GetWebApiContainer() {
                // Retrieve storage account from connection-string
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=censtorage;AccountKey=qahNbJ9Yx54oVTj06VCdDLgt3NnlldSc5lDNSo6yCV6k4HvtKJrVa4UfNiHxkR0Au1ixafLnrEf/mnDiyoLIpg==");

                // Create the blob client 
                var blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container 
                // Container name must use lower case
                var container = blobClient.GetContainerReference("mycontainer1");

                // Create the container if it doesn't already exist
                container.CreateIfNotExists();

                // Enable public access to blob
                var permissions = container.GetPermissions();
                if (permissions.PublicAccess == BlobContainerPublicAccessType.Off) {
                    permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                    container.SetPermissions(permissions);
                }

                return container;
            }
        }
        
    }
}