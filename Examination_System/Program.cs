using Examination_System.Models;
using System;
using System.Collections.Generic;

namespace HelloApp
{
    internal class Program
    {
        private static Instructors FindInstructorById(List<Instructors> instructorsList, int insId)
        {
            foreach (var instructor in instructorsList)
            {
                if (instructor.InstractorID == insId)
                {
                    return instructor;
                }
            }
            return null;
        }

        private static Courses FindCourseById(List<Courses> coursesList, int courseId)
        {
            foreach (var course in coursesList)
            {
                if (course.CourseID == courseId)
                {
                    return course;
                }
            }
            return null;
        }

        private static Students FindStudentById(List<Students> studentsList, int studentId)
        {
            foreach (var student in studentsList)
            {
                if (student.ID == studentId)
                {
                    return student;
                }
            }
            return null;
        }

        static void Main(string[] args)
        {
            #region ini
            TheSchool school = new TheSchool();

          
            Courses course1 = new Courses("Computer Science", 101, "Introduction to Computer Science");
            Courses course2 = new Courses("Mathematics", 102, "Basic Mathematics Concepts");
            Courses course3 = new Courses("Physics", 103, "Fundamentals of Physics");
            Courses course4 = new Courses("Chemistry", 104, "Introduction to Chemistry");
            school.courses.AddRange(new[] { course1, course2, course3, course4 });

            Instructors instructor1 = new Instructors("John Doe", "Computer Science");
            Instructors instructor2 = new Instructors("Jane Smith", "Mathematics");
            Instructors instructor3 = new Instructors("Alice Johnson", "Physics");
            Instructors instructor4 = new Instructors("Bob Brown", "Chemistry");
            school.instructors.AddRange(new[] { instructor1, instructor2, instructor3, instructor4 });

            instructor1.AddCourse(course1);
            instructor2.AddCourse(course2);
            instructor3.AddCourse(course3);
            instructor3.AddCourse(course1);
            instructor4.AddCourse(course4);

          
            Students student1 = new Students(1, "Ahmed Ali", "ahmed@example.com");
            Students student2 = new Students(2, "Sara Mohamed", "sara@example.com");
            school.students.AddRange(new[] { student1, student2 });
            student1.EnrollInCourse(course1);
            student1.EnrollInCourse(course2);
            student2.EnrollInCourse(course1);

            //  text and MCQ nad f/f
            Questions question1 = new Questions(course1.CourseID, "What is a computer?", "text", "computer is .....", 5);
            Questions question2 = new Questions(course2.CourseID, "What is the Pythagorean theorem?", "text", "a^2 + b^2 = c^2", 5);
            Questions question3 = new Questions(course3.CourseID, "What is Newton's second law?", "text", "F = ma", 5);
            Questions question4 = new Questions(course4.CourseID, "What is the chemical formula for water?", "text", "H2O", 5);
            Questions question5 = new Questions(course1.CourseID, "Which is a programming language?", new List<string> { "Python", "Blue", "Red", "Green" }, 0, 5);
            Questions question6 = new Questions(course2.CourseID, "What is 2 + 2?", new List<string> { "3", "4", "5", "6" }, 1, 5);
            Questions question7 = new Questions(course3.CourseID, "Is the Sky blue?", new List<string> { "True", "False" }, 0, 2);

            course1.CoursesQuestions.AddRange(new[] { question1, question5 });
            course2.CoursesQuestions.AddRange(new[] { question2, question6 });
            course3.CoursesQuestions.Add(question3);
            course4.CoursesQuestions.Add(question4);

            Exams exam1 = new Exams("Midterm Exam", 50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), instructor1, course1);
            exam1.AssignQuestion(question1); // Text
            exam1.AssignQuestion(question5); // MCQ
            exam1.AssignQuestion(question7);// T/F
            course1.AddExam(exam1);

            Exams exam2 = new Exams("Final Exam", 50, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), instructor2, course2);
            exam2.AssignQuestion(question2); // Text
            exam2.AssignQuestion(question6); // MCQ
            exam2.AssignQuestion(question7);// T/F
            course2.AddExam(exam2);
            #endregion
           
            while (true)
            {
                Console.WriteLine("\n=== Examination System Menu ===");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Remove Course from Instructor");
                Console.WriteLine("3. Add Exam to Course");
                Console.WriteLine("4. Remove Exam from Course");
                Console.WriteLine("5. Edit Exam in Course");
                Console.WriteLine("6. Add Question to Exam");
                Console.WriteLine("7. Edit Question in Exam");
                Console.WriteLine("8. Display All Courses");
                Console.WriteLine("9. Take Exam");
                Console.WriteLine("10. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Add Course
                        Console.WriteLine("Enter course title:");
                        string title = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Course title cannot be empty.");
                            break;
                        }
                        Console.WriteLine("Enter course code:");
                        if (!int.TryParse(Console.ReadLine(), out int code))
                        {
                            Console.WriteLine("Invalid course code.");
                            break;
                        }
                        Console.WriteLine("Enter course description:");
                        string description = Console.ReadLine();
                        Courses newCourse = new Courses(title, code, description);
                        school.courses.Add(newCourse);
                        Console.WriteLine($"Course {newCourse.CourseTitle} added with ID {newCourse.CourseID}.");
                        break;

                    case "2": // Remove Course from Instructor
                        Console.WriteLine("\nAvailable Instructors:");
                        foreach (var ins in school.instructors)
                        {
                            Console.WriteLine($"ID: {ins.InstractorID}, Name: {ins.Name}");
                        }
                        Console.WriteLine("Enter Instructor ID to remove course from:");
                        if (!int.TryParse(Console.ReadLine(), out int insId))
                        {
                            Console.WriteLine("Invalid Instructor ID.");
                            break;
                        }
                        Instructors selectedInstructor = FindInstructorById(school.instructors, insId);
                        if (selectedInstructor == null)
                        {
                            Console.WriteLine("Instructor not found.");
                            break;
                        }
                        Console.WriteLine($"Courses taught by {selectedInstructor.Name}:");
                        foreach (var c in selectedInstructor.TeachCourses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter course ID to remove:");
                        if (!int.TryParse(Console.ReadLine(), out int courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        Courses courseToRemove = selectedInstructor.FindCourseById(courseId);
                        if (courseToRemove != null)
                        {
                            selectedInstructor.RemoveCourse(courseToRemove);
                            Console.WriteLine($"Course {courseToRemove.CourseTitle} removed from {selectedInstructor.Name}.");
                        }
                        else
                        {
                            Console.WriteLine("Course not found for this instructor.");
                        }
                        break;

                    case "3": // Add Exam to Course
                        Console.WriteLine("\nAvailable Instructors:");
                        foreach (var ins in school.instructors)
                        {
                            Console.WriteLine($"ID: {ins.InstractorID}, Name: {ins.Name}");
                        }
                        Console.WriteLine("Enter Instructor ID:");
                        if (!int.TryParse(Console.ReadLine(), out insId))
                        {
                            Console.WriteLine("Invalid Instructor ID.");
                            break;
                        }
                        selectedInstructor = FindInstructorById(school.instructors, insId);
                        if (selectedInstructor == null)
                        {
                            Console.WriteLine("Instructor not found.");
                            break;
                        }
                        Console.WriteLine($"Courses taught by {selectedInstructor.Name}:");
                        foreach (var c in selectedInstructor.TeachCourses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        Courses selectedCourse = selectedInstructor.FindCourseById(courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Course not found for this instructor.");
                            break;
                        }
                        Console.WriteLine("Enter exam title:");
                        string examTitle = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(examTitle))
                        {
                            Console.WriteLine("Exam title cannot be empty.");
                            break;
                        }
                        Console.WriteLine("Enter max degree:");
                        if (!int.TryParse(Console.ReadLine(), out int maxDegree) || maxDegree <= 0)
                        {
                            Console.WriteLine("Invalid max degree.");
                            break;
                        }
                        Console.WriteLine("Enter start time (yyyy-mm-dd hh:mm:ss):");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
                        {
                            Console.WriteLine("Invalid start time.");
                            break;
                        }
                        Console.WriteLine("Enter end time (yyyy-mm-dd hh:mm:ss):");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime) || endTime <= startTime)
                        {
                            Console.WriteLine("Invalid end time. It must be after start time.");
                            break;
                        }
                        Exams newExam = new Exams(examTitle, maxDegree, startTime, endTime, selectedInstructor, selectedCourse);
                        selectedInstructor.AddExamToCourse(selectedCourse, newExam);
                        Console.WriteLine($"Exam {newExam.ExamTitle} added to course {selectedCourse.CourseTitle}.");
                        break;

                    case "4": // Remove Exam from Course
                        Console.WriteLine("\nAvailable Courses:");
                        foreach (var c in school.courses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        selectedCourse = FindCourseById(school.courses, courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        Console.WriteLine($"Exams in {selectedCourse.CourseTitle}:");
                        foreach (var exam in selectedCourse.CoursesExams)
                        {
                            Console.WriteLine($"ID: {exam.ExamID}, Title: {exam.ExamTitle}");
                        }
                        Console.WriteLine("Enter Exam ID to remove:");
                        if (!int.TryParse(Console.ReadLine(), out int examId))
                        {
                            Console.WriteLine("Invalid Exam ID.");
                            break;
                        }
                        Exams examToRemove = selectedCourse.CoursesExams.Find(e => e.ExamID == examId);
                        if (examToRemove != null)
                        {
                            selectedCourse.RemoveExam(examToRemove);
                        }
                        else
                        {
                            Console.WriteLine("Exam not found in this course.");
                        }
                        break;

                    case "5": // Edit Exam in Course
                        Console.WriteLine("\nAvailable Courses:");
                        foreach (var c in school.courses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        selectedCourse = FindCourseById(school.courses, courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        Console.WriteLine($"Exams in {selectedCourse.CourseTitle}:");
                        foreach (var exam in selectedCourse.CoursesExams)
                        {
                            Console.WriteLine($"ID: {exam.ExamID}, Title: {exam.ExamTitle}");
                        }
                        Console.WriteLine("Enter Exam ID to edit:");
                        if (!int.TryParse(Console.ReadLine(), out examId))
                        {
                            Console.WriteLine("Invalid Exam ID.");
                            break;
                        }
                        Exams examToEdit = selectedCourse.CoursesExams.Find(e => e.ExamID == examId);
                        if (examToEdit != null)
                        {
                            examToEdit.EditExam();
                        }
                        else
                        {
                            Console.WriteLine("Exam not found in this course.");
                        }
                        break;

                    case "6": // Add Question to Exam
                        Console.WriteLine("\nAvailable Courses:");
                        foreach (var c in school.courses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        selectedCourse = FindCourseById(school.courses, courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        Console.WriteLine($"Exams in {selectedCourse.CourseTitle}:");
                        foreach (var exam in selectedCourse.CoursesExams)
                        {
                            Console.WriteLine($"ID: {exam.ExamID}, Title: {exam.ExamTitle}");
                        }
                        Console.WriteLine("Enter Exam ID:");
                        if (!int.TryParse(Console.ReadLine(), out examId))
                        {
                            Console.WriteLine("Invalid Exam ID.");
                            break;
                        }
                        Exams selectedExam = selectedCourse.CoursesExams.Find(e => e.ExamID == examId);
                        if (selectedExam == null)
                        {
                            Console.WriteLine("Exam not found in this course.");
                            break;
                        }
                        Console.WriteLine("Available Questions:");
                        foreach (var q in selectedCourse.CoursesQuestions)
                        {
                            Console.WriteLine($"ID: {q.QuestionID}, Text: {q.QuestionText}, Type: {q.QuestionType}");
                        }
                        Console.WriteLine("Enter Question ID to add:");
                        if (!int.TryParse(Console.ReadLine(), out int questionId))
                        {
                            Console.WriteLine("Invalid Question ID.");
                            break;
                        }
                        Questions questionToAdd = selectedCourse.FindQuestionById(questionId);
                        if (questionToAdd != null)
                        {
                            selectedExam.AssignQuestion(questionToAdd);
                        }
                        else
                        {
                            Console.WriteLine("Question not found in this course.");
                        }
                        break;

                    case "7": // Edit Question in Exam
                        Console.WriteLine("\nAvailable Courses:");
                        foreach (var c in school.courses)
                        {
                            Console.WriteLine($"ID: {c.CourseID}, Code: {c.CourseCode}, Title: {c.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        selectedCourse = FindCourseById(school.courses, courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        Console.WriteLine($"Exams in {selectedCourse.CourseTitle}:");
                        foreach (var exam in selectedCourse.CoursesExams)
                        {
                            Console.WriteLine($"ID: {exam.ExamID}, Title: {exam.ExamTitle}");
                        }
                        Console.WriteLine("Enter Exam ID:");
                        if (!int.TryParse(Console.ReadLine(), out examId))
                        {
                            Console.WriteLine("Invalid Exam ID.");
                            break;
                        }
                        selectedExam = selectedCourse.CoursesExams.Find(e => e.ExamID == examId);
                        if (selectedExam == null)
                        {
                            Console.WriteLine("Exam not found in this course.");
                            break;
                        }
                        Console.WriteLine($"Questions in {selectedExam.ExamTitle}:");
                        foreach (var q in selectedExam.ExamQuestions)
                        {
                            Console.WriteLine($"ID: {q.QuestionID}, Text: {q.QuestionText}, Type: {q.QuestionType}");
                        }
                        Console.WriteLine("Enter Question ID to edit:");
                        if (!int.TryParse(Console.ReadLine(), out questionId))
                        {
                            Console.WriteLine("Invalid Question ID.");
                            break;
                        }
                        Questions questionToEdit = selectedExam.ExamQuestions.Find(q => q.QuestionID == questionId);
                        if (questionToEdit != null)
                        {
                            questionToEdit.EditQuestion();
                        }
                        else
                        {
                            Console.WriteLine("Question not found in this exam.");
                        }
                        break;

                    case "8": // Display All Courses
                        Console.WriteLine("\nAll Courses:");
                        foreach (var course in school.courses)
                        {
                            course.DisplayCourseDetails();
                        }
                        break;
                  
                    case "9": // Take Exam
                        Console.WriteLine("\nAvailable Students:");
                        foreach (var student in school.students)
                        {
                            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
                        }
                        Console.WriteLine("Enter Student ID:");
                        if (!int.TryParse(Console.ReadLine(), out int studentId))
                        {
                            Console.WriteLine("Invalid Student ID.");
                            break;
                        }
                        Students selectedStudent = FindStudentById(school.students, studentId);
                        if (selectedStudent == null)
                        {
                            Console.WriteLine("Student not found.");
                            break;
                        }
                        Console.WriteLine($"Enrolled Courses for {selectedStudent.Name}:");
                        foreach (var course in selectedStudent.EnrolledCourses)
                        {
                            Console.WriteLine($"ID: {course.CourseID}, Code: {course.CourseCode}, Title: {course.CourseTitle}");
                        }
                        Console.WriteLine("Enter Course ID:");
                        if (!int.TryParse(Console.ReadLine(), out courseId))
                        {
                            Console.WriteLine("Invalid Course ID.");
                            break;
                        }
                        selectedCourse = selectedStudent.EnrolledCourses.Find(c => c.CourseID == courseId);
                        if (selectedCourse == null)
                        {
                            Console.WriteLine("Student is not enrolled in this course.");
                            break;
                        }
                        Console.WriteLine($"Exams in {selectedCourse.CourseTitle}:");
                        foreach (var exam in selectedCourse.CoursesExams)
                        {
                            Console.WriteLine($"ID: {exam.ExamID}, Title: {exam.ExamTitle}");
                        }
                        Console.WriteLine("Enter Exam ID:");
                        if (!int.TryParse(Console.ReadLine(), out examId))
                        {
                            Console.WriteLine("Invalid Exam ID.");
                            break;
                        }
                        selectedExam = selectedCourse.CoursesExams.Find(e => e.ExamID == examId);
                        if (selectedExam == null)
                        {
                            Console.WriteLine("Exam not found in this course.");
                            break;
                        }
                        selectedStudent.TakeExam(selectedExam);
                        break;

                    case "10": // Exit
                        Console.WriteLine("Exiting program...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}