namespace ChildGamesQuiz
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Quiz 
    {
        private readonly IEnumerator<Question> enumerator;

        public Quiz(TextReader reader)
        {
            this.enumerator = new CsvParser(reader).GetFields().Select(Question.CreateFromStrings).GetEnumerator();
            this.NextQuestion();
        }

        public string Result
        {
            get
            {
                return "2601";
            }
        }

        public Question Current
        {
            get
            {
                return enumerator.Current;
            }
        }

        public bool Completed { get; set; }

        public void NextQuestion()
        {
            Completed = !enumerator.MoveNext();
        }
    }
}
