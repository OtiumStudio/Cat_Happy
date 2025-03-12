
using HC.Event;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HC.Scene
{
    public enum SceneKind { LoginScene, GameScene, MainUIScene };
    public class SceneLoader
    {
        private bool isLoad;
        private float progress = 1;
        public bool IsLoad { get => isLoad; }
        public float Progress
        {
            get => progress; private set
            {
                progress = value;
                SceneEvent.ServiceEvents.Emit(new SceneLoadProgressEvent(progress));
            }
        }

        public static SceneLoader GetSceneLoader { get; private set; }

        public void Initialize()
        {
            GetSceneLoader = this;
            isLoad = false;
        }

        public async void SceneLoad(SceneKind scenekind, bool isProgress = false)
        {
            if (isLoad)
            {
                Debug.Log("Already loading process logic");
                return;
            }

            isLoad = true;
            SceneEvent.ServiceEvents.Emit(new SceneLoadStartEvent(scenekind));

            if(isProgress) SceneLoadCoroutine(scenekind);
            else
            {
                isLoad = false;
                Progress = 1.0f;
                SceneManager.LoadScene(scenekind.ToString());
                SceneEvent.ServiceEvents.Emit(new SceneLoadEndEvent());
            }
        }

        private IEnumerator SceneLoadCoroutine(SceneKind scenekind)
        {
            Progress = 0.0f;
            yield return null;

            var unLoadResourceop = Resources.UnloadUnusedAssets();
            yield return new WaitForSeconds(0.2f);

            if (unLoadResourceop.isDone != true)
                yield return null;

            string sceneName = scenekind.ToString();

            if (sceneName != "")
            {
                var operation = SceneManager.LoadSceneAsync(sceneName);
                operation.allowSceneActivation = false;
                operation.completed += (op) =>
                {
                };

                while (operation != null && operation.progress < 0.9f)
                {
                    Progress = operation.progress;
                    yield return null;
                }

                Progress = operation.progress;

                operation.allowSceneActivation = true;
                while (operation != null && !operation.isDone)
                    yield return null;

                //var objs = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
                //foreach (var obj in objs)
                //{
                //    var launchers = obj.GetComponentsInChildren<BaseLauncher>();
                //    foreach (var launcher in launchers)
                //        yield return new WaitUntil(() => { return launcher.IsStart; });
                //}
                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

                Progress = 1.0f;
                yield return new WaitForSeconds(0.5f);
                isLoad = false;
            }

            SceneEvent.ServiceEvents.Emit(new SceneLoadEndEvent());
        }
    }
}