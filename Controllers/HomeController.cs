using Bilet_1.Models;
using Bilet_1.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bilet_1.Controllers
{
    
    public class HomeController:Controller
    { 
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult>  Index()
        {
            return View(await _context.Posts.Where(p =>!p.IsDeleted).OrderByDescending(p=>p.Id).Take(3).ToListAsync());
        }

       
    }
}