using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hotbar : MonoBehaviour {
    public List<string> items = new List<string>();
    public TextMeshProUGUI[] slotTexts;
    public Image[] slotBorders;

    public int selectedSlot = 0;

    void Start() {
        UpdateHotbar();
        HighlightSelectedSlot();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
    }

    public void AddItem(string itemName) {
        if (items.Count < slotTexts.Length) {
            items.Add(itemName);
            UpdateHotbar();
        }
    }

    void UpdateHotbar() {
        for (int i = 0; i < slotTexts.Length; i++) {
            if (i < items.Count)
                slotTexts[i].text = items[i];
        }
    }

    void SelectSlot(int index) {
        if (index >= 0 && index < slotTexts.Length) {
            selectedSlot = index;
            HighlightSelectedSlot();
        }
    }

    void HighlightSelectedSlot() {
        for (int i = 0; i < slotBorders.Length; i++) {
            slotBorders[i].color = (i == selectedSlot) ? Color.yellow : Color.white;
        }
    }

    public string GetSelectedItem() {
        if (selectedSlot < items.Count)
            return items[selectedSlot];

        return "";
    }
}