namespace Exam
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorect { get; set; }

        public Answer() {}

        public Answer(string text, bool isCorect)
        {
            Text = text;
            IsCorect = isCorect;
        }
    }
}
