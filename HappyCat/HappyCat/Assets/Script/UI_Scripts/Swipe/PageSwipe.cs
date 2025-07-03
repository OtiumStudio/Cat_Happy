using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HC.Game
{
    public class PageSwipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        // pageInfo
        private E_PAGESTATE curPage = E_PAGESTATE.RESTAURANT;
        private E_PAGEDIRECTION moveDir = E_PAGEDIRECTION.NONE;
        readonly int screenWidth = 1080;
        readonly int screenHeight = 1920;
        private Vector3 panelLocation;
        public float percentThreshold = 0.2f;
        public float easing = 0.5f;
        void Start()
        {
            panelLocation = Camera.main.transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            moveDir = E_PAGEDIRECTION.NONE;
        }
        public void OnDrag(PointerEventData data)
        {
            float differenceX = data.pressPosition.x - data.position.x;
            float differenceY = data.pressPosition.y - data.position.y;
            if (differenceX == 0 && differenceY == 0) return;

            if (moveDir.Equals(E_PAGEDIRECTION.NONE))
            {
                if (Mathf.Abs(differenceX) < Mathf.Abs(differenceY)) moveDir = differenceY > 0 ? E_PAGEDIRECTION.UP : E_PAGEDIRECTION.DOWN;
                else moveDir = differenceX > 0 ? E_PAGEDIRECTION.RIGHT : E_PAGEDIRECTION.LEFT;
            }

            Debug.Log($"{moveDir}, {differenceX}, {differenceY}");
            if (moveDir.Equals(E_PAGEDIRECTION.LEFT) || moveDir.Equals(E_PAGEDIRECTION.RIGHT)) OnDrag_Horizontal(differenceX);
            else if (moveDir.Equals(E_PAGEDIRECTION.UP) || moveDir.Equals(E_PAGEDIRECTION.DOWN)) OnDrag_Vertical(differenceY);


        }
        private void OnDrag_Horizontal(float difference)
        {
            Debug.Log("OnDrag_Horizontal");
            if (difference > 0) moveDir = E_PAGEDIRECTION.RIGHT;
            else if (difference < 0) moveDir = E_PAGEDIRECTION.LEFT;

            if (MathF.Abs(difference) > screenWidth) return;
            if (CheckPageMove(curPage)) Camera.main.transform.position = panelLocation - new Vector3(-difference, 0, 0);
        }
        private void OnDrag_Vertical(float difference)
        {
            Debug.Log("OnDrag_Vertical");
            if (difference > 0) moveDir = E_PAGEDIRECTION.UP;
            else if (difference < 0) moveDir = E_PAGEDIRECTION.DOWN;

            if (MathF.Abs(difference) > screenHeight) return;
            if (CheckPageMove(curPage)) Camera.main.transform.position = panelLocation - new Vector3(0, -difference, 0);
        }
        public void OnEndDrag(PointerEventData data)
        {
            if (!CheckPageMove(curPage)) {
                StartCoroutine(SmoothMove(Camera.main.transform.position, panelLocation, 0.01f));
                return;
            }

            float percentage = (data.pressPosition.x - data.position.x) / screenWidth;
            if (Mathf.Abs(percentage) >= percentThreshold)
            {
                Vector3 newLocation = panelLocation;
                if (percentage > 0) newLocation += new Vector3(screenWidth, 0, 0);
                else if (percentage < 0) newLocation += new Vector3(-screenWidth, 0, 0);

                StartCoroutine(SmoothMove(Camera.main.transform.position, newLocation, easing));
                ChangePage();
                panelLocation = newLocation;
            }
            else
            {
                StartCoroutine(SmoothMove(Camera.main.transform.position, panelLocation, easing));
            }
        }

        IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
        {
            if (CheckPageMove(curPage)) yield return null;
            float t = 0f;
            while (t <= 1.0)
            {
                t += Time.deltaTime / seconds;
                Camera.main.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
        }

        private bool CheckPageMove(E_PAGESTATE pageState)
        {
            return Enum.IsDefined(typeof(E_PAGESTATE), pageState + (int)moveDir);
        }
        private void ChangePage()
        {
            curPage += (int)moveDir;
            Debug.Log(curPage);
        }
    }

}
