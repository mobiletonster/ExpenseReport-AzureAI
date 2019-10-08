using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace ExpenseReports.Services
{
    public class StorageService
    {
        // private readonly StorageCredentials cred; // = new StorageCredentials("sort2019", "UawXocvEWZbIWVHUte/NBvPPXhlqhMGNPllkLUgj9ke5nVFqYIq/ZCBbngwG3+mFqy7Dypg6a7TsrUOGEjgGyQ==");
        private  CloudBlobContainer container;  // = new CloudBlobContainer(new Uri("https://sort2019.blob.core.windows.net/receipts"), cred);

        public StorageService(IConfiguration configuration)
        {
            string accountName = configuration.GetSection("storage:accountName")?.Value;
            string keyValue = configuration.GetSection("storage:keyValue")?.Value;
            string containerAddress = configuration.GetSection("storage:containerAddress")?.Value;
            StorageCredentials cred = new StorageCredentials(accountName, keyValue);
            container = new CloudBlobContainer(new Uri(containerAddress), cred);
        }

        public async Task<string> WriteStreamToBlobAsync(string filename, Stream source)
        {
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
            await blob.UploadFromStreamAsync(source);
            return filename;
        }

        public async Task<Stream> ReadBlobToStreamAsync(string filename)
        {
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(filename);
            return await blob.OpenReadAsync();
        }
    }
}
