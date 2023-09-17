using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationInCore.Models;
using StudentRegistrationInCore.Models.DB;

namespace StudentRegistrationInCore.Controllers
{
    public class StudentModelsController : Controller
    {
        private readonly StudentDbContext db;
        public StudentModelsController(StudentDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            StudentModel model = new StudentModel();
            var students = (from st in db.TblStudents
                           select new StudentModel
                           {
                               Id = st.Id,
                               Name = st.Name,
                               Address = st.Address,
                               ClassId = st.ClassId,
                               Phone = st.Phone,
                           }).ToList();
            return View(students);
        }
    }
}
