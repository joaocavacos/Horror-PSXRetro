using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private ICollectible collectible;
    private IUsable usable;

    private void OnEnable()
    {
        InputManager.OnCollect += HandleCollector;
        InputManager.OnUse += HandleUsable;
    }

    private void OnDisable()
    {
        InputManager.OnCollect -= HandleCollector;
        InputManager.OnUse += HandleUsable;
    }

    private void HandleCollector()
    {
        if(collectible != null) 
        {
            collectible.Collect();
            InterfaceManager.Instance.ClearText();
            InterfaceManager.Instance.ChangeCrosshair("Open", false);
        }
    }

    private void HandleUsable()
    {
        if (usable != null)
        {
            usable.Use();
            InterfaceManager.Instance.ChangeCrosshair("Open", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out collectible))
        {
            InterfaceManager.Instance.ShowText(collectible.Name);
            InterfaceManager.Instance.ChangeCrosshair("Open", true);
            print($"Entered {collectible.Name} trigger");
        }
        else if(other.TryGetComponent(out usable))
        {
            InterfaceManager.Instance.ChangeCrosshair("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InterfaceManager.Instance.ClearText();
        InterfaceManager.Instance.ChangeCrosshair("Open", false);
        collectible = null;
        usable = null;
    }
}
