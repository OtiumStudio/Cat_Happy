using HC.Data;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HC.Utils
{
    public class UserUtill
    {
        public static GuestTable.Data GetCat()
        {
            var visitTableList = UGSManager.GetList<VisitTable.Data>();
            var guestTableList = UGSManager.GetList<GuestTable.Data>();
            var userData = DataManager.ServerData.userData;

            VisitTable.Data selectData = visitTableList.First();
            foreach (var data in visitTableList)
            {
                if (userData.level < data.level) break;
                selectData = data;
            }


            List<WeightedItem<int>> list = new List<WeightedItem<int>>();
            list.Add(new WeightedItem<int>(1, selectData.rank1));
            list.Add(new WeightedItem<int>(2, selectData.rank2));
            list.Add(new WeightedItem<int>(3, selectData.rank3));
            list.Add(new WeightedItem<int>(4, selectData.rank4));
            list.Add(new WeightedItem<int>(5, selectData.rank5));

            int rank = DataUtill.GetWeightedRandom<int>(list);
            var guestlist = guestTableList.FindAll(x => x.rank == rank);

            return DataUtill.GetRandom(guestlist);
        }
    }
}