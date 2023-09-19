using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentRegistrationInCore.Models;
using StudentRegistrationInCore.Entity;
using System.Xml;

namespace StudentRegistrationInCore.Controllers
{
    public class StudentController : Controller
    {
        private  StudentDbContext db;
        public StudentController(StudentDbContext context)
        {
            db = context;
        }

        // GET: Student/Index
        public IActionResult Index()
        {
            StudentViewModel model = new StudentViewModel();
            var students = (from st in db.TblStudents
                            select new StudentViewModel
                            {
                                StudentId = st.Id,
                                Name = st.Name,
                                Address = st.Address,
                                ClassId = st.ClassId,
                                Phone = st.Phone,
                                
                            }).ToList();
            return View(students);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            StudentViewModel model = new StudentViewModel();
            model.ClassList = db.TblClasses.Select(x => new Dropdown
            {
                Id = x.ClassId,
                Value = x.Grade.ToString(),
            }).ToList();

            model.GenderList = db.TblGenders.Select(x => new Dropdown
            {
                Id = x.GenderId,
                Value = x.GenderName,
            }).ToList();

            model.hobbyModel = (from hob in db.TblHobbies
                                select new HobbyModel
                                {
                                    HobbyId = hob.HobbyId,
                                    HobbyName = hob.HobbyName,
                                    IsActive = hob.IsActive,
                                }).ToList();
            return View(model);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentViewModel imodel)
        {
            if (!ModelState.IsValid)
            {
                imodel.ClassList = db.TblClasses.Select(x => new Dropdown
                {
                    Id = x.ClassId,
                    Value = x.Grade.ToString(),
                }).ToList();

                imodel.GenderList = db.TblGenders.Select(x => new Dropdown
                {
                    Id = x.GenderId,
                    Value = x.GenderName,
                }).ToList();

                imodel.hobbyModel = (from hob in db.TblHobbies
                                    select new HobbyModel
                                    {
                                        HobbyId = hob.HobbyId,
                                        HobbyName = hob.HobbyName,
                                        IsActive = hob.IsActive,
                                    }).ToList();
                return View(imodel);
            }

            if (imodel.hobbyModel.Count(x => x.IsActive == true) == 0)
            {
                return View(imodel.HobbyNotSelectedError());
            }
            else
            {
                //to concat hobbies
                StringBuilder hobbies = new StringBuilder();
                foreach (var hobby in imodel.hobbyModel)
                {
                    if (hobby.IsActive)
                    {
                        hobbies.Append(hobby.HobbyName + ", ");
                    }
                }
                hobbies.Remove(hobbies.ToString().LastIndexOf(","), 1);
                imodel.Hobbies = hobbies.ToString();
            }

            //Upload documents
            TblDocument doc = new TblDocument();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileNameWithPath = Path.Combine(path, imodel.singleFileModel.File.FileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                imodel.singleFileModel.File.CopyTo(stream);
            }
            doc.Title = imodel.singleFileModel.File.FileName;
            doc.DocPath = fileNameWithPath;
            db.TblDocuments.Add(doc);
            db.SaveChanges();
            imodel.DocId = doc.DocId;

            //Upload photo
            TblImage im = new TblImage();
            string photopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos");

            if (!Directory.Exists(photopath))
                Directory.CreateDirectory(photopath);

            string photoNameWithPath = Path.Combine(photopath, imodel.photoUpload.Photo.FileName);
            using (var stream = new FileStream(photoNameWithPath, FileMode.Create))
            {
                imodel.photoUpload.Photo.CopyTo(stream);
            }
            im.Title = imodel.photoUpload.Photo.FileName;
            im.ImagePath = photoNameWithPath;
            db.TblImages.Add(im);
            db.SaveChanges();

            TblStudent ts = new TblStudent();
            ts.Address = imodel.Address;
            ts.Name = imodel.Name;
            ts.GenderId = imodel.GenderId;
            ts.Phone = imodel.Phone;
            ts.ImageId = im.ImageId;
            ts.DocId = doc.DocId;
            ts.ClassId = imodel.ClassId;
            ts.RegisteredDate = imodel.RegisteredDate;
            ts.Hobbies = imodel.Hobbies;

            db.TblStudents.Add(ts);
            db.SaveChanges();
            
            var hobbyIdList = imodel.hobbyModel.Where(x => x.IsActive == true).Select(x => x.HobbyId).ToList();
            foreach (var id in hobbyIdList)
            {
                TblMapping tm = new TblMapping();
                tm.StudentId = ts.Id;
                tm.HobbyId = id;
                db.TblMappings.Add(tm);
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }
    }
}
