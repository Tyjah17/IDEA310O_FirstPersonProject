using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Heart UI")]
    public Hearts[] hearts;

    void Start() {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        UpdateHearts();
        if (currentHealth <= 0)
            Die();
    }

    void UpdateHearts() {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentHealth){
                hearts[i].SetHeartImage(HeartStatus.Full);
            } else {
                hearts[i].SetHeartImage(HeartStatus.Empty);
            }
        }
    }

    void Die() {

    }
}