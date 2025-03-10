using System.Text;
using UnityEngine;

namespace HC.Utils
{
    public class DataUtill
    {
        public static string ObjectToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T JsonToObject<T>(string jsonData)
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
        public static byte[] DataToByte<T>(T obj) where T : class
        {
            var json = ObjectToJson(obj);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T ByteToData<T>(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonToObject<T>(json);
        }
    }
}