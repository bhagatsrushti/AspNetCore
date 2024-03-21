using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistration.Data;
using StudentRegistration.Models;

namespace StudentRegistration.Controllers
{
	[Authorize]
	public class StudentController : Controller
	{
		private readonly ApplicationDbcontext _applicationDbcontext;
        public StudentController(ApplicationDbcontext applicationDbcontext)
        {
           _applicationDbcontext = applicationDbcontext;
        }
        public async Task<IActionResult> Index()
		{
			List<StudentModel> students = new List<StudentModel>();
			students = await _applicationDbcontext.Student.ToListAsync();
			return View(students);
		}
		public async Task<IActionResult> AddStudent()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddStudent(StudentModel student)
		{
			if (ModelState.IsValid)
			{
                _applicationDbcontext.Student.Add(student);
				await _applicationDbcontext.SaveChangesAsync();
				return RedirectToAction("Index", "Student");
            }
			return View(student);
		}
		
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _applicationDbcontext.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
            
        }
        [HttpPost]
		public async Task<IActionResult> Edit(StudentModel student)
		{
			if (StudentExists(student.StudentID))
			{
                _applicationDbcontext.Update(student);
                await _applicationDbcontext.SaveChangesAsync();
                return RedirectToAction("Index", "Student");
            }

            return RedirectToAction("Index", "Student"); 
        }
        private bool StudentExists(int id)
        {
            return _applicationDbcontext.Student.Any(e => e.StudentID == id);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _applicationDbcontext.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound(); 
            }

            _applicationDbcontext.Student.Remove(student);
            await _applicationDbcontext.SaveChangesAsync();
            return RedirectToAction("Index", "Student");
        }


    }
}
