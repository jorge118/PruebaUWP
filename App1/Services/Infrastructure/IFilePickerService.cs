using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services.Infrastructure
{
    public class ImagePickerResult
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public object ImageSource { get; set; }
    }

    public interface IFilePickerService
    {
        Task<ImagePickerResult> OpenImagePickerAsync();
    }
}
