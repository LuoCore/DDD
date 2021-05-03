using Newtonsoft.Json;
using System;

namespace Infrastructure.Common
{
    public static class JsonHelper
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJSON(this object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string input)
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

        /// <summary>
        /// 序列化成Json格式的字符串
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static String ToJson(this Object Value)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            string json = js.Serialize(Value);
            return json;
        }
    }
}
