using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HV2020.Eservice.WCF.Felanmalan.Utility
{
    public class Whitelist
    {
            /// <summary>
            /// whitelist med timeout för att förhindra regex denial of service
            /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-strip-invalid-characters-from-a-string
            /// </summary>
            /// <param name="strInput"> input innan whitelist</param>
            /// <returns></returns>
            public string CleanInput(string strInput)
            {
                // Replace invalid characters with empty strings.
                try {
                    return Regex.Replace(strInput, @"[^\w\.@-]", "",
                                         RegexOptions.None, TimeSpan.FromSeconds(1.5));
                }
                // If we timeout when replacing invalid characters,
                // we should return Empty.
                catch (RegexMatchTimeoutException) {
                    return String.Empty;
                }
            }
        }
}