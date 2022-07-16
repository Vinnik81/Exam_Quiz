using Exam;
using System;
using System.Collections.Generic;
using System.IO;
using static Exam.Menu;

namespace QuizEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileQuizes = @"\project\Exam\Exam\bin\Data\Quizzes.json";
            string fileUsers = @"../../Data/Users.json";
            Reader<Users> usersReader = new JsonReader<Users>(Path.GetFullPath(fileUsers));
            Writer<Users> usersWriter = new JsonWriter<Users>(Path.GetFullPath(fileUsers));
            Reader<Quizes> quizesReader = new JsonReader<Quizes>(fileQuizes);
            Writer<Quizes> quizesWriter = new JsonWriter<Quizes>(fileQuizes);
            UserManager userManager = new UserManager(usersReader, usersWriter);
            Manager manager = new Manager(quizesReader, quizesWriter);
            bool exit = false;
            int choice;
            do
            {
                Menu.DisplayRegMenu();
                choice = Menu.GetChoice();
                switch (choice)
                {
                    case 1: userManager.DisplaySignIn(); break;
                    case 2: userManager.DisplaySignUp(); break;
                }
            } while (choice < 1 || choice > 2);
            do
            {
                Menu.DisplayMainMenu();
                switch (Menu.GetChoice())
                {
                    case 1: manager.AddQuiz(Creator.CreateQuiz(manager)); break;
                    case 2:
                        {
                            Menu.DisplayQuizMenu();
                            Console.Write("> Выбирите викторину: ");
                            int choiceQuiz = Int32.Parse(Console.ReadLine());
                            QuizType type = (QuizType)choiceQuiz - 1;
                            List<string> titles = manager.GetQuizesTitles(type);
                            Menu.DisplayQuizesTitlesMenu(titles);
                            Console.WriteLine();
                            Console.Write("> Выбирите тему: ");
                            int choiceTitle = Int32.Parse(Console.ReadLine());
                            string choicenTitle = titles[choiceTitle - 1];
                            Quiz newQuiz = Editor.EditQuiz(manager.FindQuiz(choicenTitle));
                            manager.RemoveQuiz(choicenTitle);
                            manager.AddQuiz(newQuiz);
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"Викторина \"{choicenTitle}\" успешно изменена!");
                        } break;
                    case 3:
                        {
                            Menu.DisplayQuizMenu();
                            Console.Write(">  Выбирите викторину: ");
                            int choiceQuiz = Int32.Parse(Console.ReadLine());
                            QuizType type = (QuizType)choiceQuiz - 1;
                            List<string> titles = manager.GetQuizesTitles(type);
                            Menu.DisplayQuizesTitlesMenu(titles);
                            Console.WriteLine();
                            Console.Write(">  Выбирите тему: ");
                            int choiceTitle = Int32.Parse(Console.ReadLine());
                            string choicenTitle = titles[choiceTitle - 1];
                            manager.RemoveQuiz(choicenTitle);
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"Викторина \"{choicenTitle}\" успешно удалена!");
                        } break;
                    case 4: exit = true; break;
                    default: Console.WriteLine(" Неверный символ!"); break;
                }
                if (exit) break;
            } while (Menu.AllowContinue());
            Console.WriteLine("\n\nПрограмма завершина!");
        }
    }
}
