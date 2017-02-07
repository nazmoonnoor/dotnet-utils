using System;
using System.IO;
using System.Web.Hosting;

namespace Web.Framework
{
    /// <summary>
    /// File helper utilities
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Save files
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        public static void SaveFile(byte[] content, string path)
        {
            string filePath = GetFileFullPath(path);
            if (!Directory.Exists(path: Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(path: Path.GetDirectoryName(filePath));
            }

            //Save file
            using (FileStream str = File.Create(filePath))
            {
                str.Write(content, 0, content.Length);
            }
        }

        /// <summary>
        /// Get full path of file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string path)
        {
            string relName = path.StartsWith("~") ? path : path.StartsWith("/") ? string.Concat("~", path) : path;

            string filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

            return filePath;
        }

        public static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}