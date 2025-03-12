using GooglePlayGames;
using GooglePlayGames.BasicApi;
using HC.Event;
using TMPro;
using Unity.VisualScripting;
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
#if UNITY_EDITOR
            status = SignInStatus.Success;
#endif
            if (status == SignInStatus.Success)
            {
                NetworkEvent.ServiceEvents.Emit(new GoogleLoginComplete(true));
            }
            else
            {
                NetworkEvent.ServiceEvents.Emit(new GoogleLoginComplete(false));
            }
        }
    }
}

