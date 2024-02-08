using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordApp.Entities;

namespace StudentRecordApp.Controllers
{
    /// <summary>
    /// Controls the Add, Edit and Delete functions
    /// </summary>
    public class StudentController : Controller
    {
        private StudentDbContext _StudentDbContext;

        public StudentController (StudentDbContext StudentDbContext)
        {
            _StudentDbContext = StudentDbContext;
        }

        // Here we use attr routing to specify the URL
        // and this allows us to rename the method to something more meaningful
        [HttpGet("/Student/list")]
        public IActionResult GetAllStudents()
        {
            // Use our DB context to query for all Students, order them by GrossValue:
            List<Student> Students = _StudentDbContext.Students
                .Include(stu => stu.Program)
                .OrderBy(stu => stu.StudentLastName)
                .ToList();

            // Pass that list off to the view using the view name:
            return View("List", Students);
        }




        // The GET handler that returns the blank add form:
        [HttpGet()]
        public IActionResult Add()
        {
            // query for all the Programs:
            var programs = _StudentDbContext.Programs.OrderBy(p => p.Name).ToList();

            // Define a view model with a "empty Student object"
            StudentViewModel StudentViewMOdel = new StudentViewModel()
            {
                ActiveStudent = new Student(),
                Programs = programs
            };

            // And return it to the view:
            return View(StudentViewMOdel);
        }

        // The POST handler that accepts the new Student in the
        // body and adds it to the DB; ASP.NET Core binding in the
        // pipeline takes the URL encoded POST body and creates a
        // Student object for us:
        // v2: a full View model is POSTed to us
        [HttpPost()]
        public IActionResult Add(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the student to Students coll'n:
                _StudentDbContext.Students.Add(studentViewModel.ActiveStudent);

                // and then save changes:
                _StudentDbContext.SaveChanges();

                // redirect to the all students page:
                return RedirectToAction("List", "Student");
            }
            else
            {
                // query for all the programs again:
                var programs = _StudentDbContext.Programs.OrderBy(p => p.Name).ToList();

                // update the view model with the latest Programs:
                studentViewModel.Programs = programs;

                // return the Student view model with invalid data and the
                // validn err msgs that go with that:
                return View(studentViewModel);
            }
        }


        // The GET handler that returns the edit form preloaded with the Student's data
        // where the ID for the Student is passes as route (i.e. URL) parameter:
        [HttpGet()]
        public IActionResult Edit(int id)
        {
            // Use the ID passed as an arg to retrieve the Student from the DB:
            var Student = _StudentDbContext.Students.Find(id);

            // query for all the Programs:
            var Programs = _StudentDbContext.Programs.OrderBy(g => g.Name).ToList();

            // Define a view model with a "empty Student object"
            StudentViewModel StudentViewMOdel = new StudentViewModel()
            {
                ActiveStudent = Student,
                Programs = Programs
            };

            // And return it to the view:
            return View(StudentViewMOdel);
        }

        // The POST handler that accepts the existing Student in the
        // body and updates it in the DB; ASP.NET Core binding in the
        // pipeline takes the URL encoded POST body and creates a
        // Student object for us:
        [HttpPost()]
        public IActionResult Edit(StudentViewModel StudentViewModel)
        {
            if (ModelState.IsValid)
            {
                // UPdates Student to Students coll'n:
                _StudentDbContext.Students.Update(StudentViewModel.ActiveStudent);

                // and then save changes:
                _StudentDbContext.SaveChanges();

                // redirect to the all Students page:
                return RedirectToAction("List", "Student");
            }
            else
            {
                // query for all the Programs again:
                var Programs = _StudentDbContext.Programs.OrderBy(g => g.Name).ToList();

                // update the view model with the latest Programs:
                StudentViewModel.Programs = Programs;

                // return the Student view model with invalid data and the
                // validn err msgs that go with that:
                return View(StudentViewModel);
            }
        }


        // The GET handler that returns the delete conformation form displaying with the Student's name & year
        // where the ID for the Student is passes as route (i.e. URL) parameter:
        [HttpGet()]
        public IActionResult Delete(int id)
        {
            // Use the ID passed as an arg to retrieve the Student from the DB:
            var Student = _StudentDbContext.Students.Find(id);

            // Return that Student to the delete form:
            return View(Student);
        }

        [HttpPost()]
        public IActionResult Delete(Student Student)
        {
            // NOTE: the Student object only has an ID field, BUT that is 
            // enough for EF Core to successfully delete it:
            _StudentDbContext.Students.Remove(Student);

            // save the changes:
            _StudentDbContext.SaveChanges();

            // redirect to the all Students page:
            return RedirectToAction("List", "Student");
        }







    }
}
