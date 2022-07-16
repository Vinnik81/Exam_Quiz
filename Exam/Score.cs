namespace Exam
{
    public class Score
    {
        public string UserLogin { get; set; }
        public string QuizTitle { get; set; }
        public int RightAnswer { get; set; }
        public int Max { get; set; }
        public Score() {}

        public Score(string userLogin, Quiz quiz, int rightAnswer)
        {
            UserLogin = userLogin;
            QuizTitle = quiz.Title;
            RightAnswer = rightAnswer;
            Max = CountMax(quiz);
        }

        public int CountMax(Quiz quiz)
        {
            int max = 0;
            foreach (var question in quiz.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.IsCorect) max++;
                }
            }
            return max;
        }

        public override string ToString()
        {
            return $"{RightAnswer} из {Max} ({RightAnswer * 100 / Max} %)";
        }
    }
}
