using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace StudentRecordApp.Entities
{
    /// <summary>
    /// This class will inherit from the Entity Framework (EF) class
    /// called DbContext and is used by the code to interact with the DB
    /// </summary>
    public class StudentDbContext : DbContext
    {
        /// <summary>
        /// Define a constructor that simply passes the options argument
        /// up to the base class constuctor
        /// </summary>
        /// <param name="options"></param>
        /// 
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options) { }



        // Adding a property to access all Students
        public DbSet<Student> Students { get; set; }



        // Adding a property to access all Programs
        public DbSet<Program> Programs { get; set; }



        // override the OnModelBuilding method as a place to do init'n
        // which for us will be to seed the DB w some data:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some Program:
            modelBuilder.Entity<Program>().HasData(
                new Program() { ProgramId = "CSI", Name = "Computer Science" },
                new Program() { ProgramId = "STA", Name = "Statistics" },
                new Program() { ProgramId = "HIS", Name = "History" },
                new Program() { ProgramId = "ENG", Name = "English" },
                new Program() { ProgramId = "FRE", Name = "French" }
            );

            // Seed the DB with at least 4 Students
            modelBuilder.Entity<Student>().HasData(
                 new Student()
                 {
                     StudentId = 1,
                     StudentFirstName = "John",
                     StudentLastName = "Doe",
                     StudentBirthDate = "1/01/2000",
                     StudentGPA = 3.8,
                     ProgramId = "FRE"
                 },
                 new Student()
                 {
                     StudentId = 2,
                     StudentFirstName = "Jane",
                     StudentLastName = "Smith",
                     StudentBirthDate = "15/05/1999",
                     StudentGPA = 3.5,
                     ProgramId = "STA"
                 },
                 new Student()
                 {
                     StudentId = 3,
                     StudentFirstName = "Alice",
                     StudentLastName = "Johnson",
                     StudentBirthDate = "20/08/2001",
                     StudentGPA = 4.0,
                     ProgramId = "HIS"
                 },
                 new Student()
                 {
                     StudentId = 4,
                     StudentFirstName = "Bob",
                     StudentLastName = "Williams",
                     StudentBirthDate = "10/03/2002",
                     StudentGPA = 3.2,
                     ProgramId = "ENG"
                 },
                 new Student()
                 {
                     StudentId = 5,
                     StudentFirstName = "Liam",
                     StudentLastName = "Knapp",
                     StudentBirthDate = "11/08/2003",
                     StudentGPA = 3.2,
                     ProgramId = "CSI"
                 }
            );


        }
    }
}
