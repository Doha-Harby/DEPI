using System;
using System.Collections.Generic;

namespace Examination_System.Models
{
    internal class Exams
    {
        private static int _examID = 1000;
        private string examTitle;

        public int ExamID { get; private set; }
        public string ExamTitle { get => examTitle; set => examTitle = value; }
        public int MaxDegree { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Instructors Instructor { get; set; }
        public Courses Course { get; set; }
        public List<Questions> ExamQuestions { get; set; } = new List<Questions>();

        public Exams(string title, int maxDegree, DateTime start, DateTime end, Instructors ins, Courses cor)
        {
            ExamID = _examID++;
            ExamTitle = title;
            MaxDegree = maxDegree;
            StartTime = start;
            EndTime = end;
            Instructor = ins;
            Course = cor;
        }

        public void AssignQuestion(Questions question)
        {
            if (question != null && question.CourseID == Course.CourseID)
            {
                ExamQuestions.Add(question);
                Console.WriteLine($"Question {question.QuestionID} assigned to Exam {ExamID}.");
            }
            else
            {
                Console.WriteLine("Invalid question or question does not belong to this course.");
            }
        }

        public void DisplayExamQuestions()
        {
            Console.WriteLine($"Exam {ExamTitle} Questions:");
            foreach (var q in ExamQuestions)
            {
                Console.WriteLine($"ID: {q.QuestionID}, Text: {q.QuestionText}, Type: {q.QuestionType}");
            }
        }

        public double TakeExam()
        {
            double totalScore = 0;
            Console.WriteLine($"\nStarting Exam: {ExamTitle} (Max Degree: {MaxDegree})");
            foreach (var question in ExamQuestions)
            {
                if (question.QuestionType != "mcq")
                {
                    Console.WriteLine($"Question {question.QuestionID} is a text question and will not be graded.");
                    continue;
                }

                Console.WriteLine($"\nQuestion {question.QuestionID}: {question.QuestionText}");
                question.Options.DisplayOptions();
                Console.Write("Select an option (1-based): ");
                if (int.TryParse(Console.ReadLine(), out int selectedOption) && selectedOption > 0 && selectedOption <= question.Options.Options.Count)
                {
                    double score = question.CheckAnswer(selectedOption - 1);
                    totalScore += score;
                    Console.WriteLine(score > 0 ? "Correct!" : "Incorrect.");
                }
                else
                {
                    Console.WriteLine("Invalid option selected. No marks awarded.");
                }
            }
            Console.WriteLine($"\nExam Finished. Total Score: {totalScore}/{MaxDegree}");
            return totalScore;
        }

        public void EditExam()
        {
            Console.WriteLine("Press the number to edit \n1: Title \n2: Max degree \n3: Start time \n4: End time");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter the new title:");
                    string newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        Console.WriteLine($"Title updated from {ExamTitle} to {newTitle}");
                        ExamTitle = newTitle;
                    }
                    else
                    {
                        Console.WriteLine("Title cannot be empty.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Enter the new max degree:");
                    if (int.TryParse(Console.ReadLine(), out int newMaxDegree) && newMaxDegree > 0)
                    {
                        Console.WriteLine($"Max degree updated from {MaxDegree} to {newMaxDegree}");
                        MaxDegree = newMaxDegree;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for max degree.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Enter the new start time (yyyy-mm-dd hh:mm:ss):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newStartTime))
                    {
                        Console.WriteLine($"Start time updated from {StartTime} to {newStartTime}");
                        StartTime = newStartTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for start time.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Enter the new end time (yyyy-mm-dd hh:mm:ss):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newEndTime) && newEndTime > StartTime)
                    {
                        Console.WriteLine($"End time updated from {EndTime} to {newEndTime}");
                        EndTime = newEndTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for end time or end time must be after start time.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}