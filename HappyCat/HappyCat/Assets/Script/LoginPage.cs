using HC.Network;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    public Button googleLoginButton;
    void Start()
    {
        NetworkDataManager.Init();
        NetworkDataManager.SetGoogleButton(googleLoginButton);
    }
}
