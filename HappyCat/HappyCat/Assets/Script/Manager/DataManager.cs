using HC.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace HC.Data
{
    public class DataManager
    {
        private static LocalSaveData saveData;
        private static ServerData serverData;
        public static LocalSaveData SaveData { get => saveData; }
        public static ServerData ServerData { get => serverData; }

        public static void Init()
        {
            LocalDataLoad();
        }

        private static void SetSaveData()
        {
            saveData = new LocalSaveData(LocalDataSave);
            LocalDataSave();
        }
        private static void SetServerData()
        {
            serverData = new ServerData();
            ServerDataLoad();
        }

        private static void LocalDataSave()
        {
            if (saveData == null) return;

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/LocalData.dat");
                bf.Serialize(file, saveData);
                file.Close();
                HC_Debug.Log("Local data saved!");
            }
            catch (Exception e)
            {
                HC_Debug.Log(e.Message);
            }
        }
        private static void LocalDataLoad()
        {
            if(File.Exists(Application.persistentDataPath + "/LocalData.dat") == false)
            {
                saveData = null;
                SetSaveData();
                return;
            }

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/LocalData.dat", FileMode.Open);
                saveData = (LocalSaveData)bf.Deserialize(file);
                file.Close();
                HC_Debug.Log("Local Data Loeaded!");
            }
            catch (Exception e)
            {
                SetSaveData();
                HC_Debug.Log(e.Message);
                return;
            }
        }

        private static void ServerDataLoad()
        {

        }
    }
}