using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ChangeTech.Silverlight.Common
{
    public static class ExpressionParseUtility
    {
        private static readonly string _REGEXP_IF = "^[ ]* (?:IF [ ]+ (?<ifCondition>.+?) [ ]+){0,1}  (?<ifExpression>(?:SET|GOTO|GOSUB|GOWEB|Round|EndPage) [ ]* (?:.(?!ELSE|IF))* [^\\ ])  (?: [ ]+ ELSE [ ]+ (?<elseExpression>(?:SET|GOTO|GOSUB|GOWEB|Round|EndPage) [ ]* (?:.(?!ELSE|IF))* [^\\ ])){0,1}";
        private static readonly string _REGEXP_CONDITION = "[ ]* (?<expressionA>.+?) [ ]* (?<operator><(?!=)|<=|>(?!=)|>=|==) [ ]* (?<expressionB>.+?) [ ]* $";
        private static readonly string _REGEXP_GRAPH_DATA_EXPRESSION = "[ ]* (?<expressionA>.+?) [ ]* $";
        private static readonly string _REGEXP_BAD_EQUAL_FORMATE = "[^><=!] = [^><=]";
        private static readonly string _REGEXP_BRACKET = "(?<functionName>GetIndex){0,1}\\( (?<expression>[^\\(\\)]*)\\)";
        //private static readonly string _REGEXP_BRACKET_UNMATCH = "[\\)\\(]";
        private static readonly string _REGEXP_MULTI_DIVIDE = "(?<expressionA> [^\\*\\+-\\/]+?) [ ]* (?<operator>[\\*\\/]) [ ]* (?<expressionB>-{0,1}[^\\*\\+-\\/]+)";
        //private static readonly string _REGEXP_MULTI_DIVIDE_UNMATCH = "[\\*\\/]";
        private static readonly string _REGEXP_ADD_MINUS = "^ [ ]* (?<expressionA> -{0,1} [^\\+-]+?) [ ]* (?<operator>[\\+-]) [ ]* (?<expressionB> -{0,1} [^\\+-]+)";
        private static readonly string _REGEXP_EXECUTION = "^[ ]* (?<keyword>SET|GOTO|GOSUB|GOWEB) [ ]+ (?<action>(:?.(?!SET|GOTO|GOSUB|GOWEB))* [^\\ ])";
        private static readonly string _REGEXP_ASSIGNMENT = "^[ ]* (?<variable>.+?) [ ]* = [ ]* (?<value>.+?) [ ]* $";

        public static bool IsValidExpression(string expressionString)
        {
            bool isValid = false;
            if (!expressionString.Equals("EndPage"))
            {
                Regex ifRegex = new Regex(_REGEXP_IF, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

                long matchedLength = 0;
                Match ifRegexResult = ifRegex.Match(expressionString);
                while (ifRegexResult.Success)
                {
                    matchedLength += ifRegexResult.Length;

                    isValid = ParseCompareExpression(ifRegexResult.Groups["ifCondition"].Value);

                    if (!isValid)
                    {
                        break;
                    }
                    else
                    {
                        isValid = ParseExecutionExpression(ifRegexResult.Groups["ifExpression"].Value);
                        if (!isValid)
                        {
                            break;
                        }
                        else
                        {
                            ifRegexResult = ifRegexResult.NextMatch();
                        }
                    }
                }

                if (isValid && matchedLength == expressionString.Length)
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool ParseCompareExpression(string ifExpression)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(ifExpression))
            {
                Regex conditionRegex = new Regex(_REGEXP_CONDITION, RegexOptions.IgnorePatternWhitespace);
                Match conditionRegexResult = conditionRegex.Match(ifExpression);
                isValid = conditionRegexResult.Success;
                if (!isValid)
                {
                    Regex badEqualRegex = new Regex(_REGEXP_BAD_EQUAL_FORMATE, RegexOptions.IgnorePatternWhitespace);
                    Match badEqualRegexResult = badEqualRegex.Match(ifExpression);
                    if (badEqualRegexResult.Success)
                    {
                        throw new ArgumentException("Invalid page expression. Please use == instead of = as equal operator.");
                    }
                    else
                    {
                        throw new ArgumentException("Invalid page expression. The IF condition is in bad format.");
                    }
                }
                else
                {
                    isValid = ParseMathExpression(conditionRegexResult.Groups["expressionA"].Value);
                    if (isValid)
                    {
                        isValid = ParseMathExpression(conditionRegexResult.Groups["expressionB"].Value);
                    }
                }
            }
            else
            {
                isValid = true;
            }
            return isValid;
        }

        public static bool ParseGraphDateItemExpression(string dataItemExpression)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(dataItemExpression))
            {
                Regex graphExpressionRegex = new Regex(_REGEXP_GRAPH_DATA_EXPRESSION, RegexOptions.IgnorePatternWhitespace);
                Match graphExpressioRegexResult = graphExpressionRegex.Match(dataItemExpression);
                isValid = graphExpressioRegexResult.Success;
            }
            return isValid;
        }

        private static bool ParseMathExpression(string mathExpression)
        {
            bool isValid = true;
            Regex bracketRegex = new Regex(_REGEXP_BRACKET, RegexOptions.IgnorePatternWhitespace);
            Match bracketRegexResult = bracketRegex.Match(mathExpression);
            while (bracketRegexResult.Success)
            {
                switch (bracketRegexResult.Groups["functionName"].Value)
                {
                    case "GetIndex":
                        break;
                    case "":
                        break;
                    default:
                        isValid = false;
                        throw new ArgumentException("Invalid function name.");
                }
                bracketRegexResult = bracketRegexResult.NextMatch();
            }

            if (isValid)
            {
                //Regex bracketUnmatchRegext = new Regex(_REGEXP_BRACKET_UNMATCH);
                //Match bracketUnmatchResult = bracketUnmatchRegext.Match(mathExpression);
                //if (bracketUnmatchResult.Success)
                //{
                //    isValid = false;
                //    throw new ArgumentException("Invalid page expression. Parenthesis does not match.");
                //}
                //else
                //{
                Regex multiDivideRegex = new Regex(_REGEXP_MULTI_DIVIDE, RegexOptions.IgnorePatternWhitespace);
                Match multiDivideResult = multiDivideRegex.Match(mathExpression);
                while (multiDivideResult.Success)
                {
                    switch (multiDivideResult.Groups["operator"].Value)
                    {
                        case "*":
                            break;
                        case "/":
                            break;
                        default:
                            isValid = false;
                            throw new ArgumentException(string.Format("Invalid operator {0}.", multiDivideResult.Groups["operator"].Value));
                    }

                    if (isValid)
                    {
                        multiDivideResult = multiDivideResult.NextMatch();
                    }
                }

                //Regex multiDivideUnmatchRegex = new Regex(_REGEXP_MULTI_DIVIDE_UNMATCH);
                //Match multiDivideUnmatchResult = multiDivideRegex.Match(mathExpression);
                //if (multiDivideUnmatchResult.Success)
                //{
                //    isValid = false;
                //    throw new ArgumentException("Invalid page expression. multiplication or division is not match.");
                //}
                //else
                //{
                Regex addMinusRegex = new Regex(_REGEXP_ADD_MINUS, RegexOptions.IgnorePatternWhitespace);
                Match addMinusResult = addMinusRegex.Match(mathExpression);
                while (addMinusResult.Success)
                {
                    switch (addMinusResult.Groups["operator"].Value)
                    {
                        case "+":
                            break;
                        case "-":
                            break;
                        default:
                            throw new ArgumentException(string.Format("Invalid operator {0}.", addMinusResult.Groups["operator"].Value));
                    }
                    addMinusResult = addMinusResult.NextMatch();
                }
                //}
                //}
            }

            return isValid;
        }

        private static bool ParseExecutionExpression(string executionExpression)
        {
            bool isValid = true;
            Regex executionRegex = new Regex(_REGEXP_EXECUTION, RegexOptions.IgnorePatternWhitespace);
            Match executionRegexResult = executionRegex.Match(executionExpression);
            while (executionRegexResult.Success)
            {
                switch (executionRegexResult.Groups["keyword"].Value)
                {
                    case "SET":
                    case "GOTO":
                    case "GOSUB":
                    case "GOWEB":
                        break;
                    default:
                        isValid = false;
                        throw new ArgumentException(string.Format("Invalid keyword {0}.", executionRegexResult.Groups["keyword"].Value));
                }

                if (!isValid)
                {
                    break;
                }
                else
                {
                    isValid = ParseAssignmentExpression(executionRegexResult.Groups["action"].Value);
                    executionRegexResult = executionRegexResult.NextMatch();
                }
            }
            return isValid;
        }

        private static bool ParseAssignmentExpression(string assignmentExpression)
        {
            bool isValid = true;
            Regex assignmentRegex = new Regex(_REGEXP_ASSIGNMENT, RegexOptions.IgnorePatternWhitespace);
            Match assignmentRegexResult = assignmentRegex.Match(assignmentExpression);
            while (assignmentRegexResult.Success)
            {
                isValid = ParseMathExpression(assignmentRegexResult.Groups["value"].Value);

                if (!isValid)
                {
                    break;
                }
                else
                {
                    assignmentRegexResult = assignmentRegexResult.NextMatch();
                }
            }
            return isValid;
        }
    }
}
