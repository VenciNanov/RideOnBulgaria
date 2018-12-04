using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class UploadImageViewModel : IFormFile
    {
        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; }
        public long Length { get; }
        public string Name { get; }
        public string FileName { get; }
    }
}
