using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CCMG.Monitoring.Util
{
    public static class RegexUtil
    {
        public static string regKeyValPair = "\\[(?<key>[^=]*?)=(?<value>[^\\]]*?)\\]";

        public static MatchCollection RegCollection(string input,string pattern="")
        {
            if (string.IsNullOrEmpty(input)) return null;
            if (string.IsNullOrEmpty(pattern))
                return Regex.Matches(input, regKeyValPair);

            return Regex.Matches(input, pattern);
        }

        public static string RegReplaceStr(string replaceStr,string input,string pattern,out bool isSuccess)
        {
            isSuccess = false;
            var matchs=RegCollection(replaceStr, pattern);
            if (matchs == null || matchs.Count <= 0) return input;

            foreach(Match m in matchs)
            {
                if (m.Success)
                    input = input.Replace("[" +m.Groups["key"].Value+ "]", m.Groups["value"].Value);
            }
            isSuccess = true;
            return input;
        }
    }
}
