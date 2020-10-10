using System;
using System.Collections.Generic;
using System.Text;
using wjw.helper.Extensions;

namespace wjw.helper
{
    public class DataCache
    {
        private static Dictionary<string, object> _cache = new Dictionary<string, object>();

        public static void Set(string key,object value)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
            }
            _cache.Add(key, value);
        }

        public static object Get(string key)
        {
            if (_cache.ContainsKey(key))
                return _cache[key];
            else
                return null;
        }

        public static T Get<T>(string key)
        {
            if (_cache.ContainsKey(key))
                return _cache[key].ConvertTo<T>();
            else
                return default(T);
        }

        public static bool isExist(string key)
        {
            if (_cache.ContainsKey(key))
                return true;
            else
                return false;
        }

    }

}
