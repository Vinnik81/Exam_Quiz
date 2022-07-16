using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    public class Scores: List<Score>
    {
        public Scores() {}

        public bool CheckScoreExists(string userLogin, string quizTitle) => FindScore(userLogin, quizTitle) != null;

        public Score FindScore(string userLogin, string quizTitle)
        {
            return this.FirstOrDefault(score => score.UserLogin == userLogin && score.QuizTitle == quizTitle);
        }
    }
}
