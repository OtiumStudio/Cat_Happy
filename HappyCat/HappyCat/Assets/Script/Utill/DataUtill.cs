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
            // ���� �׸��� ������ �⺻�� ��ȯ
            if (weightedItems.Count == 0)
                return default(T);

            // ��� ����ġ�� ���Ͽ� ������ ����
            float totalWeight = 0f;
            foreach (var weightedItem in weightedItems)
            {
                totalWeight += weightedItem.weight;
            }

            // 0���� ���� ������ ���� ���� ����
            float randomValue = UnityEngine.Random.value * totalWeight;

            // ���� ���� ��� ������ ���ϴ��� Ȯ���Ͽ� �׸� ����
            foreach (var weightedItem in weightedItems)
            {
                randomValue -= weightedItem.weight;
                if (randomValue <= 0)
                    return weightedItem.item;
            }

            // ������� �Դٸ� ���� �߸��� ���� �ƴϹǷ� ������ �׸� ��ȯ
            return weightedItems[weightedItems.Count - 1].item;
        }
        public static T GetRandom<T>(List<T> items)
        {
            // ���� �׸��� ������ �⺻�� ��ȯ
            if (items.Count == 0)
                return default(T);

            // 0���� ���� ������ ���� ���� ����
            float randomValue = UnityEngine.Random.value * items.Count;

            // ���� ���� ��� ������ ���ϴ��� Ȯ���Ͽ� �׸� ����
            foreach (var weightedItem in items)
            {
                randomValue -= 1;
                if (randomValue <= 0)
                    return weightedItem;
            }

            // ������� �Դٸ� ���� �߸��� ���� �ƴϹǷ� ������ �׸� ��ȯ
            return items.Last();
        }
    }
}