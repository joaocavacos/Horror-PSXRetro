using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private ICollectible collectible;

    private void OnEnable()
    {
        InputManager.OnCollect += HandleCollector;
    }

    private void OnDisable()
    {
        InputManager.OnCollect -= HandleCollector;
    }

    private void HandleCollector()
    {
        if(collectible != null) 
        {
            collectible.Collect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<ICollectible>(out collectible))
        {
            InterfaceManager.Instance.ShowText(collectible.Name);
            print($"Entered {collectible.Name} trigger");
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        collectible = null;
        if(collectible == null)
        {
            InterfaceManager.Instance.ClearText();
        }
    }
}
