using GoogleSheet;
using System;
using System.Collections.Generic;
using System.Reflection;
using UGS;
using UnityEngine;

namespace HC.Data
{
    public static class UGSManager
    {
        public static void Init()
        {
            UnityGoogleSheet.LoadAllData();
            //UnityGoogleSheet.Load<GuestTable.Guest>();
        }

        public static T GetData<T>(int code) where T : ITable
        {
            var dataMap = typeof(T).GetField("DataMap").GetValue(null) as Dictionary<int, T>;
            if (dataMap.Count < 1)
            {
                Init();
                dataMap = typeof(T).GetField("DataMap").GetValue(null) as Dictionary<int, T>;
            }
            if (!dataMap.ContainsKey(code))
                return default(T);

            return dataMap[code];
        }
        public static Dictionary<int, T> GetMap<T>()
        {
            var dataMap = typeof(T).GetField("DataMap").GetValue(null) as Dictionary<int, T>;
            if(dataMap.Count < 1)
            {
                Init();
                dataMap = typeof(T).GetField("DataMap").GetValue(null) as Dictionary<int, T>;
            }

            return dataMap;
        }
        public static List<T> GetList<T>()
        {
            var dataList = typeof(T).GetField("DataList").GetValue(null) as List<T>;
            if (dataList.Count < 1)
            {
                Init();
                dataList = typeof(T).GetField("DataList").GetValue(null) as List<T>;
            }

            return dataList;
        }
    }
}


