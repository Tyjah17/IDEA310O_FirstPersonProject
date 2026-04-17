using UnityEngine;
using TMPro;
using StealthGame;

public class PlayerInteract : MonoBehaviour {
    [Header("References")]
    public Camera playerCamera;
    public Hotbar hotbar;
    public Flashlight flashlight;

    [Header("Interaction Settings")]
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI (Optional)")]
    public TextMeshProUGUI interactText;

    void Update() {
        CheckForInteractable();
    }

    void CheckForInteractable() {
        if (playerCamera == null)
            return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {

            PickupItem pickup = hit.collider.GetComponentInParent<PickupItem>();
            if (pickup != null) {
                if (interactText != null) {
                    interactText.text = "Press E to pick up " + pickup.itemName;
                    interactText.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(interactKey)) {
                    pickup.OnPickup(hotbar, flashlight);
                }
                return;
            }

            Door door = hit.collider.GetComponentInParent<Door>();
            if (door != null) {
                bool hasKey = hotbar.HasItem("Key");
                if (interactText != null) {
                    if (hasKey) {
                        interactText.text = "Press E to open door";
                    } else {
                        interactText.text = "Need a Key to open door...";
                    }

                    interactText.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown(interactKey)) {
                    if (hasKey) {
                        door.OpenDoor();
                    }
                }
                return;
            }
        }
        if (interactText != null) {
            interactText.gameObject.SetActive(false);
        }
    }
}