using GoogleSheet;
using System;
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
    }
}


