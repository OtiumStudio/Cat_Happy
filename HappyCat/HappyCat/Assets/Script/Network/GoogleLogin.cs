using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace HC.Network
{
    public class GoogleLogin
    {
        TextMeshProUGUI text;
        public void SetText(TextMeshProUGUI text)
        {
            this.text = text;
        }
        public void Init()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
        public void SignIn()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }
        private void ProcessAuthentication(SignInStatus status)
        {
            Debug.Log(status.ToString());
            if (status == SignInStatus.Success)
            {
                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                string id = PlayGamesPlatform.Instance.GetUserId();
                string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

                Debug.Log(name);
                Debug.Log(id);
                Debug.Log(imgUrl);
                text.text = $"{name}, {id}, {imgUrl}";
            }
            else
            {
                Debug.Log("Sign in Failed!");
                text.text = $"{status.ToString()}";
            }
        }
    }
}

