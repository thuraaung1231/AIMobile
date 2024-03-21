using Newtonsoft.Json;

namespace AIMobile.Services.Utilities
{
    public static class SessionHelper
    {
        public static void SetDataToSession(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }
        public static T GetDataFromSession<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            if (value == null)
            {
                return default(T);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T)) ?? default(T);
            }
        }
        public static void ClearSession(this ISession session)
        {
            session.Clear();
        }
    }
}
