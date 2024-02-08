using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace StudentRecordApp.Entities
{
    /// <summary>
    /// This class represents the columns in the Student table in the DB
    /// </summary>
    public class Student
    {
        /// <summary>
        /// this corresponds with the table PK and will work as an auto number
        /// since it's an int
        /// </summary>
        /// 
        [Required(ErrorMessage = "Please enter the Student ID")]
        public int StudentId { get; set; }



        // Calculate GPA Scale given the GPA
        static string DetermineGPAScale(double gpa, double[] gpaRanges, string[] gpaScales)
        {
            for (int i = 0; i < gpaRanges.Length; i++)
            {
                if (gpa >= gpaRanges[i])
                {
                    return gpaScales[i];
                }
            }
            return "Unknown"; // If GPA is out of range
        }


        public string? StudentGPAScale
        {
            get
            {
                // Define GPA ranges and corresponding scales
                //double[] gpaRanges = { 4.0, 3.7, 3.5, 3.0, 2.7, 2.5, 2.0, 1.7, 1.5, 1.0, 0.7, 0.0 };
                //string[] gpaScales = { "A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "F" };

                double[] gpaRanges = { 4.0, 3.0, 2.0, 1.0, 0.0 };
                string[] gpaScales = { "Excellent", "Very Good", "Satisfactory", "Unsatisfactory" };

                // Determine GPA scale
                string gpaScale = DetermineGPAScale(StudentGPA, gpaRanges, gpaScales);

                return gpaScale;
            }
        }

        public int? StudentAge
        {
            get
            {
                if (StudentBirthDate != null)
                {
                    // Split the birth date string by '/'
                    string[] parts = StudentBirthDate.Split('/');

                    if (parts.Length == 3)
                    {
                        // Parse day, month, and year parts into integers
                        if (int.TryParse(parts[0], out int day) && int.TryParse(parts[1], out int month) && int.TryParse(parts[2], out int year))
                        {
                            // Calculate age based on birth date
                            var age = DateTime.Now.Year - year;

                            if (DateTime.Now.Month <= month)
                            {
                                if (DateTime.Now.Day < day)
                                {
                                    age--;
                                }
                            }
                            return age;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format");
                        }
                    }
                }

                return null; // Return null if StudentBirthDate is null or if parsing fails
            }
        }



        [Required(ErrorMessage = "Please enter the first Name")]
        public string? StudentFirstName { get; set; }


        [Required(ErrorMessage = "Please enter the last Name")]
        public string? StudentLastName { get; set; }

        [Required(ErrorMessage = "Please enter Birth Date")]
        public string? StudentBirthDate { get; set; }

        [Required(ErrorMessage = "Please enter the GPA")]
        [Range(0, 4.0, ErrorMessage = "GPA must be between 0 and 4.0")]
        public double StudentGPA { get; set; }





        // Establish the relationship with a Program, first by foreign key (FK):
        // NOTE: we are not forcing this to be required to ensure backwards comp'y
        public string? ProgramId { get; set; }

        // And add a navigation prop to the full associated Program object:
        public Program? Program { get; set; }
    }
}
