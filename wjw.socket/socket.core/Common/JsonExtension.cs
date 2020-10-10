using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace wjw.socket.Common
{
    public static class JsonExtension
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJSON(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 把对象转换为JSON字符串,忽略空值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSONIgnoreNullValue(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, jSetting);
        }

        public static object FromJSON(string data)
        {
            return JsonConvert.DeserializeObject(data);
        }

        public static object FromJSON(Type t, string data)
        {
            return JsonConvert.DeserializeObject(data, t);
            //MethodInfo mi = typeof(JsonConvert).GetMethod("DeserializeObject").MakeGenericMethod(t);
            //return  mi.Invoke( null,new object[] { data });
        }

        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
