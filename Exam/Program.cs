using System;
using System.Collections.Generic;
using static Exam.Menu;

namespace Exam
{

    class Program
    {
        static void Main(string[] args)
        {
            string fileQuizes = @"../../../bin/Data/Quizzes.json";
            string fileUsers = @"../../Data/Users.json";
            string fileScores = @"../../Data/Scores.json";
            Reader<Users> usersReader = new JsonReader<Users>(fileUsers);
            Writer<Users> usersWriter = new JsonWriter<Users>(fileUsers);
            Reader<Scores> scoresReader = new JsonReader<Scores>(fileScores);
            Writer<Scores> scoresWriter = new JsonWriter<Scores>(fileScores);
            Reader<Quizes> quizesReader = new JsonReader<Quizes>(fileQuizes);

            UserManager userm = new UserManager(usersReader, usersWriter);
            QuizManager quizm = new QuizManager(quizesReader);
            ScoreManager scorem = new ScoreManager(scoresReader, scoresWriter);

            bool exit = false;
            int choice;
            do
            {
                Menu.DisplayRegMenu();
                choice = Menu.GetChoise();
                switch (choice)
                {
                    case 1: userm.DisplaySignIn(); break;
                    case 2: userm.DisplaySignUp(); break;
                }
            } while (choice < 1 || choice > 2);
            do
            {
                Menu.DisplayMainMenu();
                switch (Menu.GetChoise())
                {
                    case 1:
                        {
                            Menu.DisplayQuizMenu();
                            Console.WriteLine();
                            Console.Write("> Выбирите название викторины: ");
                            int choiceQuiz = Int32.Parse(Console.ReadLine());
                            QuizType choicenQuiz = (QuizType)choiceQuiz - 1;
                            string choicenTitle;
                            if (choicenQuiz == QuizType.Mixed) choicenTitle = "Смешанная викторина";
                            else
                            {
                                List<string> titles = quizm.GetQuizesTitles(choicenQuiz);

                                Menu.DisplayQuizesTitlesMenu(titles);
                                Console.WriteLine();
                                Console.Write("> Выберите тему: ");
                                int choiceTitle = Int32.Parse(Console.ReadLine());
                                choicenTitle = titles[choiceTitle - 1];
                            }
                            Score score = quizm.StartQuiz(choicenQuiz, choicenTitle, userm.CurUser);
                            scorem.AddScore(score);
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"Ваш результат: {score}");
                            Console.WriteLine();
                            Console.WriteLine("Место в топе: ");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("--------------------------");
                            Console.ResetColor();
                            scorem.DisplayUserScoreTop(choicenTitle, score);
                        }
                        break;
                    case 2: scorem.DisplayScoresUser(userm.CurUser.Login); break;
                    case 3:
                        {
                            Menu.DisplayQuizMenu();
                            Console.WriteLine();
                            Console.Write("> Выбирите раздел викторины:");
                            int choiceQuiz = Int32.Parse(Console.ReadLine());
                            List<string> titles = quizm.GetQuizesTitles((QuizType)choiceQuiz - 1);
                            Menu.DisplayQuizesTitlesMenu(titles);
                            Console.WriteLine();
                            Console.Write("> Выбирите тему:");
                            int choiceTitle = Int32.Parse(Console.ReadLine());
                            int topAmount = 20;
                            scorem.DisplayTopScores(topAmount, titles[choiceTitle - 1]);
                        }
                        break;
                    case 4:
                        Menu.DisplayChangeSetMenu();
                        switch (Menu.GetChoise())
                        {
                            case 1: userm.DisplayChangeBirthday(); break;
                            case 2: userm.DisplayChangePassword(); break;
                            default: Console.WriteLine(" Неверный символ!"); break;
                        }
                        break;
                    case 5: exit = true; break;
                    default: Console.WriteLine(" Неверный символ!"); break;
                }
                if (exit) break;
            } while (Menu.AllowContinue());
            Console.WriteLine("\n\nПрограмма завершена!");
        }
    }
}
