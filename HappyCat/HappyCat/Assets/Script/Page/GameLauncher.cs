using HC.Scene;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private void Awake()
    {
        var sceneLoader = new SceneLoader();
        sceneLoader.Initialize();
        sceneLoader.SceneLoad(SceneKind.LoginScene);
    }
}
