using UnityEngine;

public class GhostDamage : MonoBehaviour {
    public int damageAmount = 1;
    public float damageRate = 1f; // damage per second

    private float damageTimer = 0f;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f / damageRate) {
                PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(damageAmount);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            damageTimer = 0f;
    }
}