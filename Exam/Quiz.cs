using System.Collections.Generic;

namespace Exam
{
    public class Quiz
    {
        public QuizType Type { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz() {}

        public Quiz(QuizType type, string title, List<Question> questions)
        {
            Type = type;
            Title = title;
            Questions = questions;
        }
    }
}
