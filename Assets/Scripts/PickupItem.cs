using UnityEngine;

public class HotbarPickup : MonoBehaviour
{
    public string itemName = "Key";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hotbar hotbar = FindObjectOfType<Hotbar>();

            if (hotbar != null)
            {
                hotbar.AddItem(itemName);
                Destroy(gameObject);
            }
        }
    }
}