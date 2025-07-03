using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public virtual UniTask OnOpen() { return default; }
    public virtual void OnClose() { }
}