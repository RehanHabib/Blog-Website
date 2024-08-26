using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Blog_Website.Models.VewModels;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository )
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
                                                       


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Submittag(AddTagRequest addTagRequest)
        {
            // Mapping AddTagRequest to Tag domain Model
            var tag = new Tag
            {
                Name= addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
           
           await tagRepository.AddAsync(tag);
        

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        [ActionName("Edit")]
        //Get By ID
        public async Task<IActionResult> Edit(int id)
        {
            var tag =await tagRepository.GetAsync(id);
            if ( tag!= null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);

            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };


          var updatedTag= await  tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            { 
            
            }
            else
            {

            }

            return RedirectToAction("Edit", new{id= editTagRequest.Id  } );
            

        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }

}                                                 
