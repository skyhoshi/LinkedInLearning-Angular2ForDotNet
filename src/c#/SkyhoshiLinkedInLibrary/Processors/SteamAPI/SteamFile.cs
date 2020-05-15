using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace SkyhoshiLinkedInLibrary.Processors.SteamAPI
{
    public class SteamFile : FileInfoBase
    {
        public SteamFile(string fullPath)
        {
            ContentStreamType = StreamType.FileStream;
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                this.Location = fullPath;
            }
        }

        public SteamFile(MemoryStream memoryStream)
        {
            string fileName = Path.GetRandomFileName();
            string fileExtension = ".json";
            Location = $@"memory:\\{fileName}{fileExtension}";
            ContentStreamType = StreamType.MemoryStream;
            ContentStream = memoryStream;

        }

        /// <summary>
        /// the Full Rooted Path to the steam file.
        /// </summary>
        public string Location { get; set; }
        public string FileName
        {
            get
            {
                return Path.GetFileName(Location);
            }
        }

        [FileExtensions()]
        public string FileExtension
        {
            get
            {
                return Path.GetExtension(Location);
            }
        }

        #region Overridden Properties
        public override string Name { get { return Path.GetFileName(Location); } }

        public override string FullName
        {
            get
            {
                return Location;
            }
        }

        public override DirectoryInfoBase ParentDirectory
        {
            get
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Location);
                DirectoryInfoWrapper diw = new DirectoryInfoWrapper(directoryInfo);
                return diw.GetDirectory(Location);
            }
        }
        #endregion
        public SteamApiCallType FileSteamApiCallType { get; set; }

        public StreamType ContentStreamType { get; set; }
        public Stream ContentStream { get; set; }

        public void Open()
        {
            if (this.ContentStream.Length > 0)
            {
                //Something is already here, should we provide an override? 
            }
            else
            {
                //Load FileStream with content from file.
                FileStream file = System.IO.File.Open($@"{FullName}", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
        }

        public void Save()
        {
            if (this.ContentStream.Length > 0)
            {

            }
            else
            {
                //Nothing to save. maybe alert someone?
            }
        }

        public static void SaveSteamFile(SteamFile file)
        {
            file.Save();
        }

        public static void OpenSteamFile(SteamFile file)
        {
            file.Open();
        }

        public string GetStringFromContentStream()
        {
            MemoryStream outputMemoryStream = null;
            switch (ContentStreamType)
            {
                case StreamType.FileStream:
                    FileStream outputFileStream = (FileStream)this.ContentStream;
                    outputMemoryStream = new MemoryStream();
                    outputFileStream.CopyTo(outputMemoryStream);
                    break;
                case StreamType.MemoryStream:
                    outputMemoryStream = (MemoryStream)this.ContentStream;
                    break;
                default:
                    throw new FormatException("the content stream type is not known.");
            }

            if (outputMemoryStream != null)
            {
                byte[] arrayMemory = outputMemoryStream.ToArray();
                return Encoding.Default.GetString(arrayMemory);
            }
            else
            {
                return string.Empty;
            }


        }
    }

    public enum StreamType
    {
        FileStream,
        MemoryStream
    }
}
