using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudentManagementSystem.MVVM.Model
{
    public class Student
    {
        [Key]
        public string StdID { get; set; }
        public string FirstName { get; set; }       
        public string LastName { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public int TelNo { get; set; }
        public string Dept { get; set; }
        public double Gpa { get; set; }
        public byte[] Img { get; set; }
        private static int count = 0;

        public Student( )
        {
            StdID = GenerateStudentID();
          
        }

        public string GenerateStudentID()
        {
            string facultyCode = "EG";
            int currentYear = DateTime.Now.Year;

           
            int sequenceNumber = ++count;

            string year = "2020";
            string regNO = sequenceNumber.ToString().PadLeft(4, '0');

            string studentID = $"{facultyCode}/{year}/{regNO}";

            return studentID;
        }

    }
}
