using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class OneButtonPopup : MonoBehaviour
{
    [SerializeField]
    private Button block;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Button checkButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        block.onClick.AddListener(OnClose);
        checkButton.onClick.AddListener(OnClose);
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

    void OnClose()
    {
        Destroy(gameObject);
    }
}
