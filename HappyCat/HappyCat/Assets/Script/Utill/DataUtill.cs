using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HC.Utils
{
    [System.Serializable]
    public class WeightedItem<T>
    {
        public T item;
        public float weight;

        public WeightedItem(T item, float weight){
            this.item = item;
            this.weight = weight;
        }
    }

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
        public static T GetWeightedRandom<T>(List<WeightedItem<T>> weightedItems)
        {
            // 만약 항목이 없으면 기본값 반환
            if (weightedItems.Count == 0)
                return default(T);

            // 모든 가중치를 더하여 총합을 구함
            float totalWeight = 0f;
            foreach (var weightedItem in weightedItems)
            {
                totalWeight += weightedItem.weight;
            }

            // 0부터 총합 사이의 랜덤 값을 생성
            float randomValue = UnityEngine.Random.value * totalWeight;

            // 랜덤 값이 어느 범위에 속하는지 확인하여 항목 선택
            foreach (var weightedItem in weightedItems)
            {
                randomValue -= weightedItem.weight;
                if (randomValue <= 0)
                    return weightedItem.item;
            }

            // 여기까지 왔다면 뭔가 잘못된 것이 아니므로 마지막 항목 반환
            return weightedItems[weightedItems.Count - 1].item;
        }
        public static T GetRandom<T>(List<T> items)
        {
            // 만약 항목이 없으면 기본값 반환
            if (items.Count == 0)
                return default(T);

            // 0부터 총합 사이의 랜덤 값을 생성
            float randomValue = UnityEngine.Random.value * items.Count;

            // 랜덤 값이 어느 범위에 속하는지 확인하여 항목 선택
            foreach (var weightedItem in items)
            {
                randomValue -= 1;
                if (randomValue <= 0)
                    return weightedItem;
            }

            // 여기까지 왔다면 뭔가 잘못된 것이 아니므로 마지막 항목 반환
            return items.Last();
        }
    }
}