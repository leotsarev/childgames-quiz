// -----------------------------------------------------------------------
// <copyright file="Question.cs" company="Leonid Tcarev">
// © 2013 Leonid Tcarev, Apache 2.0
// </copyright>
// -----------------------------------------------------------------------

namespace ChildGamesQuiz
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Implements Quiz question
    /// </summary>
    public class Question
    {
        public string Picture { get; private set; }

        private ICollection<Regex> CorrectAnswersMask { get; set; }

        public bool Check(string text)
        {
          text = text.ToUpperInvariant();
          return CorrectAnswersMask.Any(re => re.IsMatch(text));
        }

        private static string MaskToRegex(string mask)
        {
          return mask.ToUpperInvariant().Replace(".", "[.]").Replace("*", ".*").Replace("?", ".");
        }

        public static Question CreateFromStrings(IList<string> row)
        {
          return new Question
          {
            Picture = row[0],
            CorrectAnswersMask = row.Skip(1).Select(mask => new Regex(MaskToRegex(mask))).ToArray()
          };
        }
    }
}
