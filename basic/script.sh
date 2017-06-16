#!/bin/bash

# Download binary file stored in a predefined private Azure Blob Storage container.
# Access to the file is provided by an Azure Function that checks "licenseKey" string against a known set of strings
# and redirects to the 60-minute SAS URI of the blob to download.
# If file cannot be downloaded an error message is output into /tmp/INVALID_LICENSE_KEY.txt file and the process terminates,
# but returns success code so that the HDInsight Script Action does not show failure.
downloadUsingLicenseKey() {
        # Exit with success code so that script action in HDInsight does not show a failure
        licenseKey=$1
        fileName=$2
        expectedSize=$3

        # Call Azure Function or another web URL that checks the licenseKey string and redirects to the download
        wget -O $fileName -q "https://hdiappdownload1.azurewebsites.net/api/download?licenseKey=$licenseKey&fileName=$fileName"
        
        # Check whether file was downloaded successfully
        if [ $? -ne 0 ] || [ ! -f $fileName ]; then
                echo "Download of <$fileName> using license key <$licenseKey> failed." | tee /tmp/INVALID_LICENSE_KEY.txt
                exit 0
        fi

        # Check the size of the downloaded file to make sure it is close to what's expected
        fileSize=$(stat -c%s "$fileName")
        if [ $fileSize -lt $expectedSize ]; then
                echo "Size of <$fileName> is <$fileSize> which is smaller than expected <$expectedSize> bytes." | tee /tmp/INVALID_LICENSE_KEY.txt
                exit 0
        fi

        echo "Downloaded <$fileName> with size <$fileSize> bytes."
}

echo "Basic script starting"

# License key string is passed as one of the script parameters from the ARM template
NODETYPE=$1
CLUSTERNAME=$2
LICENSEKEY=$3

# Download all required binary files
downloadUsingLicenseKey "$LICENSEKEY" mongo.zip 4000000
downloadUsingLicenseKey "$LICENSEKEY" mysql.zip 1000000
downloadUsingLicenseKey "$LICENSEKEY" postgres.zip 2500000

echo "Basic script finished"