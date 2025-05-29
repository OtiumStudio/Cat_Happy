using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace HC.Resource
{
    public class LoadAddressableManager
    {
        //cache
        static GameObject rootCat;

        //parent
        static string UIPath;
        static string catPrefabPath = "Assets/HC_Resources/Prefabs/Cat/Cat.prefab";
        static string catAnimationPath = "Assets/HC_Resources/Art/Cat/Animator/";
        static string imageFurniturePath = "Assets/HC_Resources/Art/Atlas/BG.spriteatlasv2";

        private static async Task<T> Load<T>(string path)
        {
            var handle = await Addressables.LoadAssetAsync<T>(path).Task;

            return handle;
        }

        public static async Task<T> Load_AnimController<T>(string name)
        {
            string path = $"{catAnimationPath}AC_{name}.controller";

            return await Load<T>(path);
        }

        public static async Task<T> Create_Cat<T>(int code)
        {
            if (rootCat == null) rootCat = GameObject.Find("Cats");

            var p = await Load<GameObject>(catPrefabPath);
            var cat = GameObject.Instantiate(p);
            cat.GetComponent<Cat_Actor>().Init(code);
            cat.SetActive(true);
            
            cat.transform.SetParent(rootCat.transform);
            return cat.GetComponent<T>();
        }

        public static async Task<Sprite> LoadImage_Furniture(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var sa = await Load<SpriteAtlas>(imageFurniturePath);
            return sa.GetSprite(name);
        }
    }
}

