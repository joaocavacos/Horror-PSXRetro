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

    private void Start()
    {
        InputManager.Instance.OnFlashlightToggle += HandleFlashlightToggle;
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
        //UIManager.Instance.UpdateEnergy(energyPercentage);

        //Item batteryItem = Inventory.Instance.GetItemByID(0);

        if (energyPercentage > 0)
        {
            flashLight.enabled = true;
            energyPercentage -= energyConsumption * Time.deltaTime;
        }
        else if (energyPercentage <= 0) //Reload Flashlight && batteryItem != null
        {
            energyPercentage = maxEnergyPercentage;
            //Inventory.Instance.RemoveItem(batteryItem, 1);
        }
        else if (energyPercentage <= 0) //Toggle off if no battery && batteryItem == null
        {
            ToggleOff();
        }

    }

    void ToggleOff()
    {
        flashLight.enabled = false;
    }
}
