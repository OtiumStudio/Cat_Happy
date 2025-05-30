using UnityEngine;

public class UI_Button : MonoBehaviour
{
    public GameObject popup;
    private void Awake()
    {
        popup.SetActive(false);
    }

    public void Open()
    {
        popup.SetActive(true);

    }
    public void Close()
    {
        popup.SetActive(false);
    }
}
