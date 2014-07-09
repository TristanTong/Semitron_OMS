using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Semitron_OMS.CommonWeb
{
    public class JsonTranslator
    {

        /// <summary>
        /// �����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcBytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] srcBytes)
        {
            string result = Encoding.UTF8.GetString(srcBytes);
            return JsonConvert.DeserializeObject<T>(result);
        }
        /// <summary>
        /// �����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcBytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string srcStr)
        {
            return JsonConvert.DeserializeObject<T>(srcStr);
        }
        static int index = 0;
        /// <summary>
        /// ���л�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {

            string json = JsonConvert.SerializeObject(obj, new IsoDateTimeConverter());
            return Encoding.UTF8.GetBytes(json);

        }

    }

}