using HC.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    public Button googleLoginButton;
    public TextMeshProUGUI testText;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        NetworkDataManager.Init();
        NetworkDataManager.SetGoogleButton(googleLoginButton);
        NetworkDataManager.SetText(testText);
    }
}
