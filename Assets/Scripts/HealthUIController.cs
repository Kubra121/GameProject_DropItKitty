using UnityEngine;
using UnityEngine.UI;
public class HealthUIController : MonoBehaviour
{
    public Image healthImage;               // UI’daki Image component
    public Sprite[] heartSprites;          // Kalp resimleri dizisi (0-4 can)

    private int currentHealth;

    void Start()
    {
        currentHealth = heartSprites.Length - 1; // Örneðin 4 can için index 4
        UpdateHeartUI(currentHealth);
    }

    public void UpdateHeartUI(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, heartSprites.Length - 1);
        healthImage.sprite = heartSprites[currentHealth];
    }
}
