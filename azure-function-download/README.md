# Azure Function Redirect to Binary Blob Download

The function has a public URL that can be invoked from script to download binaries if the license key string is valid

```
wget -O $fileName -q "https://hdiappdownload1.azurewebsites.net/api/download?licenseKey=$licenseKey&fileName=$fileName"
```

## Create Azure Function
[![Create Azure Function](images/azf-create.png)]

## Set Azure Function Settings
[![Set Azure Function Settings](images/azf-settings.png)]
