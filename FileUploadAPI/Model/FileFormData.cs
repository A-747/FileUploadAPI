using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Model
{
    public class FileFormData
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IFormFile File { get; set; }
        public byte[] FileContent { get; set; }  
    }
}
