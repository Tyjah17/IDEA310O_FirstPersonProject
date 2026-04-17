using UnityEngine;
using TMPro;

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

        // ray from center of screen (your reticle)
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {
            // look for PickupItem on hit object or its parent
            PickupItem pickup = hit.collider.GetComponentInParent<PickupItem>();

            if (pickup != null) {
                // show UI text if assigned
                if (interactText != null) {
                    interactText.text = "Press E to pick up " + pickup.itemName;
                    interactText.gameObject.SetActive(true);
                }
                // press E to pick up
                if (Input.GetKeyDown(interactKey)) {
                    pickup.OnPickup(hotbar, flashlight);
                }
                return;
            }
        }
        if (interactText != null) {
            interactText.gameObject.SetActive(false);
        }
    }
}