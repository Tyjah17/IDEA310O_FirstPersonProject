using UnityEngine;

public class PickupItem : MonoBehaviour {
    public enum ItemType {
        Generic,
        Flashlight,
        Key
    }

    [Header("Item Info")]
    public string itemName = "Item";
    public ItemType itemType = ItemType.Generic;
    public bool destroyOnPickup = true;

    public void OnPickup(Hotbar hotbar, Flashlight flashlight) {
        if (hotbar != null) {
            hotbar.AddItem(itemName);
        }

        HandleSpecialPickup(flashlight);

        if (destroyOnPickup)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    void HandleSpecialPickup(Flashlight flashlight) {
        switch (itemType) {
            case ItemType.Flashlight:
                if (flashlight != null) {
                    flashlight.UnlockFlashlight();
                }
                break;
        }
    }
}