using UnityEngine;
using System.Collections;
public class CatController : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastJumpTime = -1f;
    public float jumpBoostWindow = 1f; // İki zıplama arası süre sınırı
    public float boostedJumpForce = 8f; // Normalden daha yüksek zıplama

    public int maxJumps = 3;
    private int jumpCount = 0;

    public static bool isCatMoving = false;
    public static int moveDirection = 0;//-1: left, 1: right, 0: not moving

    public static CatController Instance;
    private float originalSpeed;
    private bool isSlowed = false;

    private HealthPotionController healthPotionController;

    void Awake()
    {
        Instance = this;
        originalSpeed = moveSpeed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthPotionController = GetComponent<HealthPotionController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Hareket yönünü ve hareket durumunu belirle
        if (moveX > 0.01f)
        {
            moveDirection = 1;
            isCatMoving = true;
        }
        else if (moveX < -0.01f)
        {
            moveDirection = -1;
            isCatMoving = true;
        }
        else
        {
            moveDirection = 0;
            isCatMoving = false;
        }


        // Karakter yönü ve koşma animasyonu
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            animator.SetBool("IsRunning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }




        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            float currentTime = Time.time;


            // Eğer yerdeyse → ilk zıplama
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isGrounded = false; // havaya çıktı
                jumpCount = 0;
                animator.SetBool("IsJumping", true);
            }
            // Eğer kısa süre içinde ikinci kez basılmışsa → boosted jump
            else if (currentTime - lastJumpTime <= jumpBoostWindow && jumpCount < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, boostedJumpForce);
                jumpCount++;
                animator.SetBool("IsJumping", true);
            }

            lastJumpTime = currentTime;
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Önceden olan diğer kontrollerin yanına ekle
        if(collision.gameObject.CompareTag("Candle"))
        {
            if (healthPotionController != null)
            {
                healthPotionController.TakeDamage(1);
            }
        }

        // Çarptığı objede FallDetector varsa, düşmesini tetikle
        FallDetector fallDetector = collision.gameObject.GetComponent<FallDetector>();
        if (fallDetector != null)
        {
            fallDetector.TriggerFall();
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Shelf") || collision.gameObject.CompareTag("Table"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Shelf") || collision.gameObject.CompareTag("Table"))
        {
            isGrounded = false;
        }
    }



    public void SlowDownTemporarily()
    {
        if (!isSlowed)
        {
            isSlowed = true;
            moveSpeed *= 0.5f; // Hızı yarıya düşür
            StartCoroutine(RestoreSpeedAfterDelay(3f)); // 3 saniye sonra eski hıza dön
        }
    }

    IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = originalSpeed;
        isSlowed = false;
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HealthPotion"))
        {
            if (healthPotionController != null)
            {
                healthPotionController.Heal(1);
            }
            Destroy(other.gameObject);
        }
    }







   


}
