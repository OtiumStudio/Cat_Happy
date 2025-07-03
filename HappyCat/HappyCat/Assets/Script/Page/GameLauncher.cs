using HC.Scene;
using HC.Utils;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private void Awake()
    {
        Init();

        var sceneLoader = new SceneLoader();
        sceneLoader.Initialize();
        sceneLoader.SceneLoad(SceneKind.LoginScene);
    }

    private void Init()
    {
    }
}
