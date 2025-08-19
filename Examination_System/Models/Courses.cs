using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System.Models
{
    internal class Courses
    {
        public static double MaxDgree = 100;
        private static int _courseCounter = 100;
        public int CourseID { get; private set; }
        public string CourseTitle { get; set; }
        public int CourseCode { get; set; }
        public string Description { get; set; }
        public List<Exams> CoursesExams { get; set; } = new List<Exams>();
        public List<Questions> CoursesQuestions { get; set; } = new List<Questions>();

        public Courses(string title, int code, string description)
        {
            CourseID = _courseCounter++;
            CourseTitle = title;
            CourseCode = code;
            Description = description;
        }

        public void AddExam(Exams exam)
        {
            if (exam != null)
            {
                CoursesExams.Add(exam);
                Console.WriteLine($"The exam: {exam.ExamID} => {exam.ExamTitle} is added to the {this.CourseCode} {this.CourseTitle} ");
            }
            else
            {
                Console.WriteLine("Exam can not be null");
            }
        }

        public void RemoveExam(Exams exam)
        {
            if (exam != null && CoursesExams.Contains(exam))
            {
                CoursesExams.Remove(exam);
                Console.WriteLine($"The exam: {exam.ExamID} => {exam.ExamTitle} is removed from the {this.CourseCode} {this.CourseTitle} ");
            }
            else
            {
                Console.WriteLine("Exam can not be null or not found in the course");
            }
        }

        public void AddQuestion()
        {
            Console.WriteLine("Enter the question text:");
            string questionText = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(questionText))
            {
                Console.WriteLine("Question text can not be empty.");
                return;
            }
            Console.WriteLine("Enter the question type (e.g., MCQ, True/False, Essay):");
            string questionType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(questionType))
            {
                Console.WriteLine("Question type can not be empty.");
                return;
            }
            Console.WriteLine("Enter the answer:");
            string answer = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(answer))
            {
                Console.WriteLine("Answer can not be empty.");
                return;
            }
            Console.WriteLine("Enter the Mark:");
            if (!double.TryParse(Console.ReadLine(), out double mark) || mark < 0 || mark > Courses.MaxDgree)
            {
                Console.WriteLine($"Invalid degree. It must be between 0 and {Courses.MaxDgree}.");
                return;
            }

            Questions newQuestion = new Questions(this.CourseID, questionText, questionType, answer, mark);
            CoursesQuestions.Add(newQuestion);
            Console.WriteLine($"Question added with ID: {newQuestion.QuestionID}");
        }

        public Questions FindQuestionById(int questionId)
        {
            foreach (var q in CoursesQuestions)
            {
                if (q.QuestionID == questionId)
                {
                    return q; 
                }
            }
            return null; 
        }

        public void EditQuestion()
        {
            Console.WriteLine("Enter the Question ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int questionId))
            {
                Questions questionToEdit = FindQuestionById(questionId);
                if (questionToEdit != null)
                {
                    questionToEdit.EditQuestion();
                    Console.WriteLine($"Question with ID: {questionId} has been updated.");
                }
                else
                {
                    Console.WriteLine("Question not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Question ID.");
            }
        }

        public void RemoveQuestionFromCourse()
        {
            Console.WriteLine("Enter the Question ID to remove:");
            if (int.TryParse(Console.ReadLine(), out int questionId))
            {
                Questions questionToRemove = FindQuestionById(questionId);
                if (questionToRemove != null)
                {
                    CoursesQuestions.Remove(questionToRemove);
                    Console.WriteLine($"Question with ID: {questionId} has been removed.");
                }
                else
                {
                    Console.WriteLine("Question not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Question ID.");
            }
        }

        public void DisplayCourseDetails()
        {
            Console.WriteLine($"Course ID: {CourseID}");
            Console.WriteLine($"Course Title: {CourseTitle}");
            Console.WriteLine($"Course Code: {CourseCode}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine("Exams in this course:");
            foreach (var exam in CoursesExams)
            {
                Console.WriteLine($"Exam ID: {exam.ExamID}, Title: {exam.ExamTitle}, Max Degree: {exam.MaxDegree}");
            }
            Console.WriteLine("Questions in this course:");
            foreach (var question in CoursesQuestions)
            {
                Console.WriteLine($"Question ID: {question.QuestionID}, Text: {question.QuestionText}, Type: {question.QuestionType}, Answer: {question.Answer}, Mark: {question.Mark}");
            }



        }


    }
}