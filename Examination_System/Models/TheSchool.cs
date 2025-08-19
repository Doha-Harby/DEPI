using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System.Models
{
    internal class TheSchool
    {
        public static int ID = 111;
        public static string Name = "El Shaheed School";

        public List<Instructors> instructors = new List<Instructors>();
        public List<Students> students = new List<Students>();
        public List<Courses> courses = new List<Courses>();

    }
}
