using UnityEngine;
using System;
using System.Collections;

//default
public class Fadein_out : MonoBehaviour // Panel ������ ������ ���̵��� or ���̵�ƿ�
{
    public bool isFadeIn; // true=FadeIn, false=FadeOut
    public GameObject dropdown_panal; // �������� ������ Panel ������Ʈ
    private Action onCompleteCallback; // FadeIn �Ǵ� FadeOut ������ ������ �Լ�

    void Start()
    {
        if (!dropdown_panal)
        {
            throw new MissingComponentException();
        }

        if (isFadeIn) // Fade In Mode -> �ٷ� �ڷ�ƾ ����
        {
            dropdown_panal.SetActive(true); // Panel Ȱ��ȭ
            StartCoroutine(CoFadeIn());
        }
        else
        {
            dropdown_panal.SetActive(false); // Panel ��Ȱ��ȭ
        }
    }

    public void FadeOut()
    {
        dropdown_panal.SetActive(true); // Panel Ȱ��ȭ
        StartCoroutine(CoFadeOut());
    }

    public void FadeIn()
    {
        dropdown_panal.SetActive(false);
        StartCoroutine(CoFadeIn());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // ���� ��� �ð�
        float fadedTime = 0.5f; // �� �ҿ� �ð�

        while (elapsedTime <= fadedTime)
        {
            dropdown_panal.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dropdown_panal.SetActive(false); // Panel�� ��Ȱ��ȭ
        onCompleteCallback?.Invoke(); // ���Ŀ� �ؾ� �ϴ� �ٸ� �׼��� �ִ� ���(null�� �ƴ�) �����Ѵ�
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; // ���� ��� �ð�
        float fadedTime = 0.5f; // �� �ҿ� �ð�

        while (elapsedTime <= fadedTime)
        {
            dropdown_panal.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke(); // ���Ŀ� �ؾ� �ϴ� �ٸ� �׼��� �ִ� ���(null�� �ƴ�) �����Ѵ�
        yield break;
    }

    public void RegisterCallback(Action callback) // �ٸ� ��ũ��Ʈ���� �ݹ� �׼� ����ϱ� ���� ���
    {
        onCompleteCallback = callback;
    }

}
