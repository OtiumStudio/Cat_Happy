using UnityEngine;

namespace HC.Utils
{
    public class DontDestroyLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
