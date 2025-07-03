using HC.Config;
using HC.Data;
using HC.Event;
using HC.Game;
using HC.Network;
using HC.Scene;
using HC.Utils;
using System;
using System.Threading.Tasks;
using UGS;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Init();
        CatManager.CatInit();
        FurnitureManager.FurnitureInit();
    }
    void Start()
    {
        Bind();
        Config.SetCamera(E_Camera.Default);
    }
    void Bind()
    {
    }
    private void OnDestroy()
    {
    }

    private void Update()
    {
        CatManager.CatUpdate();
    }

    #region event

    #endregion
}
