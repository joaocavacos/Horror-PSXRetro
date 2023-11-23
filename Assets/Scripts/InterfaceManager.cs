using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : Singleton<InterfaceManager>
{
    public TMP_Text subtitleText;

    public Animator crosshairAnimator;

    public void ShowText(string textToShow)
    {
        subtitleText.text = textToShow;
    }

    public void ClearText()
    {
        subtitleText.text = string.Empty;
    }

    public void ChangeCrosshair(string anim, bool state)
    {
        crosshairAnimator.SetBool(anim, state);
    }
}
