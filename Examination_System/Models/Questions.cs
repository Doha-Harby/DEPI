using System;
using System.Collections.Generic;

namespace Examination_System.Models
{
    internal class Questions
    {
        private static int _questionCounter = 10000;
        public int QuestionID { get; private set; }
        public int CourseID { get; private set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } // text  mcq t/f
        public string Answer { get; set; } // text q
        public double Mark { get; set; }
        public QuestionOptions Options { get; set; } // mcq q

        //text q
        public Questions(int courseId, string questionText, string questionType, string answer, double mark)
        {
            QuestionID = _questionCounter++;
            CourseID = courseId;
            QuestionText = questionText;
            QuestionType = questionType;
            Answer = answer;
            Mark = mark;
        }

        // mcq q
        public Questions(int courseId, string questionText, List<string> options, int correctAnswerIndex, double mark)
        {
            QuestionID = _questionCounter++;
            CourseID = courseId;
            QuestionText = questionText;
            QuestionType = "mcq";
            Options = new QuestionOptions(options, correctAnswerIndex);
            Mark = mark;
        }

        public void EditQuestion()
        {
            Console.WriteLine($"Editing Question ID: {QuestionID}");
            Console.WriteLine("Press the number to edit \n1: Question text \n2: Question type \n3: Answer/Options \n4: Mark");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter the new question text:");
                    string newQuestionText = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newQuestionText))
                    {
                        Console.WriteLine($"Question updated from '{QuestionText}' to '{newQuestionText}'");
                        QuestionText = newQuestionText;
                    }
                    else
                    {
                        Console.WriteLine("Question text cannot be empty.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Enter the new question type (text/mcq):");
                    string newQuestionType = Console.ReadLine()?.ToLower();
                    if (newQuestionType == "text" || newQuestionType == "mcq")
                    {
                        if (newQuestionType == QuestionType)
                        {
                            Console.WriteLine("Question type unchanged.");
                        }
                        else if (newQuestionType == "text" && QuestionType == "mcq")
                        {
                            Console.WriteLine("Enter the new answer for text question:");
                            string newAnswer = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newAnswer))
                            {
                                QuestionType = newQuestionType;
                                Answer = newAnswer;
                                Options = null;
                                Console.WriteLine("Question type changed to text and answer updated.");
                            }
                            else
                            {
                                Console.WriteLine("Answer cannot be empty.");
                            }
                        }
                        else if (newQuestionType == "mcq" && QuestionType == "text")
                        {
                            Console.WriteLine("Enter number of options (at least 2):");
                            if (int.TryParse(Console.ReadLine(), out int numOptions) && numOptions >= 2)
                            {
                                List<string> options = new List<string>();
                                for (int i = 0; i < numOptions; i++)
                                {
                                    Console.WriteLine($"Enter option {i + 1}:");
                                    string option = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(option))
                                        options.Add(option);
                                    else
                                    {
                                        Console.WriteLine("Option cannot be empty. Try again.");
                                        return;
                                    }
                                }
                                Console.WriteLine("Enter the correct option number (1-based):");
                                if (int.TryParse(Console.ReadLine(), out int correctIndex) && correctIndex > 0 && correctIndex <= numOptions)
                                {
                                    QuestionType = newQuestionType;
                                    Answer = null;
                                    Options = new QuestionOptions(options, correctIndex - 1);
                                    Console.WriteLine("Question type changed to MCQ and options updated.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid correct option number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of options.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid question type. Use 'text' or 'mcq'.");
                    }
                    break;
                case "3":
                    if (QuestionType == "text")
                    {
                        Console.WriteLine("Enter the new answer:");
                        string newAnswer = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newAnswer))
                        {
                            Console.WriteLine($"Answer updated from '{Answer}' to '{newAnswer}'");
                            Answer = newAnswer;
                        }
                        else
                        {
                            Console.WriteLine("Answer cannot be empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Current options:");
                        Options.DisplayOptions();
                        Console.WriteLine("Enter number of new options (at least 2):");
                        if (int.TryParse(Console.ReadLine(), out int numOptions) && numOptions >= 2)
                        {
                            List<string> options = new List<string>();
                            for (int i = 0; i < numOptions; i++)
                            {
                                Console.WriteLine($"Enter option {i + 1}:");
                                string option = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(option))
                                    options.Add(option);
                                else
                                {
                                    Console.WriteLine("Option cannot be empty. Try again.");
                                    return;
                                }
                            }
                            Console.WriteLine("Enter the correct option number (1-based):");
                            if (int.TryParse(Console.ReadLine(), out int correctIndex) && correctIndex > 0 && correctIndex <= numOptions)
                            {
                                Options = new QuestionOptions(options, correctIndex - 1);
                                Console.WriteLine("Options updated.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid correct option number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of options.");
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("Enter the new mark:");
                    if (double.TryParse(Console.ReadLine(), out double newMark) && newMark >= 0 && newMark <= Courses.MaxDgree)
                    {
                        Console.WriteLine($"Mark updated from {Mark} to {newMark}");
                        Mark = newMark;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid mark. It must be between 0 and {Courses.MaxDgree}.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void DisplayQuestionDetails()
        {
            Console.WriteLine($"Question ID: {QuestionID}");
            Console.WriteLine($"Course ID: {CourseID}");
            Console.WriteLine($"Question Text: {QuestionText}");
            Console.WriteLine($"Question Type: {QuestionType}");
            if (QuestionType == "text")
            {
                Console.WriteLine($"Answer: {Answer}");
            }
            else
            {
                Console.WriteLine("Options:");
                Options.DisplayOptions();
                Console.WriteLine($"Correct Answer: {Options.Options[Options.CorrectAnswerIndex]}");
            }
            Console.WriteLine($"Mark: {Mark}");
        }

        public double CheckAnswer(int selectedOptionIndex)
        {
            if (QuestionType != "mcq")
                return 0; // Text not graded
            return Options.IsCorrectAnswer(selectedOptionIndex) ? Mark : 0;
        }
    }
}