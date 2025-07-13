using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;

    public HealthUIController healthUIController; // UI script referansý

    void Start()
    {
        currentHealth = maxHealth;
        

        healthUIController = FindFirstObjectByType<HealthUIController>();
        healthUIController.UpdateHeartUI(currentHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUIController.UpdateHeartUI(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUIController.UpdateHeartUI(currentHealth);
    }

    void Die()
    {
        // Ölüme özgü iþlemler (örneðin animasyon, hareketi durdur)
    }
}
