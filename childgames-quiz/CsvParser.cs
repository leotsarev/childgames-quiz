
namespace ChildGamesQuiz
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.VisualBasic.FileIO;

    public class CsvParser
    {
        private readonly TextFieldParser textFieldParser;

        public CsvParser(TextReader reader)
        {
            textFieldParser = new TextFieldParser(reader) { TextFieldType = FieldType.Delimited };
            textFieldParser.SetDelimiters(",");
        }

        public IEnumerable<string[]> GetFields()
        {
            while (!this.textFieldParser.EndOfData)
            {
                var row = this.textFieldParser.ReadFields();
                if (row != null && row.Length != 0)
                {
                    yield return row;
                }
            }
        }
    }
}
