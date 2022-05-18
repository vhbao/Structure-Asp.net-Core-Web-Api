using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.ExtensionMethods
{
    public static class JTokenExtensions
    {
        public static string ToString(JToken token)
        {
            try
            {
                if (token == null)
                    return "";                     
                return token.ToString();
            }
            catch(Exception ex) { return ""; }            
        }
        public static string ToString<T>(JToken token)
        {
            try
            {
                if (token == null)
                    return "";
                return JsonConvert.SerializeObject(token.ToObject<T>());
            }
            catch (Exception ex) { return ""; }
        }
    }
}
