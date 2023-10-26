using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight energy")]
    [SerializeField] private float energyPercentage;
    [SerializeField] private float maxEnergyPercentage = 100f;
    [SerializeField] private float energyConsumption;

    [Header("UI")]
    [SerializeField] private Light flashLight;

    [SerializeField] private bool flashToggle;

    private void Awake()
    {
        energyPercentage = maxEnergyPercentage;
    }

    private void OnEnable()
    {
        InputManager.OnFlashlightToggle += HandleFlashlightToggle;
    }

    private void OnDisable()
    {
        InputManager.OnFlashlightToggle -= HandleFlashlightToggle;
    }

    void Update()
    {
        if (flashToggle) ToggleOn();
    }

    private void HandleFlashlightToggle()
    {
        flashToggle = !flashToggle;

        if (!flashToggle) ToggleOff();
    }

    void ToggleOn()
    {
        InventoryItem batteryItem = Inventory.Instance.GetItemByID(0);

        if (energyPercentage > 0)
        {
            flashLight.enabled = true;
            energyPercentage -= energyConsumption * Time.deltaTime;
        }
        else if (energyPercentage <= 0) 
        {
            if(batteryItem != null) //Reload Flashlight
            {
                Inventory.Instance.RemoveItem(batteryItem.itemData);
                energyPercentage = maxEnergyPercentage;
            }
            else 
            {
                ToggleOff();
            }
        }
    }

    void ToggleOff()
    {
        flashLight.enabled = false;
    }
}
