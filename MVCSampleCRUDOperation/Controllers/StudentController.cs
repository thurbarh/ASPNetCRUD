using MVCSampleCRUDOperation.Models;
using MVCSampleCRUDOperation.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSampleCRUDOperation.Controllers
{
    public class StudentController : Controller
    {
        MVCSampleCRUD_DBEntities dbContext = new MVCSampleCRUD_DBEntities();
        public ActionResult Index()
        {
            var model = dbContext.Students.Select(m=>new StudentViewModel()
            {
                Student_Id=m.Id,
                Student_Age=m.Age,
                Student_DOB=m.DOB,
                Student_Email=m.Email,
                Student_Name=m.Name,
                Address_Line1=m.AddressLine1,
                Address_Line2=m.AddressLine2,
                Phone_Number=m.PhoneNumber

            }).ToList();

            return View(model);
        }
        public ActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(StudentViewModel model)
        {

            try
            {
                var student = new Student()
                {
                    Name = model.Student_Name,
                    PhoneNumber = model.Phone_Number,
                    DOB = model.Student_DOB,
                    Email = model.Student_Email,
                    AddressLine1 = model.Address_Line1,
                    AddressLine2 = model.Address_Line2,
                    Age = DateTime.Now.Subtract(model.Student_DOB.Value).Days
                };
                dbContext.Students.Add(student);
                if (dbContext.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",$"Error {ex?.Message}");
                return View(model);
            }
            ModelState.AddModelError("", "Error occurred while saving record, please try again");
            return View(model);
        }
        public ActionResult ViewStudent(int id=0)
        {
            var model = new StudentViewModel();
            var data = dbContext.Students.Find(id);
            if (data != null)
            {
                model.Address_Line1 = data.AddressLine1;
                model.Address_Line2 = data.AddressLine2;
                model.Phone_Number = data.PhoneNumber;
                model.Student_Age = data.Age;
                model.Student_DOB = data.DOB;
                model.Student_Email = data.Email;
                model.Student_Id = data.Id;
                model.Student_Name = data.Name;
            }
            return View(model);
        }
        public ActionResult EditStudent(int id = 0)
        {
            var model = new StudentViewModel();
            var data = dbContext.Students.Find(id);
            if (data != null)
            {
                model.Address_Line1 = data.AddressLine1;
                model.Address_Line2 = data.AddressLine2;
                model.Phone_Number = data.PhoneNumber;
                model.Student_Age = data.Age;
                model.Student_DOB = data.DOB;
                model.Student_Email = data.Email;
                model.Student_Id = data.Id;
                model.Student_Name = data.Name;
            }
            return View(model);
        }
        public ActionResult Edit_student(StudentViewModel model)
        {
            try
            {

                var student = dbContext.Students.Find(model.Student_Id);
                if (student != null)
                {
                    student.Name = model.Student_Name;
                    student.PhoneNumber = model.Phone_Number;
                    student.DOB = model.Student_DOB;
                    student.Email = model.Student_Email;
                    student.AddressLine1 = model.Address_Line1;
                    student.AddressLine2 = model.Address_Line2;
                    student.Age = DateTime.Now.Subtract(model.Student_DOB.Value).Days;

                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error {ex?.Message}");
                return View("EditStudent", model);
            }
            ModelState.AddModelError("", "Error occurred while saving record, please try again");
            return View("EditStudent", model);
        }
        public ActionResult DeleteStudent(int id = 0)
        {
            var model = new StudentViewModel();
            var data = dbContext.Students.Find(id);
            dbContext.Students.Remove(data);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}