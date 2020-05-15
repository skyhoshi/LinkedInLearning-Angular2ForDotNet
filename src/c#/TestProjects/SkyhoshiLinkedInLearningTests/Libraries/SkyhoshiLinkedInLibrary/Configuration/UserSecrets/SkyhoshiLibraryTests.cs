using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyhoshiLinkedInLibrary.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Text;
using SkyhoshiLinkedInLibrary.Processors;

namespace SkyhoshiLinkedInLibrary.Configuration.UserSecrets.Tests
{
    [TestClass()]
    public class SkyhoshiLibraryTests
    {
        private SkyhoshiLibrary SkyhoshiLibraryConfiguration { get; set; }
        UserAzureStorageBlob blob => new UserAzureStorageBlob() { BlobName = "settingBlobName" };
        List<UserAzureStorageBlob> blobs => new List<UserAzureStorageBlob>(){ blob};
        UserAzureStorageContainer container => new UserAzureStorageContainer() { Blobs = blobs, ContainerName = "settingContainerName" };

        private List<string> KnownSettingsProperties { get; set; }

        [TestInitialize()]
        public void Setup()
        {
            SkyhoshiLibraryConfiguration.AzureSettings.AzureStorage.AccountName = "settingAccountName";
            SkyhoshiLibraryConfiguration.AzureSettings.AzureStorage.Key = "settingKey";
            SkyhoshiLibraryConfiguration.AzureSettings.AzureStorage.Location = "settingLocation";
            SkyhoshiLibraryConfiguration.AzureSettings.AzureStorage.Containers = new List<UserAzureStorageContainer>() { container };
            SkyhoshiLibraryConfiguration.SteamSettings.APIKey = "settingsAPIKey";
            SkyhoshiLibraryConfiguration.LocationSettings.APILocation = "https://api.steampowered.com/ISteamApps/GetAppList/v2/?key=";
            SkyhoshiLibraryConfiguration.LocationSettings.LocalLocation = $@"{{userprofile}}\.Skyhoshi Games\";
            SkyhoshiLibraryConfiguration.LocationSettings.StorageLocationType = StorageLocationTypes.Local;
        }



        [TestMethod()]
        public void ConfigurationListRetrievalTest()
        {

        }

        /// <summary>
        /// This test will generate the Most Current Settings file structure.
        /// </summary>
        [TestMethod()]
        public void ExportJsonTest()
        {
            string strJson = SkyhoshiLibrary.ExportJson(SkyhoshiLibraryConfiguration);
            System.Diagnostics.Debug.WriteLine(strJson);
        }

        public bool ValidConfigurationPOCO()
        {
            return false;
        }
    }
}