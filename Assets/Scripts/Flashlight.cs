using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Hotbar hotbar;
    public Light flashlightLight;
    public string flashlightItemName = "Flashlight";
    public KeyCode toggleKey = KeyCode.F;

    private bool isOn = false;

    void Start()
    {
        if (flashlightLight != null)
        {
            flashlightLight.enabled = false;
            isOn = false;
        }
    }

    void Update()
    {
        if (hotbar == null || flashlightLight == null)
            return;

        if (Input.GetKeyDown(toggleKey))
        {
            string selectedItem = hotbar.GetSelectedItem();

            if (selectedItem == flashlightItemName)
            {
                ToggleFlashlight();
            }
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
    }

    public bool IsFlashlightOn()
    {
        return isOn;
    }
}