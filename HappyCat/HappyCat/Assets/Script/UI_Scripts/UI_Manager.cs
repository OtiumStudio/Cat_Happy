using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    UI_Button Popup_UI;
    Fadein_out FadeCTR;

    [SerializeField]
    public GameObject popup_ui;
    public GameObject dropdown_panal; // 불투명도를 조절할 Panel 오브젝트

    private void Awake()
    {

        Popup_UI = GetComponent<UI_Button>();
        FadeCTR = GetComponent<Fadein_out>();

    }



    public void dropDown()
    {
        if (dropdown_panal.activeSelf == true)
        {
            FadeCTR.FadeOut();
        }
        else
        {
            FadeCTR.FadeIn();
        }
    }
    public void alert()
    { }
    public void calender()
    { }
    public void colection_book()
    { Popup_UI.Open(); }
    public void star()
    { }
    public void mail()
    { }
}
