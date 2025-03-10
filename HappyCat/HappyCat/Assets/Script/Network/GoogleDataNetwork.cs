using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using HC.Data;
using HC.Utils;
using UnityEngine;

namespace HC.Network
{
    public class GoogleDataNetwork
    {
        private SavedGameRequestStatus status;
        private ISavedGameMetadata game;
        public void SaveData<T>(T oData) where T : class
        {
            ISavedGameClient client = PlayGamesPlatform.Instance.SavedGame;

            if(oData is IData)
            {
                var _data = (IData)oData;
                client.OpenWithAutomaticConflictResolution(_data.UID, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, CheckData);
            }

            if (status == SavedGameRequestStatus.Success)
            {
                var update = new SavedGameMetadataUpdate.Builder().Build();
                byte[] bData = DataUtill.DataToByte<T>(oData);
                client.CommitUpdate(game, update, bData, OnSaveComplete);
            }
            else
            {
                Debug.Log("Save No.....");
            }
        }
        public void LoadData<T>(T oData)
        {
            ISavedGameClient client = PlayGamesPlatform.Instance.SavedGame;

            if (oData is IData)
            {
                var _data = (IData)oData;
                client.OpenWithAutomaticConflictResolution(_data.UID, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, CheckData);
            }

            if (status == SavedGameRequestStatus.Success)
            {
                client.ReadBinaryData(game, OnLoadComplete);
            }
            else
            {
                Debug.Log("Load No.....");
            }
        }

        private void CheckData(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            this.status = status;
            this.game = game;
        }

        private void OnSaveComplete(SavedGameRequestStatus status, ISavedGameMetadata data)
        {

        }
        private void OnLoadComplete(SavedGameRequestStatus status, byte[] loadedData)
        {
            //return DataUtill.ByteToData(loadedData);
        }
    }
}

