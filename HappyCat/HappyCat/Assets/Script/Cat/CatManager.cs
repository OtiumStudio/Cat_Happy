using UnityEngine;

namespace HC.Game
{
    public class CatManager
    {
        static CatJoin joinCat;
        public static void CatInit()
        {
            joinCat = new CatJoin();
        }
        public static void CatUpdate()
        {
            joinCat?.Update();
        }
    }
}

