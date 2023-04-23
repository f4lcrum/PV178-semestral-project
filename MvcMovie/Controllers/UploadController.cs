using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace FileUpload.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            try
            {
                if (file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    var rString = Guid.NewGuid().ToString();
                    fileName = rString + fileName;
                    string path = Path.Combine("UploadedFiles", fileName);
                    using FileStream fs = System.IO.File.OpenWrite(path);
                    await file.CopyToAsync(fs).ConfigureAwait(false);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}