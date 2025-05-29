using GoogleSheet;
using System;
using System.Collections.Generic;
using System.Reflection;
using UGS;
using UnityEngine;

namespace HC.Utils
{
    public class CatPathManager
    {
        static readonly Vector3 startPath1 = new Vector3(160, 870, 0);
        static readonly Vector3 startPath2 = new Vector3(250, 870, 0);
        static readonly Vector3 startPath3 = new Vector3(340, 870, 0);
        static readonly Vector3 waitPath = new Vector3(250, 260, 0);
        static readonly Vector3 table1Path = new Vector3(-315, 63, 0);
        static readonly Vector3 table2Path = new Vector3(0, 63, 0);
        static readonly Vector3 table3Path = new Vector3(315, 63, 0);
        static readonly Vector3 table4Path = new Vector3(-315, -238, 0);
        static readonly Vector3 table5Path = new Vector3(0, -238, 0);
        static readonly Vector3 table6Path = new Vector3(315, -238, 0);

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

        //public static Vector3 GetExplorePosition()
        //{

        //}
    }
}


