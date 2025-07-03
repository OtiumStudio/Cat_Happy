using Cysharp.Threading.Tasks;
using HC.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace HC.Utils
{
    class WorkQueueData
    {
        public string uiAddress;
        public GameObject parent;
        public GameObject ui;
    }
    class UIManager
    {
        private static Canvas root;

        private static Dictionary<string, GameObject> activeUIs = new();
        private static Queue<WorkQueueData> workQueue = new Queue<WorkQueueData>();
        private static bool isProcess = false;

        private static GameObject loadingPopup;
        private static string loadingName = "Loading_Popup";

        public static void Init()
        {
            root = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        public static async UniTaskVoid RegisterUI(string uiAddress, GameObject ui)
        {
            if (workQueue.Any(data => data.uiAddress == uiAddress)) return;
            workQueue.Enqueue(new WorkQueueData { uiAddress = uiAddress, ui = ui } );
            TryNextPopup();

            //    if (activeUIs.ContainsKey(uiAddress))
            //    {
            //        activeUIs[uiAddress].SetActive(true);
            //        activeUIs[uiAddress].GetComponent<UIBase>()?.OnOpen();
            //        return;
            //    }

            //activeUIs[uiAddress] = ui;
            //await ui.GetComponent<UIBase>().OnOpen();
        }

        public static async void ShowUI(string uiAddress, GameObject parent = null)
        {
            if (workQueue.Any(data => data.uiAddress == uiAddress)) return;
            workQueue.Enqueue(new WorkQueueData { uiAddress = uiAddress, parent = parent });
            TryNextPopup();

            //if (activeUIs.ContainsKey(uiAddress))
            //{
            //    activeUIs[uiAddress].SetActive(true);
            //    activeUIs[uiAddress].GetComponent<UIBase>()?.OnOpen();
            //    return;
            //}



            //var ob = await LoadAddressableManager.LoadPopup<GameObject>(uiAddress);

            //if (ob != null)
            //{
            //    GameObject uiInstance = GameObject.Instantiate(ob, parent == null ? root.transform : parent.transform);
            //    activeUIs[uiAddress] = uiInstance;
            //    uiInstance.GetComponent<UIBase>()?.OnOpen();
            //}
            //else
            //{
            //    Debug.LogError($"[UIManager] Failed to load UI: {uiAddress}");
            //}
        }

        private static async void TryNextPopup()
        {
            if (workQueue.Count <= 0) return;
            isProcess = true;

            try
            {
                await OpenLoadingPopup();
                
                var nextPopup = workQueue.Dequeue();

                if (activeUIs.ContainsKey(nextPopup.uiAddress))
                {
                    activeUIs[nextPopup.uiAddress].SetActive(true);
                    activeUIs[nextPopup.uiAddress].GetComponent<UIBase>()?.OnOpen();
                    ProcessEnd();
                    return;
                }

                if (nextPopup.ui != null)
                {
                    activeUIs[nextPopup.uiAddress] = nextPopup.ui;
                    await nextPopup.ui.GetComponent<UIBase>().OnOpen();
                }else
                {
                    var ob = await LoadAddressableManager.LoadPopup<GameObject>(nextPopup.uiAddress);
                    if (ob != null)
                    {
                        GameObject uiInstance = GameObject.Instantiate(ob, nextPopup.parent == null ? root.transform : nextPopup.parent.transform);
                        activeUIs[nextPopup.uiAddress] = uiInstance;
                        uiInstance.GetComponent<UIBase>()?.OnOpen();
                    }
                    else
                    {
                        Debug.LogError($"[UIManager] Failed to load UI: {nextPopup.uiAddress}");
                    }
                }
                ProcessEnd();

            }
            catch(Exception e)
            {
                HC.Utils.HC_Debug.Log(e);
                ProcessEnd();
            }
        }
        private static void ProcessEnd()
        {
            isProcess = false;
            CloseLoadingPopup();
            TryNextPopup();
        }

        public void HideUI(string uiAddress)
        {
            if (activeUIs.TryGetValue(uiAddress, out GameObject ui))
            {
                ui.SetActive(false);
                //ui.GetComponent<UIBase>()?.OnClose();
            }
        }

        public void CloseUI(string uiAddress)
        {
            if (activeUIs.TryGetValue(uiAddress, out GameObject ui))
            {
                ui.GetComponent<UIBase>()?.OnClose();
                GameObject.Destroy(ui);
                activeUIs.Remove(uiAddress);
            }
        }

        #region loading

        private static async UniTask OpenLoadingPopup()
        {
            var loading = await LoadAddressableManager.LoadPopup<GameObject>(loadingName);
            loadingPopup = GameObject.Instantiate(loading, root.transform);
            await loadingPopup.GetComponent<UIBase>().OnOpen();
        }
        private static void CloseLoadingPopup()
        {
            if (loadingPopup == null) return;

            loadingPopup.GetComponent<UIBase>().OnClose();
            GameObject.Destroy(loadingPopup);
            loadingPopup = null;
        }
        #endregion
    }
}
