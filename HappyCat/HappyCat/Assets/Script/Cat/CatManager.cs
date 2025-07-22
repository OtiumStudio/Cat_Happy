using HC.Event;
using HC.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HC.Game
{
    public class CatManager
    {
        static List<Cat_Actor> cats = new List<Cat_Actor>();
        static Queue<Cat_Actor> waitCats = new Queue<Cat_Actor>();
        static CatJoin joinCat;
        public static void CatInit()
        {
            joinCat = new CatJoin();

            Bind();
        }
        public static void CatUpdate()
        {
            joinCat?.Update();
        }

        static void Bind()
        {
            GameEvent.ServiceEvents.On<JoinCatEvent>(OnJoinCat);
        }

        private static void OnJoinCat(JoinCatEvent e)
        {
            cats.Add(e.Cat);
            waitCats.Enqueue(e.Cat);
            e.Cat.SetDestination(CatPathManager.GetWaitPosition(waitCats.Count - 1));
        }
    }
}

