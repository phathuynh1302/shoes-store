using Microsoft.AspNetCore.Http;
using static PRN211_ShoesStore.Utils.UploadFileService;
using System.IO;
using System;
using PRN211_ShoesStore.Utils.Interface;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Utils
{
    public class UploadFileService : IUploadFileService
    {
        readonly string pathUrl = "/public/image/";
        readonly string folderUrl = "/wwwroot/public/image/";
        public static string[] imageExtension = { "png", "jpg", "jpeg" };
        public bool CheckFileExtension(IFormFile file, string[] extensions)
        {
            bool result = false;
            string fileExtension = file.FileName.ToLower().Split(".")[file.FileName.ToLower().Split(".").Length - 1];
            foreach (string extension in extensions)
            {
                if (extension == fileExtension)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool CheckListFileExtension(List<IFormFile> files, string[] extensions)
        {
            foreach (var item in files)
            {
                if (!CheckFileExtension(item, extensions))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckFileSize(IFormFile file, int limit)
        {

            // Unit: MB
            return file.Length < limit * 1024 * 1024;
        }

        public bool CheckListFileSize(List<IFormFile> files, int limit)
        {
            foreach(var item in files)
            {
                if(!CheckFileSize(item, limit))
                {
                    return false;
                }
            }
            return true;
        }

        public string Upload(IFormFile file)
        {
            string formatFolderUrl = "." + folderUrl;
            string fileExtension = file.FileName.ToLower().Split(".")[file.FileName.ToLower().Split(".").Length - 1];
            string fortmatFileName = System.Guid.NewGuid().ToString() + "." + fileExtension;

            try
            {
                if (!Directory.Exists(formatFolderUrl))
                {
                    Directory.CreateDirectory(formatFolderUrl);
                }

                using (FileStream fileStream = System.IO.File.Create(formatFolderUrl + fortmatFileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                return pathUrl + fortmatFileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
