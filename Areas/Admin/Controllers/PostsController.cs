using Bilet_1.DAL;
using Bilet_1.Models;
using Bilet_1.Utilities.Contains;
using Bilet_1.Utilities.Extensions;
using Bilet_1.ViewModels;
using Bilet_1.ViewMoldes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bilet_1.Areas.Admin.Controllers
{
    
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public PostsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [Area("Admin")]
        public async Task<IActionResult>  Index()
        {
            List<Post> posts = await _context.Posts.Where(p=>!p.IsDeleted).OrderByDescending(p=>p.Id).ToListAsync();
            return View(posts);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreatePostVM postVM)
        {
            if (!ModelState.IsValid) return View(postVM);
            if (!postVM.Photo.CheckContentType("image/")) {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageType);
                return View(postVM);
            }

            if (!postVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileSizeMustLessThan200kb);
                return View(postVM);    
            }
            string rootPath = Path.Combine(_environment.WebRootPath, "assets", "images");
           string fileName = await postVM.Photo.SaveAsync(rootPath);
            Post post = new Post()
            {
                Title= postVM.Title,
                Description= postVM.Description,
                ImagePath= fileName
            };
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }



        public async Task<IActionResult> Update(int id)
        {
            Post post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            UpdatePostVM update = new UpdatePostVM()
            {
                Title = post.Title,
                Description = post.Description,
                Id = id
            };
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(UpdatePostVM postVM)
        {
            if (!ModelState.IsValid) return View(postVM); return View(postVM);  
            //if (!postVM.Photo.CheckContentType("image/"))
            //{
            //    ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageType);
            //    return View(postVM);
            //}

            //if (!postVM.Photo.CheckFileSize(200))
            //{
            //    ModelState.AddModelError("Photo", ErrorMessages.FileSizeMustLessThan200kb);
            //    return View(postVM);
            //}
            //string rootPath = Path.Combine(_environment.WebRootPath, "assets", "images");
            //string fileName = await postVM.Photo.SaveAsync(rootPath);
            //Post post = new Post()
            //{
            //    Title = postVM.Title,
            //    Description = postVM.Description,
            //    ImagePath = fileName
            //};
            //await _context.AddAsync(post);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult>  Delete(int id)
        {
          Post post=  await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "images",post.ImagePath);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            _context.Posts.Remove(post);
           await _context.SaveChangesAsync();//cox adamin bali gedib buna gore yadda saxla
            return RedirectToAction(nameof(Index));
           
        }
    }
}
