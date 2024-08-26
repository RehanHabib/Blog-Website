﻿using Blog_Website.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace Blog_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRespository;

        public ImagesController(IImageRepository imageRespository)
        {
            this.imageRespository = imageRespository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var imageURL = await imageRespository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Sometihng went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
