using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HC.Resource
{
    public class ResourcesManager
    {
        //parent
        

        static string UIPath;
        static string catPrefabPath = "Assets/HC_Resources/Prefabs/Cat/Cat.prefab";
        static string catAnimationPath = "Assets/HC_Resources/Art/Cat/Animator/";

        private static async Task<T> Create<T>(string path)
        {
            var handle = await Addressables.LoadAssetAsync<T>(path).Task;

            return handle;
        }

        public static async Task<T> Create_AnimController<T>(string name)
        {
            string path = $"{catAnimationPath}AC_{name}.controller";

            return await Create<T>(path);
        }

        public static async Task<T> Create_Cat<T>(int code)
        {
            var p = await Create<GameObject>(catPrefabPath);
            var cat = GameObject.Instantiate(p);
            cat.GetComponent<Cat_Actor>().Init(code);
            cat.SetActive(true);
            var parent = GameObject.Find("Cats");
            cat.transform.SetParent(parent.transform);
            return cat.GetComponent<T>();
        }
    }
}

