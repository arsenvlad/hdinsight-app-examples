#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // This example assumes that the binaries are stored in a container "binaries" which is part of 
    // the same storage account as used by the Azure Function.
    // If you are using a different storage account you will need to create another AppSetting and 
    // store the connection string (e.g. DefaultEndpointsProtocol=https;AccountName=STORAGEACCOUNT_NAME;AccountKey=STORAGE_ACCOUNTKEY)
    // for that storage account there.
    string storageAccountConnectionString = ConfigurationManager.AppSettings["AzureWebJobsStorage"];
    string container = "binaries";

    // Get query string parameters
    string fileName = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "fileName", true) == 0)
        .Value;

    string licenseKey = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "licenseKey", true) == 0)
        .Value;

    // Log
    log.Info("Download Request fileName=" + fileName + " licenseKey=" + licenseKey);

    if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(licenseKey)) {
        log.Info("fileName and licenseKey cannot be null");
        return req.CreateResponse(HttpStatusCode.Forbidden, "fileName and licenseKey cannot be null");
    }

    // Check licenseKey value again a valid set
    // NOTE: This is only an example. 
    // You will want to store vaid license key strings in some data store (Azure SQL DB) 
    // that you can update easily when adding more keys.
    var validLicenseKeys = new string[] {
        "084cdc29-09d9-48a3-8e5d-e182deb7fcd7",
        "71762bce-09df-42e9-8420-8fde0b654337",
        "64c0a2ce-2d98-473e-a30e-78386bc067e3"};
    
    if (!validLicenseKeys.Contains(licenseKey)) {
        // If license key is invalid return 403 error
        log.Info("Invalid licenseKey=" + licenseKey);
        return req.CreateResponse(HttpStatusCode.Forbidden, "Invalid licenseKey=" + licenseKey);
    }
    else {
        // If license key is valid, get 60 minute SAS token for the blob and return the full URL
        log.Info("Valid licenseKey=" + licenseKey); 
        var storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var containerReference = blobClient.GetContainerReference(container);
        string uri = GetBlobSasUri(containerReference, fileName, 60);

        // Redirect to the blob using its SAS token
        var response = req.CreateResponse(HttpStatusCode.Moved);
        response.Headers.Location = new Uri(uri);
        return response;
    }
}

public static string GetBlobSasUri(CloudBlobContainer containerReference, string blobName, int minutes) {
    string uri;

    CloudBlockBlob blob = containerReference.GetBlockBlobReference(blobName);

    var adHocSas = new SharedAccessBlobPolicy() {
        // Set start time to five minutes before now to avoid clock skew
        SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(60),
        Permissions = SharedAccessBlobPermissions.Read
    };

    uri = blob.Uri + blob.GetSharedAccessSignature(adHocSas);

    return uri;
}