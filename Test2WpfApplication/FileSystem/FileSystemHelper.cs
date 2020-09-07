using Test2WpfApplication.FileSystem.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using Test2WpfApplication.Utilities;

namespace Test2WpfApplication.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileSystemHelper
    {
        /// <summary>
        /// Returns local system drives
        /// </summary>
        /// <returns></returns>
        public static List<DataItem> GetLogicalDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives().Where(drive => drive.DriveType == DriveType.Fixed).ToArray();
            return drives.Select(drive => new DataItem { FullPath = drive.VolumeLabel + " (" + drive.Name + ")", Type = DataType.Drive, }).ToList();
        }

        /// <summary>
        /// Finds all Folders and Files for a given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileOrFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            if (File.Exists(path))
                return Path.GetFileName(path);
             
            return path.Split(Path.DirectorySeparatorChar).Last();
        }

        /// <summary>
        /// Gets full content for a folder
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<DataItem> GetFolderContent(string fullPath)
        {
            var items = new List<DataItem>();

            GetItems(ref items, fullPath, DataType.FolderClosed);
            GetItems(ref items, fullPath, DataType.File);

            return items;
        }

        /// <summary>
        /// Populates items List with folders and files
        /// </summary>
        /// <param name="items"></param>
        /// <param name="fullPath"></param>
        /// <param name="datatype"></param>
        private static void GetItems(ref List<DataItem> items, string fullPath, DataType datatype)
        {
            try
            {
                string[] itemsToAdd = null;
                switch(datatype)
                {
                    case DataType.FolderClosed:
                        itemsToAdd = Directory.GetDirectories(fullPath);
                        break;
                    case DataType.File:
                        itemsToAdd = Directory.GetFiles(fullPath);
                        break;
                }

                if (itemsToAdd != null && itemsToAdd.Length > 0)
                {
                    items.AddRange(itemsToAdd.Select(item => new DataItem
                    {
                        FullPath = item,
                        Type = datatype
                    }));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.HandleException(ex);
            }
        }
    }
}
