using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HCButton : Button
{
    private bool isProcessing = false;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable || isProcessing) return;

        base.OnPointerClick(eventData); // �⺻ Ŭ�� ���� ȣ��
        StartCoroutine(HandleClickOnce());
    }

    private IEnumerator HandleClickOnce()
    {
        isProcessing = true;

        yield return DoSomethingAsync();

        isProcessing = false;
    }

    private IEnumerator DoSomethingAsync()
    {
        yield return new WaitForSeconds(0.7f);
    }
}
