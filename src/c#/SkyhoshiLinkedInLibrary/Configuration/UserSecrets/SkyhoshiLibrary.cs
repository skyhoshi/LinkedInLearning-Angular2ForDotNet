using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkyhoshiLinkedInLibrary.Processors;

namespace SkyhoshiLinkedInLibrary.Configuration.UserSecrets
{
    using static Bootstrap.ApplicationConfiguration<SkyhoshiLibrary>;
    public class SkyhoshiLibrary
    {
        public UserLocations LocationSettings { get; set; } = new UserLocations();
        public UserAzureSettings AzureSettings { get; set; } = new UserAzureSettings();
        public UserSteamSettings SteamSettings { get; set; } = new UserSteamSettings();
        public static string ExportJson(SkyhoshiLibrary library)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

            return Newtonsoft.Json.JsonConvert.SerializeObject(library);
        }
    }

    public class UserSteamSettings
    {
        public string APIKey { get; set; } = Configuration["SteamSettings:APIKey"];
    }

    public class UserAzureSettings
    {
        public UserAzureStorage AzureStorage { get; set; } = new UserAzureStorage();

    }
    public class UserAzureStorage
    {
        public string Location { get; set; } = Configuration["AzureSettings:AzureStorage:Location"];
        public string AccountName { get; set; } = Configuration["AzureSettings:AzureStorage:AccountName"];
        public string Key { get; set; } = Configuration["AzureSettings:AzureStorage:Key"];

        public List<UserAzureStorageContainer> Containers { get; set; } = GetAzureStorageContainersFromSettings();
        public string PrimaryContainerName { get; set; }= Configuration["AzureSettings:AzureStorage:PrimaryContainer"];
        public string PrimaryBlobName { get; set; }= Configuration["AzureSettings:AzureStorage:PrimaryBlob"];

        public static List<UserAzureStorageContainer> GetAzureStorageContainersFromSettings()
        {
            string strContainers = Configuration["AzureSettings:AzureStorage:Containers"];

            if (!string.IsNullOrWhiteSpace(strContainers))
            {
                List<UserAzureStorageContainer> containers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserAzureStorageContainer>>(strContainers);
            }

            return new List<UserAzureStorageContainer>();
        }
    }

    public class UserAzureStorageContainer
    {
        public string ContainerName { get; set; }

        public List<UserAzureStorageBlob> Blobs { get; set; } = new List<UserAzureStorageBlob>();

        public static List<UserAzureStorageBlob> GetAzureBlobsFromSettings()
        {
            return new List<UserAzureStorageBlob>();
        }
    }
    public class UserAzureStorageBlob
    {
        public string BlobName { get; set; }
    }
    public class UserData
    {
        public UserLocations Locations { get; set; } = new UserLocations();

    }

    public class UserLocations
    {
        public string LocalLocation { get; set; } = Configuration["LocationSettings:LocalLocation"];
        public string APILocation { get; set; } = Configuration["LocationSettings:APILocation"];

        [JsonConverter(typeof(StringEnumConverter))]
        public StorageLocationTypes StorageLocationType { get; set; }
    }
}
