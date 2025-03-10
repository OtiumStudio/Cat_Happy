using System;
using UnityEngine;

namespace HC.Data
{
    [Serializable]
    public class LocalSaveData : IData
    {
        private Action SAVE;
        public string UID { get; set; }
        public string saveGoogleAccessToken;
        public int gameTargetFrame;
        public bool isBgm;
        public bool isSfx;
        public float volumeBgm;
        public float volumeSfx;
        public int selectLanguage;
        public byte[] temp_serverData;
        public LocalSaveData(Action save)
        {
            SAVE = save;
            this.UID = this.GetType().Name;
            saveGoogleAccessToken = string.Empty;
            gameTargetFrame = 60;
            isBgm = true;
            isSfx = true;
            volumeBgm = 0.8f;
            volumeSfx = 0.8f;
            selectLanguage = (int)Application.systemLanguage;
        }

        public void GoogleAccessTokenSave(string saveGoogleAccessToken)
        {
            this.saveGoogleAccessToken = saveGoogleAccessToken;
            SAVE.Invoke();
        }
        public void IsBgmSave(bool isBgm)
        {
            this.isBgm = isBgm;
            SAVE.Invoke();
        }
        public void IsSfxSave(bool isSfx)
        {
            this.isSfx = isSfx;
            SAVE.Invoke();
        }
        public void VolumeBgmSave(float volumeBgm)
        {
            this.volumeBgm = volumeBgm;
            SAVE.Invoke();
        }
        public void VolumeSfxSave(float volumeSfx)
        {
            this.volumeSfx = volumeSfx;
            SAVE.Invoke();
        }

    }
}
