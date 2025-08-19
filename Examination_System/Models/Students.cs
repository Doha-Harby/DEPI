using System;
using System.Collections.Generic;

namespace Examination_System.Models
{
    internal class Students
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Courses> EnrolledCourses { get; set; } = new List<Courses>();
        public Dictionary<int, double> ExamScores { get; set; } = new Dictionary<int, double>(); // ExamID -> Score

        public Students(int id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }

        public void EnrollInCourse(Courses course)
        {
            if (course != null && !EnrolledCourses.Contains(course))
            {
                EnrolledCourses.Add(course);
                Console.WriteLine($"{Name} enrolled in {course.CourseTitle}.");
            }
            else
            {
                Console.WriteLine("Invalid course or already enrolled.");
            }
        }

        public void TakeExam(Exams exam)
        {
            if (exam != null && EnrolledCourses.Contains(exam.Course))
            {
                double score = exam.TakeExam();
                ExamScores[exam.ExamID] = score;
                Console.WriteLine($"{Name}'s score for exam {exam.ExamTitle} (ID: {exam.ExamID}) is {score}/{exam.MaxDegree}.");
            }
            else
            {
                Console.WriteLine("Cannot take exam. Ensure you are enrolled in the course.");
            }
        }
    }
}