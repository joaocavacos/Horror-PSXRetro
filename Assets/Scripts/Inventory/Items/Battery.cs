using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, ICollectible
{
    public static event HandleBatteryCollected OnBatteryCollected;
    public delegate void HandleBatteryCollected(ItemData itemData);

    public ItemData batteryData;

    public void Collect()
    {
        Destroy(gameObject);
        OnBatteryCollected?.Invoke(batteryData);
    }

   
}
