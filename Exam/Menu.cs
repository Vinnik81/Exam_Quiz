using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    public static class Menu
    {
        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Начать новую викторину");
            Console.WriteLine("\t\t\t2 - Посмотреть результаты своих викторин");
            Console.WriteLine("\t\t\t3 - Посмотреть ТОП-20 по конкретной викторине");
            Console.WriteLine("\t\t\t4 - Изменить настройки");
            Console.WriteLine("\t\t\t5 - Выход");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static int GetChoise()
        {
            Console.WriteLine("\n> Выберите нужное действие: ");
            int choice; 
            int.TryParse(Console.ReadLine(), out choice);
            return choice;
        }

        public static bool AllowContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n> Продолжить (y/n)? ");
            Console.ResetColor();
            string answer = Console.ReadLine();
            return answer == "y" ? true : false;
        }

        public static void DisplayRegMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t Добро пожаловать в приложение **\"Викторина\"**");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Войти");
            Console.WriteLine("\t\t\t2 - Зарегестрироваться");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static void DisplayQuizMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - История");
            Console.WriteLine("\t\t\t2 - Физика");
            Console.WriteLine("\t\t\t3 - Смешанная викторина");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static void DisplayChangeSetMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
            Console.WriteLine("\t\t\t1 - Изменить дату рождения");
            Console.WriteLine("\t\t\t2 - Изменить пароль");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public static void DisplayQuizesTitlesMenu(List<string> titles)
        {
            Console.Clear();
            Console.WriteLine();
            int num = 1;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t#############################################################");
            foreach (var title in titles)
            {
                Console.WriteLine($"\t\tТема: \t {num} - {title}");
                num++;
            }
            Console.WriteLine("\t\t#############################################################");
            Console.ResetColor();
        }

        public class QuizManager
        {
            private Reader<Quizes> _quizesReader;
            private Quizes _quizes;

            public QuizManager(Reader<Quizes> quizesReader)
            {
                _quizesReader = quizesReader;
                _quizes = _quizesReader.Read() == null ? new Quizes() : _quizesReader.Read();
                int numQuestions = GetAllQuestions().Count < 20 ? GetAllQuestions().Count : 20;
                _quizes.Add(GetMixedQuiz(numQuestions));
            }

            public Score StartQuiz(QuizType quizType, string title, User curUser)
            {
                foreach (var item in _quizes )
                {
                    Console.WriteLine($"{item.Type} - {item.Title}");
                }
                Console.WriteLine("#################################################");
                Console.WriteLine($"{quizType} - {title}");
                Quiz curQuiz = _quizes.Find(quiz => quiz.Type == quizType && quiz.Title == title);
                List<Question> questions = curQuiz.Questions;
                int countRightAnswers = 0;
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"\"{curQuiz.Title}\"");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Если правильных ответов несколько, введите их через пробелы");
                    Console.ResetColor();
                    Question question = questions[i];
                    Console.WriteLine();
                    Console.WriteLine($" {i + 1} - {question.Text}");
                    Console.WriteLine();
                    List<Answer> answers = question.Answers;
                    for (int j = 0; j < answers.Count; j++)
                    {
                        Answer answer = answers[j];
                        Console.WriteLine($" {j + 1} - {answer.Text}");
                    }
                    Console.WriteLine();
                    Console.Write("> ");
                    List<int> userAnswers = Console.ReadLine().Split(' ').Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => int.Parse(a)).ToList();
                    countRightAnswers += userAnswers.FindAll(a => answers[a - 1].IsCorect).Count;
                }
                return new Score(curUser.Login, curQuiz, countRightAnswers);
            }
            private Quiz GetMixedQuiz(int numQuestions) => new(QuizType.Mixed, "Смешанная викторина", GetRandomQuestions(numQuestions));
           

            public List<Question> GetRandomQuestions(int num)
            {
                List<Question> result = new List<Question>();
                List<Question> allQuestions = GetAllQuestions();
                Random random = new Random();
                for (int i = 0; i < num; i++)
                {
                    int randInd = random.Next(0, allQuestions.Count - 1);
                    Question randQuestion = allQuestions[randInd];
                    result.Add(randQuestion);
                }
                return result;
            }

            public List<Question> GetAllQuestions()
            {
               List<Question> result = new List<Question>();
                foreach (var quiz in _quizes)
                {
                    foreach (var question in quiz.Questions)
                    {
                        result.Add(question);
                    }
                }
                return result;
            }

            public List<string> GetQuizesTitles(QuizType type)
            {
                List<string> result = new List<string>();
                foreach (var quiz in _quizes)
                {
                    if (quiz.Type == type) result.Add(quiz.Title);
                }
                return result;
            }
        }

        public class ScoreManager
        {
            private Reader<Scores> _scoresReader;
            private Writer<Scores> _scoresWriter;
            private Scores _scores;

            public ScoreManager(Reader<Scores> scoresReader, Writer<Scores> scoresWriter)
            {
                _scoresReader = scoresReader;
                _scoresWriter = scoresWriter;
                _scores = _scoresReader.Read() == null ? new Scores() : _scoresReader.Read();
            }

            public void AddScore(Score score)
            {
                if (_scores.CheckScoreExists(score.UserLogin, score.QuizTitle))
                    _scores.Remove(_scores.FindScore(score.UserLogin, score.QuizTitle));
                _scores.Add(score);
                _scoresWriter.Write(_scores);
            }

            public List<Score> GetTop(string quizTitle)
            {
                List<Score> scoresQuiz = _scores.FindAll((s) => s.QuizTitle == quizTitle);
                List<Score> topScores = scoresQuiz.OrderByDescending((s) => s.RightAnswer).ToList();
                return topScores;
            }

            public List<Score> GetScoresUser(string login) => _scores.FindAll((s) => s.UserLogin == login);

            public void DisplayScoresUser(string login)
            {
                Console.Clear();
                Console.WriteLine();
                List<Score> scoresUser = GetScoresUser(login);
                if (scoresUser.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Нет результатов!");
                    Console.WriteLine("Вы не прошли ни одной викторины.");
                }
                else
                {
                    foreach (var score in scoresUser) Console.WriteLine($" {score.QuizTitle} - {score}");
                }
            }

            public void DisplayTopScores(int topAmount, string quizTitle)
            {
                Console.Clear();
                Console.WriteLine();
                List<Score> topScores = GetTop(quizTitle);
                if (topScores.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Нет результатов по данной викторине!");
                }
                else
                {
                    topAmount = topAmount > topScores.Count ? topScores.Count : topAmount;
                    for (int i = 0; i < topAmount; i++)
                    {
                        Score score = topScores[i];
                        Console.WriteLine($"{i + 1} - {score.UserLogin} - {score}");
                    }
                }
            }

            public void DisplayUserScoreTop(string quizTitle, Score userScore)
            {
                Console.WriteLine();
                List<Score> topScores = GetTop(quizTitle);
                int indUserScore = topScores.IndexOf(userScore);
                int delta = 3;
                int start = indUserScore > delta ? indUserScore - delta : 0;
                int end = indUserScore < topScores.Count - delta - 1 ? indUserScore + delta : topScores.Count - 1;
                for (int i = start; i <= end; i++)
                {
                    Score score = topScores[i];
                    if (score.UserLogin == userScore.UserLogin)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($" {i + 1} - {score.UserLogin} - {score}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($" {i + 1} - {score.UserLogin} - {score}");
                }
            }
        }

        public class UserManager
        {
            private Reader<Users> _usersReader;
            private Writer<Users> _usersWriter;
            public Users Users { get; set; }
            public User CurUser { get; set; }

            public UserManager(Reader<Users> usersReader, Writer<Users> usersWriter)
            {
                _usersReader = usersReader;
                _usersWriter = usersWriter;
                Users = _usersReader.Read() == null ? new Users() : _usersReader.Read();
            }

            public void DisplaySignUp()
            {
                string login, password, birthday = "";
                bool isSignUp = true;
                do
                {
                    Console.Clear();
                    if (!isSignUp)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Пользователь с таким логином уже существует!");
                        Console.ResetColor();
                        Console.WriteLine("Попробуй снова.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("> Введите дату рождения(yy-mm-dd): ");
                        birthday = Console.ReadLine();
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Придумай логин и пароль");
                    Console.ResetColor();
                    Console.Write("> Логин: ");
                    login = Console.ReadLine();
                    Console.Write("> Пароль: ");
                    password = Console.ReadLine();
                    isSignUp = Users.SingUp(login, password, birthday);
                } while (!isSignUp);
                _usersWriter.Write(Users);
                CurUser = Users.FindUser(login);
            }

            public void DisplaySignIn()
            {
                string login, password;
                bool isSignIn = true;
                do
                {
                    Console.Clear();
                    if (!isSignIn)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный логин или пароль!");
                        Console.ResetColor();
                        Console.WriteLine("Попробуй снова");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Введите логин и  пароль для входа");
                    Console.ResetColor();
                    Console.Write("> Логин: ");
                    login = Console.ReadLine();
                    Console.Write("> Пароль: ");
                    password = Console.ReadLine();
                    isSignIn = Users.SingIn(login, password);
                } while (!isSignIn);
                CurUser = Users.FindUser(login);
            }

            public void DisplayChangePassword()
            {
                string newPassword, password;
                bool passwordCorrect = true;
                do
                {
                    Console.Clear();
                    if (!passwordCorrect)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный пароль!");
                        Console.ResetColor();
                        Console.WriteLine("Попробуй снова.");
                    }
                    Console.WriteLine();
                    Console.Write("> Введите ваш старый пароль: ");
                    password = Console.ReadLine();
                    passwordCorrect = Users.CheckPassword(CurUser.Login, password);
                } while (!passwordCorrect);
                Console.Write("> Введите новый пароль: ");
                newPassword = Console.ReadLine();
                Users.ChangeUserPassword(CurUser.Login, newPassword);
                _usersWriter.Write(Users);
                CurUser = Users.FindUser(CurUser.Login);
            }

            public void DisplayChangeBirthday()
            {
                string newBirthday;
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Текущая дата рождения: {CurUser.Birthday.ToShortDateString()}");
                Console.ResetColor();
                Console.Write("> Введите новую дату рождения(yy-mm-dd): ");
                newBirthday = Console.ReadLine();
                Users.ChangeUserBirthday(CurUser.Login, newBirthday);
                _usersWriter.Write(Users);
                CurUser = Users.FindUser(CurUser.Login);
            }
        }
    }
}
