using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public static class SyntaxHighlighter
{
    #region private Dictionary<string, string> KeywordColors;
    private static readonly Dictionary<string, string> KeywordColors = new Dictionary<string, string>
    {
        {"int ", "<color=#569CD6>"},
        {"void ", "<color=#569CD6>"},
        {"char ", "<color=#569CD6>"},
        {"bool ", "<color=#569CD6>"},
        {"float ", "<color=#569CD6>"},
        {"unsigned ", "<color=#569CD6>"},
        {"long ", "<color=#569CD6>"},
        {"typename ", "<color=#569CD6>"},
        {"typedef ", "<color=#569CD6>"},
        {"class ", "<color=#569CD6>"},
        {"template ", "<color=#569CD6>"},
        {"const ", "<color=#569CD6>"},
        {"string ", "<color=#569CD6>"},
        {"virtual ", "<color=#569CD6>"},
        {"override ", "<color=#569CD6>"},

        {"enable_if_t ", "<color=#569CD6>"},
        {"unordered_map ", "<color=#569CD6>"},
        {"vector ", "<color=#569CD6>"},
        {"pair ", "<color=#569CD6>"},
        {"map ", "<color=#569CD6>"},
        {"queue ", "<color=#569CD6>"},
        {"array ", "<color=#569CD6>"},
        {"tuple ", "<color=#569CD6>"},
        {"mutex ", "<color=#569CD6>"},

        {"if ", "<color=#C586C0>"},
        {"for ", "<color=#C586C0>"},
        {"while ", "<color=#C586C0>"},
        {"switch ", "<color=#C586C0>"},
        {"do ", "<color=#C586C0>"},
        {"return ", "<color=#C586C0>"},

        {"namespace ", "<color=#569CD6>"},
        {"static_cast ", "<color=#569CD6>"},
        {"static_assert ", "<color=#569CD6>"},
    };
    #endregion

    private static readonly Regex keywordRegex;
    private static readonly string regexPattern;

    static SyntaxHighlighter()
    {
        var sortedKeywords = KeywordColors.Keys
            .OrderByDescending(k => k.Length)
            .Select(Regex.Escape)
            .ToArray();

        regexPattern = @"\b(" + string.Join("|", sortedKeywords) + @")\b";
        keywordRegex = new Regex(regexPattern, RegexOptions.Compiled);
    }

    public static string Highlight(string text)
    {
        return keywordRegex.Replace(text, match =>
        {
            string keyword = match.Value;
            return $"{KeywordColors[keyword]}{keyword}</color>";
        });
    }
}
