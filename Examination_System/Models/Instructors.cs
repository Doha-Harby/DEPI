using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System.Models
{
    internal class Instructors
    {
        private static int _insCounter = 100;
        //private string _name;
        //private string _specialization;
       
        public int InstractorID { get ; private set; }
        public string Name { get; set; }    
        public string Specialization { get; set; }
        public List<Courses> TeachCourses { get; set; } = new List<Courses>();

        public Instructors(String name, string specialization)
        {
            InstractorID = _insCounter++;
            Name = name;
            Specialization = specialization;
        }

        public void AddCourse(Courses course)
        {
            if (course != null)
            {
                TeachCourses.Add(course);
            }
            else
            {
                Debug.WriteLine("Course can not be null");
            }
        }

        public void RemoveCourse(Courses course)
        {
            if (course != null)
            {
                TeachCourses.Remove(course);
            }
            else
            {
                Debug.WriteLine("Course can not be null");
            }
        }

        public void AddExamToCourse(Courses course, Exams exam)
        {
            if (course != null && exam != null)
            {
                course.AddExam(exam);
            }
            else
            {
                Debug.WriteLine("Course or Exam can not be null");
            }
        }

        public void RemoveExamFromCourse(Courses course, Exams exam)
        {
            if (course != null && exam != null)
            {
                course.RemoveExam(exam);
            }
            else
            {
                Debug.WriteLine("Course or Exam can not be null");
            }
        }

        public void EditExamFromCourse(Exams exam)
        {
            if (exam != null)
            {
                exam.EditExam();
            }
            else
            {
                Debug.WriteLine("Exam can not be null");
            }
        }

        public void AssignQuestionToExam(Courses course, Exams exam, Questions question)
        {
            if (course != null && exam != null && question != null)
            {
                if (course.CoursesQuestions.Contains(question))
                {
                    exam.AssignQuestion(question);
                }
                else
                {
                    Console.WriteLine("This question does not belong to the selected course.");
                }
            }
        }

        public Courses FindCourseById(int courseId)
        {
            foreach (var course in TeachCourses)
            {
                if (course.CourseID == courseId)
                    return course;
            }
            return null;
        }

    }
}
