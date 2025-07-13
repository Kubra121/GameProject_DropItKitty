using UnityEngine;
using System.Collections;

public class FallDetector : MonoBehaviour
{
    private bool hasScored = false;
    public AudioClip fallSound;
    private AudioSource audioSource;
    public GameObject firePrefab; // Ateþ prefabý (Animasyon içeriyor olmalý)
    public HealthPotionController healthManager;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        healthManager = FindFirstObjectByType<HealthPotionController>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sadece "Ground" etiketi olan nesnelere tepki ver
        if (!hasScored && collision.gameObject.CompareTag("Ground"))
        {
            hasScored = true;
            // Caný 1 azalt
            
                
           
            // Ses çal
            if (fallSound != null)
            {
                AudioSource.PlayClipAtPoint(fallSound, transform.position + Vector3.right * Random.Range(-0.1f, 0.1f));
            }

            if (CompareTag("Candle"))
            {
                // MUM düþerse:
                if (ScoreManagerController.Instance != null)
                {
                    ScoreManagerController.Instance.AddScore(-10);
                    healthManager.TakeDamage(1);
                }

                // Ateþi oluþtur
                if (firePrefab != null)
                {
                    GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);

                    // Animator üzerinden animasyon süresini al ve ateþi yok et
                    Animator anim = fire.GetComponent<Animator>();
                    if (anim != null)
                    {
                        AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(0);
                        if (clips.Length > 0)
                        {
                            float animLength = clips[0].clip.length;
                            Destroy(fire, animLength);
                        }
                        else
                        {
                            Destroy(fire, 1.5f); // Yedek: animasyon süresi alýnamazsa
                        }
                    }
                    else
                    {
                        Destroy(fire, 1.5f); // Animator yoksa yedek
                    }
                }

                // Kediyi yavaþlat
                if (CatController.Instance != null)
                {
                    CatController.Instance.SlowDownTemporarily();
                }
            }
            else
            {
                // Bardak/þiþe düþerse:
                if (ScoreManagerController.Instance != null)
                {
                    ScoreManagerController.Instance.AddScore(10);
                }
            }

            Destroy(gameObject);
        }
    }

    public void TriggerFall()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
        }
    }
}
