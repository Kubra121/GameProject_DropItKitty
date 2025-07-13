using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Transform catTransform;
    public float escapeDistance = 2f;
    public float escapeForce = 6f;
    public float waitBeforeEscape = 0.3f;
    public float returnDelay = 0.5f;
    public float moveSpeed = 5f;
    public float forwardOffset = 1f;

    private Vector3 initialPosition;
    private Vector3 returnTargetPosition;
    private bool isEscaping = false;
    private bool isReturning = false;
    private float escapeTimer = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // Kuþun parent objesi varsa kaldýrýyoruz, böylece baðýmsýz hareket eder
        transform.parent = null;
    }

    void Update()
    {
        float distanceToCat = Vector2.Distance(transform.position, catTransform.position);

        if (!isEscaping && !isReturning && distanceToCat < escapeDistance)
        {
            escapeTimer += Time.deltaTime;

            if (escapeTimer >= waitBeforeEscape)
            {
                StartEscape();
            }
        }

        if (isReturning)
        {
            // Sabit hýzla geri dön
            Vector3 newPos = Vector3.MoveTowards(transform.position, returnTargetPosition, moveSpeed * Time.deltaTime);
            transform.position = newPos;

            if (Vector3.Distance(transform.position, returnTargetPosition) < 0.05f)
            {
                isReturning = false;
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
                escapeTimer = 0f;
                initialPosition = transform.position;
            }
        }
        else if (!isEscaping)
        {
            // Normalde ileri doðru sabit hýzda hareket et
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (!isEscaping && !isReturning && distanceToCat >= escapeDistance)
        {
            escapeTimer = 0f;
        }
    }

    void StartEscape()
    {
        isEscaping = true;
        escapeTimer = 0f;

        rb.gravityScale = 1.2f;
        rb.linearVelocity = new Vector2(0f, escapeForce);

        Invoke(nameof(StartReturn), returnDelay);
    }

    void StartReturn()
    {
        isEscaping = false;
        isReturning = true;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // Geri dönüþ hedefini ileri kaydýr
        returnTargetPosition = new Vector3(initialPosition.x + forwardOffset, initialPosition.y, initialPosition.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            Destroy(gameObject);
        }
    }

}
