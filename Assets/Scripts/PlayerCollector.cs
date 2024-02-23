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
        InputManager.OnUse -= HandleUsable;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ICollectible newCollectible))
        {
            collectible = newCollectible;
            InterfaceManager.Instance.ShowText(collectible.Name);
            print($"Entered {collectible.Name} trigger");
        }
        else if(other.TryGetComponent(out IUsable newUsable))
        {
            usable = newUsable;
        }
        InterfaceManager.Instance.ChangeCrosshair("Open", true);
    }

    private void OnTriggerExit(Collider other)
    {
        InterfaceManager.Instance.ClearText();
        InterfaceManager.Instance.ChangeCrosshair("Open", false);
        collectible = null;
        usable = null;
    }
}
