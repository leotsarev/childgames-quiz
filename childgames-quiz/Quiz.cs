namespace ChildGamesQuiz
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  public class Quiz
  {
    private readonly IEnumerator<Question> enumerator;

    public Quiz(TextReader reader, string result)
    {
      Result = result;
      enumerator = new CsvParser(reader).GetFields().Select(Question.CreateFromStrings).GetEnumerator();
      NextQuestion();

    }

    public string Result { get; private set; }

    public Question Current
    {
      get { return enumerator.Current; }
    }

    public bool Completed { get; private set; }

    public void NextQuestion()
    {
      Completed = !enumerator.MoveNext();
    }
  }
}
