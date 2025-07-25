using DG.Tweening;
using HC.Data;
using HC.Event;
using HC.Resource;
using HC.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace HC.Game
{
    public class FoodData
    {
        public int TableIndex { get; set; }
        public int KetchenIndex { get; set; }

        public FoodData(int tableIndex, int ketchenIndex)
        {
            this.TableIndex = tableIndex;
            this.KetchenIndex = ketchenIndex;
        }
    }
}

