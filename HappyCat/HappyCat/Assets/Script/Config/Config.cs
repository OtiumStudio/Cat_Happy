using HC.Utils;
using UnityEngine;

namespace HC.Config
{
    public enum E_Camera
    {
        Default,
        loadingPage
    }
    public class Config
    {
        static readonly float _1080x2340 = 0.4615384615384615f;
        static readonly float _1080x1920 = 0.5625f;
        static readonly int _1080x2340h = 2340;
        static readonly int _1080x1920h = 1920;
        public static void SetCamera(E_Camera setting = E_Camera.Default)
        {
            switch (setting)
            {
                case E_Camera.loadingPage:
                    SetCamera_Loading();
                    break;
                case E_Camera.Default:
                    SetCamera_Default(_1080x1920, _1080x1920h);
                    break;
            }
        }
        private static void SetCamera_Loading()
        {
            SetCamera_MinSize(_1080x2340, _1080x2340h, _1080x1920, _1080x1920h);
        }


        private static void SetCamera_Default(float desiredAspect, float desiredHeight)
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float desiredCameraHeight = desiredAspect / screenAspect;
            if (screenAspect > desiredAspect)
            {
                Camera.main.orthographicSize = desiredHeight / 2f;
                return;
            }
            float tagetHeight = desiredCameraHeight * desiredHeight;
            Camera.main.orthographicSize = tagetHeight / 2f;
        }
        private static void SetCamera_MinSize(float desiredAspect, float desiredHeight, float aspect, float height)
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float desiredCameraHeight = desiredAspect / screenAspect;
            if (screenAspect >= aspect)
            {
                Camera.main.orthographicSize = height / 2f;
                return;
            }
            else if (screenAspect > desiredAspect)
            {
                Camera.main.orthographicSize = desiredHeight / 2f;
                return;
            }
            float tagetHeight = desiredCameraHeight * desiredHeight;
            Camera.main.orthographicSize = tagetHeight / 2f;
        }
    }
}

