using Cysharp.Threading.Tasks;
using HC.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    protected RectTransform rect;
    protected Image backImage;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public virtual UniTask OnOpen() { return default; }
    public virtual void OnClose() {
        backImage?.GetComponent<HCButton>()?.onClick?.RemoveAllListeners();
    }
    public virtual void Setting(GameObject objectRoot)
    {
        GameObject back = new GameObject("back", typeof(RectTransform));
        back.transform.SetParent(objectRoot.transform);
        back.transform.SetSiblingIndex(0);
        backImage = back.AddComponent<Image>();
        backImage.color = new Color(0, 0, 0, 0.5f);
        var button = back.AddComponent<HCButton>();
        back.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        back.GetComponent<RectTransform>().anchorMax = Vector2.one;
        back.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        back.GetComponent<RectTransform>().offsetMax = Vector2.zero;

        button.onClick.AddListener(Close);

    }

    protected void Close()
    {
        UIManager.CloseUI(rect.parent.name);
    }
}