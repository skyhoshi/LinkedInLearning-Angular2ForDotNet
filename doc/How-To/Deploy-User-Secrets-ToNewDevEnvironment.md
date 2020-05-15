# User-Secrets in New Dev Environment
Navigate to the proper source folder for the {project}

Run the command below.

## Knownable References: 
Convert these values on your own developer PC.

| Mustach Tag |Human Readable|Description/purpose|
|:----------|---|--:|
|{repo-location}|Git Repository Storage Location|Represents the Full Path to the Repository storage location on your development PC|
|{project}|File Name of the project|The File name of the project your working with|
|{user-secrets-guid}|ID of User-Secrets|Guid of the User-Secrets that can be found within the {project} csproj file. |
|{username}|Username of Logged in User|The Username of the account the user is logged into|

```powershell
# Set-Location = to the location of your repo and the {project}
CD ~\{repo-location}\
# Initialize your local User secrets with the following ID : this id is shared and should not be changed for any reason
dotnet user-secrets init --id {user-secrets-guid}
```

Response should look similar to below : Replace {username} with your username OR update the Location to your repo storage location.

```powershell
# This is output from the dotnet command run above. use it to validate your execution
Set UserSecretsId to '{user-secrets-guid}' for MSBuild project 'C:\Users\{username}\source\repos\{repo-location}\{project}.csproj'.
```

Once Completed
1. Open the Solution in visual studio
2. right click on "{project}" project and choose "Manage Secrets"
3. Copy the contents of the file (given below) into the file and save the file, close the editor

You should then be able to run the application. 

```json
{
    "LocationSettings": {
      "LocalLocation": "C:\\TempOrPerminate\\StorageLocation\\",
      "APILocation": "https://api.steampowered.com/ISteamApps/GetAppList/v2/?key=",
      "StorageLocationType": "Local"
    },
    "AzureSettings": {
      "AzureStorage": {
        "Location": "",
        "AccountName": "",
        "Key": "",
        "PrimaryContainer": "",
        "PrimaryBlob": "",
        "Containers": [
          {
            "ContainerName": "",
            "Blobs": [ 
                { 
                "BlobName": "" 
                } 
            ]
          }
        ]
      }
    },
    "SteamSettings": { 
        "APIKey": "" 
    }
  }
```