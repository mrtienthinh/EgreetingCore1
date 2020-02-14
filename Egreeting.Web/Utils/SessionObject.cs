using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Egreeting.Web.Utils
{
    public class SessionObject
    {
        static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }

        public static T Get<T>(string key)
        {
            if (Session[key] == null)
                return default(T);
            else
                return (T)Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        public static string GetString(string key)
        {
            string s = Get<string>(key);
            return s == null ? string.Empty : s;
        }

        public static void SetString(string key, string value)
        {
            Set<string>(key, value);
        }

        public static int GetInt(string key)
        {
            int s = Get<int>(key);
            return s;
        }

        public static void SetInt(string key, int value)
        {
            Set<int>(key, value);
        }

        public static int? GetNullableInt(string key)
        {
            int? s = Get<int?>(key);
            return s;
        }

        public static void SetNullableInt(string key, int? value)
        {
            Set<int?>(key, value);
        }

        public static bool GetBool(string key)
        {
            bool s = Get<bool>(key);
            return s;
        }

        public static void SetBool(string key, bool value)
        {
            Set<bool>(key, value);
        }
    }
}