using Microsoft.AspNetCore.Mvc;
using Fitness.S1.Models;        
using Fitness.S1.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fitness.S1.Areas.Admin.ViewModels;
using Fitness.S1.Utilities.Enums;
using Fitness.S1.Utilities.Extensions;

namespace Fitness.S1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public TrainerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Trainer> trainers= await _context.Trainers.ToListAsync();
            return View(trainers);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerVM createTrainerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(createTrainerVM);
            }
            if(!createTrainerVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View(createTrainerVM);
            }
            if(!createTrainerVM.Photo.ValidateSize(FileSize.MB, 20))
            {
                ModelState.AddModelError("Photo", "File size is incorrect");
                return View(createTrainerVM);
            }
            Trainer trainer = new Trainer()
            {
                Name=createTrainerVM.Name,
                Specialty=createTrainerVM.Specialty,
                Bio=createTrainerVM.Bio,
                PhotoUrl=await createTrainerVM.Photo.CreateFile(_env.WebRootPath, "images" )
            };
            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Update(int? id)
        {
            if(id==null || id<1)
            {
                return BadRequest();
            }
            Trainer trainer=await _context.Trainers.FirstOrDefaultAsync(t=>t.Id==id);
            if(trainer==null)
            {
                return NotFound();
            }
            UpdateTrainerVM updateTrainerVM=new UpdateTrainerVM()
            {
                Id=trainer.Id,
                Name=trainer.Name,
                Specialty=trainer.Specialty,
                Bio=trainer.Bio,
                PhotoUrl=trainer.PhotoUrl
            };
            return View(updateTrainerVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, UpdateTrainerVM updateTrainerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(updateTrainerVM);
            }
            Trainer trainer=await _context.Trainers.FirstOrDefaultAsync(t=>t.Id==id);
            if(updateTrainerVM.Photo!=null)
            {
                if(!updateTrainerVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(updateTrainerVM.Photo), "File type must be image");
                    return View(updateTrainerVM);
                }
                if(!updateTrainerVM.Photo.ValidateSize(FileSize.MB, 20))
                {
                    ModelState.AddModelError(nameof(updateTrainerVM.Photo), "File size is incorrect");
                    return View(updateTrainerVM);
                }
                string fileName=await updateTrainerVM.Photo.CreateFile(_env.WebRootPath, "images");
                trainer.PhotoUrl.DeleteFile(_env.WebRootPath, "images");
                trainer.PhotoUrl=fileName;
            }
            trainer.Name=updateTrainerVM.Name;
            trainer.Specialty=updateTrainerVM.Specialty;
            trainer.Bio=updateTrainerVM.Bio;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null || id<1)
            {
                return BadRequest();
            }
            Trainer trainer=await _context.Trainers.FirstOrDefaultAsync(t=>t.Id==id);
            if(trainer==null)
            {
                return NotFound();
            }
            trainer.PhotoUrl.DeleteFile(_env.WebRootPath, "images");
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            Trainer trainer=await _context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            if(trainer==null)
            {
                return NotFound();
            }

            DetailTrainerVM detailTrainerVM = new DetailTrainerVM()
            {
                Name = trainer.Name,
                Specialty = trainer.Specialty,
                Bio = trainer.Bio,
                PhotoUrl = trainer.PhotoUrl
            };
            return View(detailTrainerVM);
        }
    }
}
