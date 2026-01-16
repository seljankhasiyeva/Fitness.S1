using System.Diagnostics;
using Fitness.S1.DAL;
using Fitness.S1.Models;
using Fitness.S1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.S1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TrainerVM trainerVM = new TrainerVM()
            {
                Trainers = _context.Trainers.ToList()
            };
            return View(trainerVM);
        }
    }
}
