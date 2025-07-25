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
    [SerializeField] GameObject topLayer;
    private void Awake()
    {
        UIManager.Init();
        CatManager.CatInit();
        FurnitureManager.FurnitureInit();
        KitchenManager.KitchenInit();

        UIManager.RegisterUI("TopLayout", topLayer).Forget();
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
        CatManager.Close();
        KitchenManager.Close();
    }

    private void Update()
    {
        CatManager.CatUpdate();
        KitchenManager.KitchenUpdate();
    }

    #region event

    #endregion
}
