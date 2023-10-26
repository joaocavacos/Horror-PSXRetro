using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, ICollectible, IUsable
{
    public delegate void HandleBatteryCollected(ItemData itemData);
    public static event HandleBatteryCollected OnBatteryCollected;

    public delegate void HandleBatteryUsed(ItemData itemData);
    public static event HandleBatteryUsed OnBatteryUsed;

    public ItemData batteryData;

    public void Collect()
    {
        Destroy(gameObject);
        OnBatteryCollected?.Invoke(batteryData);
    }

    public void Use()
    {
        OnBatteryUsed?.Invoke(batteryData);
    }
}
