using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : Singleton<InterfaceManager>
{
    public TMP_Text subtitleText; 

    public void ShowText(string textToShow)
    {
        subtitleText.text = textToShow;
    }

    public void ClearText()
    {
        subtitleText.text = string.Empty;
    }
}
