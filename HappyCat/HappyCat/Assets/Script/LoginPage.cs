using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    public Button LoginButton;
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        LoginButton.onClick.AddListener(SignIn);
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            Debug.Log(name);
            Debug.Log(id);
            Debug.Log(imgUrl);
        }
        else
        {
            Debug.Log("Sign in Failed!");
        }
    }
}
