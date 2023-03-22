using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Utils.Interface
{
    public interface IUploadFileService
    {
        public bool CheckFileSize(IFormFile file, int limit);
        public bool CheckFileExtension(IFormFile file, string[] extensions);
        public bool CheckListFileSize(List<IFormFile> file, int limit);
        public bool CheckListFileExtension(List<IFormFile> file, string[] extensions);
        public string Upload(IFormFile file);
    }
}
