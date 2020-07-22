using ArmyTechTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArmyTechTask.Controllers
{
    public class StudentController : Controller
    {
        ArmyTechTaskEntities db = new ArmyTechTaskEntities();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        //Get All Student
        public JsonResult List()
        {
            List<Student> students = db.Students.ToList();
            return Json(students.Select(x => new {
                Id = x.ID,
                Name = x.Name,
                BirthDate = x.BirthDate.ToString(),
                Governorate = x.Governorate.Name,
                Neighborhood = x.Neighborhood.Name,
                Field=x.Field.Name

            }), JsonRequestBehavior.AllowGet);
        }
        //Get All Governorates
        public JsonResult GetGovernorates()
        {
            List<Governorate> governorates = db.Governorates.ToList();
            return Json(governorates.Select(x => new {
                ID = x.ID,
                Name = x.Name
            }), JsonRequestBehavior.AllowGet);
        }
        //Get All Neighborhoods
        public JsonResult GetNeighborhood()
        {
            List<Neighborhood> neighborhoods = db.Neighborhoods.ToList();
            return Json(neighborhoods.Select(x => new {
                ID = x.ID,
                Name = x.Name
            }), JsonRequestBehavior.AllowGet);
        }
        //Get All Fields
        public JsonResult GetFields()
        {
            List<Field> fields = db.Fields.ToList();
            return Json(fields.Select(x => new {
                ID = x.ID,
                Name = x.Name
            }), JsonRequestBehavior.AllowGet);
        }

        //Add New Student
        public int Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return student.ID;
        }
        public JsonResult AddStudent(Student student)
        {
            return Json(Add(student), JsonRequestBehavior.AllowGet);
        }

        //Get Student row
        public JsonResult GetbyID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var student1 = db.Students.Where(x => x.ID == ID).FirstOrDefault();
            Student student = new Student
            {
                ID = student1.ID,
                Name = student1.Name,
                BirthDate = student1.BirthDate,
                NeighborhoodId = student1.NeighborhoodId,
                GovernorateId = student1.GovernorateId,
                FieldId = student1.FieldId
            };
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        //Update Student
        public JsonResult UpdateStudent(Student student)
        {
            return Json(Update(student), JsonRequestBehavior.AllowGet);
        }
        public int Update(Student student)
        {
            Student f = db.Students.Where(x => x.ID == student.ID).FirstOrDefault();
            f.Name = student.Name;
            f.BirthDate = student.BirthDate;
            f.GovernorateId = student.GovernorateId;
            f.NeighborhoodId = student.NeighborhoodId;
            f.FieldId = student.FieldId;

            db.SaveChanges();

            return student.ID;

        }

        //Delete Student
        public JsonResult DeleteStudent(int ID)
        {
            return Json(delete(ID), JsonRequestBehavior.AllowGet);
        }
        public int delete(int Id)
        {
            Student student = db.Students.Where(x => x.ID == Id).FirstOrDefault();

            db.Students.Remove(student);
            db.SaveChanges();

            return student.ID;

        }

    }
}