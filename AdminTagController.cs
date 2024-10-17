using BlogMVC.Data;
using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class AdminTagController : Controller
    {
        private BlogDbContext blogDbContext;
        public AdminTagController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddTagRequest atr)
        {
            var tag = new Tag
            { 
                Name = atr.Name,
                DisplayName = atr.DisplayName
            };


            blogDbContext.Add(tag);
            blogDbContext.SaveChanges();
            return View("Add");
            return RedirectToAction("List");
        
        }
        [HttpGet]
        public IActionResult List() 
        {
            var tags = blogDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = blogDbContext.Tags.Find(id);
            if (tag != null)
            {
                var etr = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(etr);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult Edit(EditTagRequest etr)
        {
            var tag = new Tag
            { 
            Id = etr.Id,
            Name = etr.Name,    
            DisplayName = etr.DisplayName
            };
            var oldTag = blogDbContext.Tags.Find(tag.Id);
            if (oldTag != null) { 
            oldTag.Name = etr.Name;
            oldTag.DisplayName = etr.DisplayName;
                blogDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction(null);
        }


    }
}
