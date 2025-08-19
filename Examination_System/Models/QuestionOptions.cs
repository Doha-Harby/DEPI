using System;
using System.Collections.Generic;

namespace Examination_System.Models
{
    internal class QuestionOptions
    {
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; } 

        public QuestionOptions(List<string> options, int correctAnswerIndex)
        {
            if (options == null || options.Count < 2)
                throw new ArgumentException("At least two options are required for a multiple-choice question.");
            if (correctAnswerIndex < 0 || correctAnswerIndex >= options.Count)
                throw new ArgumentException("Invalid correct answer index.");

            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public void DisplayOptions()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }

        public bool IsCorrectAnswer(int selectedIndex)
        {
            return selectedIndex == CorrectAnswerIndex;
        }
    }
}