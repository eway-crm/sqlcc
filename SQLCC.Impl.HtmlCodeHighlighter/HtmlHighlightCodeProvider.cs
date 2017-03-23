using System.Collections.Generic;
using System.Text;
using SQLCC.Core;
using SQLCC.Core.Objects;
using System;

namespace SQLCC.Impl.HtmlCodeHighlighter
{
    public class HtmlHighlightCodeProvider : HighlightCodeProvider
    {
        private const string InsertStartTag = "<span class=\"cc\">";
        private const string InsertEndTag = "</span>";

        public override string HighlightCode(string code, List<DbCodeSegment> codeSegments)
        {
            if (codeSegments.Count == 0)
                return code;
            var codeContents = new StringBuilder(code);
            var offset = 0;
            try
            {
                foreach (var lineOfCode in codeSegments)
                {
                    codeContents.Insert(lineOfCode.StartByte + offset, InsertStartTag);
                    offset += InsertStartTag.Length;
                    codeContents.Insert(lineOfCode.EndByte + offset, InsertEndTag);
                    offset += InsertEndTag.Length;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Unable to highlight code in '{0}', the exception is: {1}", code, ex);

                return code;
            }
            return codeContents.ToString();
        }
    }
}
