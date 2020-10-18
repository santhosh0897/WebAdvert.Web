using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Service
{
    public interface IFileUploader
    {

        Task<bool> UploadFileAsync(string filename, Stream storagestream);
    }
}
