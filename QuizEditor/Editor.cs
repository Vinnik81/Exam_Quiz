using Exam;
using System;
using System.Collections.Generic;

namespace QuizEditor
{
    public static class Editor
    {
        public static Quiz EditQuiz(Quiz quiz)
        {
            List<Question> newQuestions = quiz.Questions;
            Console.Clear();
            Menu.DisplayEditMenu();
            switch (Menu.GetChoice())
            {
                case 1:
                    Question newQuestion = Creator.CreateQuestion();
                    newQuestions.Add(newQuestion);
                    break;
                case 2:
                    for (int i = 0; i < quiz.Questions.Count; i++)
                    {
                        Question question = quiz.Questions[i];
                        Console.WriteLine($" {i + 1} - {question.Text}");
                    }
                    Console.WriteLine();
                    Console.Write("\n> Выбирите вопрос который нужно удалить: ");
                    int choiceQuestion = Int32.Parse(Console.ReadLine());
                    Question choicenQuestion = quiz.Questions[choiceQuestion - 1];
                    newQuestions.Remove(choicenQuestion);
                    break;
                default:
                    Console.WriteLine("Неверный символ!");
                    break;
            }
            return new Quiz(quiz.Type, quiz.Title, newQuestions);
        }
    }
}
