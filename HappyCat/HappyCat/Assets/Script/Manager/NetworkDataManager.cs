using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HC.Network
{
    public class NetworkDataManager
    {
        private static GoogleLogin googleLogin;
        private static IosLogin iosLogin;
        public static void Init()
        {
            if (googleLogin == null) googleLogin = new GoogleLogin();
            if (iosLogin == null) iosLogin = new IosLogin();

            googleLogin.Init();
            iosLogin.Init();
        }

        public static void SetText(TextMeshProUGUI text)
        {
            googleLogin.SetText(text);
        }
        public static void SetGoogleButton(Button googleLoginButton)
        {
            if (googleLogin == null) return;

            googleLoginButton?.onClick.AddListener(googleLogin.SignIn);
        }

        public static void SetIosButton(Button iosLoginButton)
        {
            iosLoginButton?.onClick.AddListener(iosLogin.SignIn);
        }
    }
}

