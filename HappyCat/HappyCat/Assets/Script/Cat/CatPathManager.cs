using GoogleSheet;
using HC.Game;
using System;
using System.Collections.Generic;
using System.Reflection;
using UGS;
using UnityEngine;

namespace HC.Utils
{
    public class CatPathManager
    {
        static readonly Vector3 tableOffset = new Vector3(0, 60, 0);

        static readonly Vector3 startPath1 = new Vector3(160, 870, 0);
        static readonly Vector3 startPath2 = new Vector3(250, 870, 0);
        static readonly Vector3 startPath3 = new Vector3(340, 870, 0);
        static readonly Vector3 waitPath = new Vector3(250, 260, 0);
        //static readonly Vector3 table1Path = new Vector3(-315, 3, 0);
        //static readonly Vector3 table2Path = new Vector3(0, 3, 0);
        //static readonly Vector3 table3Path = new Vector3(314, 3, 0);
        //static readonly Vector3 table4Path = new Vector3(-312.2f, -242.6f, 0);
        //static readonly Vector3 table5Path = new Vector3(0, -242.6f, 0);
        //static readonly Vector3 table6Path = new Vector3(314, -242.6f, 0);
        //static readonly Vector3 oven1Path = new Vector3(-330, 20.5f, 0);
        //static readonly Vector3 oven2Path = new Vector3(-4, 20.5f, 0);
        //static readonly Vector3 oven3Path = new Vector3(320, 20.5f, 0);
        //static readonly Vector3 oven4Path = new Vector3(-330, -290, 0);
        //static readonly Vector3 oven5Path = new Vector3(-4, -290, 0);
        //static readonly Vector3 oven6Path = new Vector3(320, -290, 0);

        public static Vector3 GetStartPosition()
        {
            List<Vector3> list = new List<Vector3>() { 
                startPath1,
                startPath2,
                startPath3,
            };
            return DataUtill.GetRandom(list);
        }

        public static Vector3 GetWaitPosition(int index)
        {
            var pos = waitPath;
            pos.Set(pos.x, pos.y + (index * 50), pos.z);
            return pos;
        }

        public static Vector3 GetTablePosition(int index)
        {
            var a = FurnitureManager.Table1.transform;
            switch (index) { 
                case 0: return FurnitureManager.Table1.transform.position + tableOffset;
                case 1: return FurnitureManager.Table2.transform.position + tableOffset;
                case 2: return FurnitureManager.Table3.transform.position + tableOffset;
                case 3: return FurnitureManager.Table4.transform.position + tableOffset;
                case 4: return FurnitureManager.Table5.transform.position + tableOffset;
                case 5: return FurnitureManager.Table6.transform.position + tableOffset;
                default: return Vector3.zero;
            }
        }

        //public static Vector3 GetExplorePosition()
        //{

        //}
    }
}


