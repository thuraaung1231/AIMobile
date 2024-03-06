using AIMobile.Models.DataModels;
using Newtonsoft.Json;
using System.Collections;

namespace AIMobileCus.Services.Utilities
{
    public static class SessionHelper
    {
        public static void SetDataToSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static ArrayList GetDataFromSession<T>(this ISession session, string key)
        {
            ArrayList response = new ArrayList();
            var value = session.GetString(key);
            var count = JsonConvert.DeserializeObject<T[]>(value);
            if(count != null)
            {
				for (int i = 0; i < count.Length; i++)
				{
					response.Add(JsonConvert.DeserializeObject<T[]>(value)[i]);
				}
			}
            return response;

			//return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}

		public static void ClearSession(this ISession session)
        {
            session.Clear();
        }
    }
}
