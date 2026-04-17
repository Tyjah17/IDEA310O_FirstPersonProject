using UnityEngine;

public class Flashlight : MonoBehaviour {
    public Hotbar hotbar;

    [Header("Held Flashlight")]
    public GameObject flashlightObject;
    public Light flashlight;

    [Header("Settings")]
    public string itemName = "Flashlight";
    public KeyCode toggleKey = KeyCode.F;

    private bool hasFlashlight = false;
    private bool isOn = false;

    void Start() {
        if (flashlightObject != null)
            flashlightObject.SetActive(false);
        if (flashlight != null)
            flashlight.enabled = false;
    }

    void Update() {
        if (hotbar == null)
            return;

        UpdateHeldFlashlightVisibility();

        if (hasFlashlight && Input.GetKeyDown(toggleKey)) {
            string selectedItem = hotbar.GetSelectedItem();

            if (selectedItem == itemName) {
                ToggleFlashlight();
            }
        }
    }

    public void UnlockFlashlight() {
        hasFlashlight = true;
    }

    void ToggleFlashlight() {
        isOn = !isOn;

        if (flashlight != null) {
            flashlight.enabled = isOn;
        }
    }

    void UpdateHeldFlashlightVisibility() {
        if (flashlightObject == null)
            return;

        string selectedItem = hotbar.GetSelectedItem();
        bool shouldShow = hasFlashlight && selectedItem == itemName;

        flashlightObject.SetActive(shouldShow);

        if (!shouldShow && flashlight != null) {
            flashlight.enabled = false;
            isOn = false;
        }
    }

    public bool IsFlashlightOn() {
        return isOn;
    }
}