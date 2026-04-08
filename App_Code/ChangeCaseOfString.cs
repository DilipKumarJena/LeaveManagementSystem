using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;

/// <summary>
/// This class will help you to convert your string into the case type
/// it has all types which are there in Word.
/// </summary>
public class ChangeCaseOfString
{
    #region ~ Private Member Variables ~
    private static ChangeCaseType caseType = ChangeCaseType.Sentence;
    #endregion
    /// <summary>
    /// Enumerator for the list of case types avaliable in MS-Word
    /// </summary>
    public enum ChangeCaseType
    {
        Sentence,
        lower,
        UPPER,
        Title,
        tOGGLE
    }

    #region ~ ctor/dctor ~
    /// <summary>
    /// It will take case type by its name...and will choose the 
    /// appropriate value for that
    /// </summary>
    /// <param name="CaseType">Name of the type</param>
    public ChangeCaseOfString(string CaseType)
    {
        caseType = (ChangeCaseType)Enum.Parse(typeof(ChangeCaseType), CaseType);
    }

    /// <summary>
    /// It will take the type
    /// </summary>
    /// <param name="CaseType">Type of Case</param>
    public ChangeCaseOfString(ChangeCaseType CaseType)
    {
        caseType = CaseType;
    }
    #endregion


    #region ~ Public Member Functions ~
    /// <summary>
    /// It will convert the value to its appropriate type
    /// </summary>
    /// <param name="Value">String Value Input</param>
    /// <returns>String converted case value</returns>
    public static string ConvertTextCase(string StringValue, ChangeCaseType CaseType)
    {
        caseType = (ChangeCaseType)Enum.Parse(typeof(ChangeCaseType), CaseType.ToString());
        switch (ChangeCaseOfString.caseType)
        {
            case ChangeCaseType.Sentence:
                return ChangeCaseOfString.SentenceCase(StringValue);
            case ChangeCaseType.lower:
                return new CultureInfo("en-Us").TextInfo.ToLower(StringValue);
            case ChangeCaseType.UPPER:
                return new CultureInfo("en-Us").TextInfo.ToUpper(StringValue);
            case ChangeCaseType.Title:
                return new CultureInfo("en-Us").TextInfo.ToTitleCase(StringValue);
            case ChangeCaseType.tOGGLE:
                return ToggleCase(StringValue);
        }
        return string.Empty;
    }
    #endregion

    #region ~ Private Member Functions ~
    private static string ToggleCase(string Value)
    {
        Regex r = new Regex(@"\b(\w)(\w+)?\b", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        MatchEvaluator matchEval = new MatchEvaluator(replaceToToggle);

        return r.Replace(Value, matchEval);
    }
    private static string SentenceCase(string Value)
    {
        Regex r = new Regex(@"\b(\w)((.*?\.)|(.*))", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        MatchEvaluator matchEval = new MatchEvaluator(replaceToSentence);

        return r.Replace(Value, matchEval);
    }
    private static string replaceToToggle(Match m)
    {
        string FirstLetter;
        string Word;
        Word = m.Groups[2].Value;
        FirstLetter = m.Groups[1].Value.ToLower();
        return FirstLetter + Word.ToUpper();
    }
    private static string replaceToSentence(Match m)
    {
        string FirstLetter;
        string Word;
        Word = m.Groups[2].Value;
        FirstLetter = m.Groups[1].Value.ToUpper();
        return FirstLetter + Word.ToLower() + ".";
    }
    #endregion
}
