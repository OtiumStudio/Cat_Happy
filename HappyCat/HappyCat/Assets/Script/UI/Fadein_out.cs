using UnityEngine;
using System;
using System.Collections;

//default
public class Fadein_out : MonoBehaviour // Panel 불투명도 조절해 페이드인 or 페이드아웃
{
    public bool isFadeIn; // true=FadeIn, false=FadeOut
    public GameObject dropdown_panal; // 불투명도를 조절할 Panel 오브젝트
    private Action onCompleteCallback; // FadeIn 또는 FadeOut 다음에 진행할 함수

    void Start()
    {
        if (!dropdown_panal)
        {
            throw new MissingComponentException();
        }

        if (isFadeIn) // Fade In Mode -> 바로 코루틴 시작
        {
            dropdown_panal.SetActive(true); // Panel 활성화
            StartCoroutine(CoFadeIn());
        }
        else
        {
            dropdown_panal.SetActive(false); // Panel 비활성화
        }
    }

    public void FadeOut()
    {
        dropdown_panal.SetActive(true); // Panel 활성화
        StartCoroutine(CoFadeOut());
    }

    public void FadeIn()
    {
        dropdown_panal.SetActive(false);
        StartCoroutine(CoFadeIn());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 0.5f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            dropdown_panal.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dropdown_panal.SetActive(false); // Panel을 비활성화
        onCompleteCallback?.Invoke(); // 이후에 해야 하는 다른 액션이 있는 경우(null이 아님) 진행한다
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 0.5f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            dropdown_panal.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke(); // 이후에 해야 하는 다른 액션이 있는 경우(null이 아님) 진행한다
        yield break;
    }

    public void RegisterCallback(Action callback) // 다른 스크립트에서 콜백 액션 등록하기 위해 사용
    {
        onCompleteCallback = callback;
    }

}
