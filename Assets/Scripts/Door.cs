using UnityEngine;

public class Door : MonoBehaviour {
    public bool isOpen = false;

    public void OpenDoor() {
        if (isOpen)
            return;
        isOpen = true;
        gameObject.SetActive(false); // disappear
    }
}