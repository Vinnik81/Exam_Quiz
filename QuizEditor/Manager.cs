using Exam;
using System.Collections.Generic;
using System.Linq;

namespace QuizEditor
{
    public class Manager
    {
        private Quizes _createdQuizes;
        private Reader<Quizes> _quizesReader;
        private Writer<Quizes> _quizesWriter;

        public Manager(Reader<Quizes> quizesReader, Writer<Quizes> quizesWriter)
        {
            _quizesReader = quizesReader;
            _quizesWriter = quizesWriter;
            _createdQuizes = _quizesReader.Read() == null ? new Quizes() : _quizesReader.Read();
        }

        public void AddQuiz(Quiz newQuiz)
        {
            _createdQuizes.Add(newQuiz);
            _quizesWriter.Write(_createdQuizes);
        }

        public bool CheckQuizExists(string title) => FindQuiz(title) != null;

        public Quiz FindQuiz(string title) => _createdQuizes.FirstOrDefault(quiz => quiz.Title == title);

        public void RemoveQuiz(string title)
        {
            Quiz removeQuiz = FindQuiz(title);
            _createdQuizes.Remove(removeQuiz);
            _quizesWriter.Write(_createdQuizes);
        }

        public List<string> GetQuizesTitles(QuizType type)
        {
            List<string> result = new List<string>();
            foreach (var quiz in _createdQuizes)
            {
                if (quiz.Type == type) result.Add(quiz.Title);
            }
            return result;
        }
    }
}
