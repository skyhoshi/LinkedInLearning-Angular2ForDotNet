using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Blob.Protocol;
using SkyhoshiLinkedInLibrary.Configuration.UserSecrets;
using SkyhoshiLinkedInLibrary.Data.Steam.Apps;
using SkyhoshiLinkedInLibrary.Processors.Interfaces;

namespace SkyhoshiLinkedInLibrary.Processors
{

    public class SteamJsonProcessor : IProcessor<SteamAppsList>
    {
        private SkyhoshiLinkedInLibrary.Configuration.UserSecrets.SkyhoshiLibrary LibraryConfiguration = new SkyhoshiLinkedInLibrary.Configuration.UserSecrets.SkyhoshiLibrary();
        public string strJsonData { get; set; }

        private string storage { get; set; } = @"\Steam\API\";
        public void RetrieveJsonData(StorageLocationTypes location = StorageLocationTypes.Azure)
        {

            string userProfileFolder = "";
            string profileLocation = "";
            userProfileFolder = location == StorageLocationTypes.Local ? LibraryConfiguration.LocationSettings.LocalLocation : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            profileLocation = $@"{userProfileFolder}{storage}";
            if (!Directory.Exists(profileLocation))
            {
                Directory.CreateDirectory(profileLocation);
            }
            FileStream file = System.IO.File.Open($@"{profileLocation}games.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            string containerName;
            string keyValue;
            string accountName;
            string blobName;
            string storageLocationUri;
            switch (location)
            {
                case StorageLocationTypes.Api:
                    string key = LibraryConfiguration.SteamSettings.APIKey;
                    //https://api.steampowered.com/ISteamApps/GetAppList/v2/?key=
                    Uri steamAPIUri = new Uri($"https://api.steampowered.com/ISteamApps/GetAppList/v2/?key={key}");
                    WebClient webClient = new System.Net.WebClient();
                    Stream dl = webClient.OpenRead(steamAPIUri);
                    dl?.CopyTo(file);
                    if (file.Length != 0)
                    {
                        file.Flush(true);
                    }
                    TextReader apiTextReader = new StreamReader(file);
                    strJsonData = apiTextReader.ReadToEnd();
                    break;
                case StorageLocationTypes.Azure:
                    storageLocationUri = LibraryConfiguration.AzureSettings.AzureStorage.Location;
                    Uri primaryUri = new Uri(storageLocationUri);
                    StorageUri storageUri = new StorageUri(primaryUri);
                    accountName = LibraryConfiguration.AzureSettings.AzureStorage.AccountName;
                    keyValue = LibraryConfiguration.AzureSettings.AzureStorage.Key;
                    StorageCredentials creds = new StorageCredentials(accountName, keyValue);
                    CloudBlobClient cloudClient = new CloudBlobClient(storageUri, creds);
                    UserAzureStorageContainer containerConfig = LibraryConfiguration.AzureSettings.AzureStorage.Containers.First();
                    //containerName = containerConfig.ContainerName;
                    containerName = LibraryConfiguration.AzureSettings.AzureStorage.PrimaryContainerName;
                    CloudBlobContainer container = cloudClient.GetContainerReference(containerName);
                    UserAzureStorageBlob storageBlobConfig = containerConfig.Blobs.First();
                    blobName = LibraryConfiguration.AzureSettings.AzureStorage.PrimaryBlobName;
                    CloudBlob blob = container.GetBlobReference(blobName);
                    Stream blobStream = new MemoryStream();
                    blob.DownloadToStream(blobStream);
                    blobStream.Position = 0;
                    TextReader azureTextReader = new StreamReader(blobStream);
                    strJsonData = azureTextReader.ReadToEnd();
                    blobStream.Position = 0;
                    blobStream.CopyTo(file);
                    file.Flush(true);
                    break;
                case StorageLocationTypes.Local:
                    TextReader localTextReader = new StreamReader(file);
                    strJsonData = localTextReader.ReadToEnd();
                    break;
                default:
                    TextReader defaultReader = new StreamReader(file);
                    strJsonData = defaultReader.ReadToEnd();
                    break;
            }
            file.Dispose();
        }

        //private void StoreFileStream(FileStream file)
        //{

        //}

        private bool IsAPIAvailable()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return false;
            System.Net.Http.HttpClient client = new HttpClient();
            try
            {
                string SteamKey = LibraryConfiguration.SteamSettings.APIKey;
                string result = client.GetStringAsync($"https://api.steampowered.com/ISteamWebAPIUtil/GetServerInfo/v1/?key={SteamKey}").Result;
                return !string.IsNullOrWhiteSpace(result);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SteamAppsList GameList()
        {
            RetrieveJsonData(StorageLocationTypes.Azure);
            return !string.IsNullOrWhiteSpace(strJsonData) ? Newtonsoft.Json.JsonConvert.DeserializeObject<SteamAppsList>(strJsonData) : null;
        }

        public List<SteamAppsList> List()
        {
            return new List<SteamAppsList>() { GameList() };
        }

        public List<SteamAppsList> Parse()
        {
            return new List<SteamAppsList>() { GameList() };
        }
    }
}