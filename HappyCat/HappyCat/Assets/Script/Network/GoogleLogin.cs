using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace HC.Network
{
    public class GoogleLogin
    {
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
            }
            else
            {
                Debug.Log("Sign in Failed!");
            }
        }
    }
}

