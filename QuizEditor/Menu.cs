using System;
using System.Collections.Generic;

namespace QuizEditor
{
    public static class Menu
    {
        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Создать новую викторину");
            Console.WriteLine("\t\t\t2 - Редактировать викторину");
            Console.WriteLine("\t\t\t3 - Удалить викторину");
            Console.WriteLine("\t\t\t4 - Выход");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static int GetChoice()
        {
            Console.Write("\n> Выбирите нужное действие: ");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            return choice;
        }

        public static bool AllowContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n> Продолжить (y/n)? - ");
            Console.ResetColor();
            string answer = Console.ReadLine();
            return answer == "y" ? true : false;
        }

        public static void DisplayRegMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\t\t **Добро пожаловать в приложение для редактирования викторин!** ");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Войти");
            Console.WriteLine("\t\t\t2 - Зарегистрироваться");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static void DisplayQuizMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - История");
            Console.WriteLine("\t\t\t2 - Физика");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static void DisplayQuizesTitlesMenu(List<string> titles)
        {
            Console.Clear();
            Console.WriteLine();
            int number = 1;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var title in titles)
            {
                Console.WriteLine($"\t  {number} - {title}");
                number++;
            }
            Console.ResetColor();
        }

        public static void DisplayEditMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Добавить вопрос");
            Console.WriteLine("\t\t\t2 - Удалить вопрос");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }
    }
}
