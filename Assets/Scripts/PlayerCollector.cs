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
        collectible = other.GetComponent<ICollectible>();
        print($"Trigger entered in {collectible}");
    }

    private void OnTriggerExit(Collider other)
    {
        collectible = null;
    }
}
