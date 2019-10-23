using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IImageHandlerService<T> where T : class
    {
        Task<T> UploadImage(IFormFile file);
    }

    public interface IImageWriter
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
