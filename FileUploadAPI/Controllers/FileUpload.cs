using FileUploadAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Controllers
{
    public class FileUpload : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult FileUploadAction([FromForm] FileFormData formData)
        {
            HttpRequest httpRequest = HttpContext.Request;
            IFormFile uploadFile = httpRequest.Form.Files["File"];  // File= property of FileFormData Class
            var filestream = uploadFile.OpenReadStream();
            BinaryReader br = new BinaryReader(filestream);
            byte[] bytesContentForFile = br.ReadBytes((Int32)filestream.Length);
            FileFormData fileObj = new FileFormData();
            fileObj.CustomerId = formData.CustomerId;
            fileObj.CustomerName = formData.CustomerName;
            fileObj.File = formData.File;
            fileObj.FileContent = bytesContentForFile;

            //writing the file into specified folder
            string pathString = @"E:\Repo\Testing_Purpose";

            // To create a string that specifies the path to a subfolder under your
            // top-level folder, add a name for the subfolder (TestFileSpace) to folderName.
            // You can write out the path name directly instead of using the Combine
            // method. Combine just makes the process easier.

            //string pathString = System.IO.Path.Combine(folderName, "TestFileSpace");

            System.IO.Directory.CreateDirectory(pathString);
            string fileName = fileObj.File.FileName;
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write integers 0 - 99 to it.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.

            if (!System.IO.File.Exists(pathString))
            {
                {
                    System.IO.File.WriteAllBytes(pathString, bytesContentForFile);
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
            }

            return Ok("success");
        }
    }
}
