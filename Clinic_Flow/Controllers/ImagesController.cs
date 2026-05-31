using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ImagesController : ControllerBase
        {

            [HttpPost("UploadImage")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public async Task<IActionResult> UploadImage(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var uploadDirectory = @"C:\Myuploaded";


                var fillName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var FilePath = Path.Combine(uploadDirectory, fillName);


                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                // Save the file to the specified path

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new { FilePath });
            }







            //===================================

            // Endpoint to retrieve image from the server

            [HttpGet("GetImage/{fileName}")]
            public IActionResult GetImage(string fileName)
            {
                // Directory where files are stored
                var uploadDirectory = @"C:\Myuploaded";
                var filePath = Path.Combine(uploadDirectory, fileName);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                    return NotFound("Image not found.");

                // Open the image file for reading
                var image = System.IO.File.OpenRead(filePath);
                var mimeType = GetMimeType(filePath);

                // Return the file with the correct MIME type
                return File(image, mimeType);
            }

            // Helper method to get the MIME type based on file extension
            /*
             This code defines a  method called GetMimeType that takes a file path as a parameter 
             and returns the corresponding MIME type as a string. 
             MIME types are used to indicate the nature and format of a file, 
             especially in web contexts where you need to specify the type of content you're sending, 
             like images, text, etc.

            MIME type stands for Multipurpose Internet Mail Extensions type. 
            It's a standard way to indicate the nature and format of a file or content. 
            MIME types are used to tell browsers, email clients, and 
            other software about the type of data they're handling, so they can process it correctly.
             */
            private string GetMimeType(string filePath)
            {
                var extension = Path.GetExtension(filePath).ToLowerInvariant();
                return extension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    _ => "application/octet-stream",
                };
            }
        }
    }
