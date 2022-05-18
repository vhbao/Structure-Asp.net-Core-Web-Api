using Newtonsoft.Json;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Wolf.Core.ExtensionMethods
{
    public static class RegularExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                string pattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                return Regex.IsMatch(email, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsValidPhoneVN(this string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(phonenumber))
                return false;
            try
            {
                string pattern = "(84|0[3|5|7|8|9])+([0-9]{8})\b";
                return Regex.IsMatch(phonenumber, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
