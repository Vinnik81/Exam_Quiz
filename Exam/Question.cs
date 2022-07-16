using System.Collections.Generic;

namespace Exam
{
    public class Question
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

        public Question() {}

        public Question(string text, List<Answer> answers)
        {
            Text = text;
            Answers = answers;
        }
    }
}
