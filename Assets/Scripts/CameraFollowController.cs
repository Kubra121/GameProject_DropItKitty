using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public Transform target;           // Kedinin transform'u
    public Vector2 threshold = new Vector2(2f, 1f); // Kameranýn hareket etmeden önceki toleransý
    public float smoothSpeed = 3f;     // Takip yumuþaklýðý

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Threshold'u ekran boyutuna göre ayarla (isteðe baðlý)
        threshold = CalculateThreshold();
    }

    private void Update()
    {
        Vector3 difference = target.position - transform.position;
        Vector3 newPosition = transform.position;

        if (Mathf.Abs(difference.x) >= threshold.x)
        {
            newPosition.x = target.position.x - Mathf.Sign(difference.x) * threshold.x;
        }

        if (Mathf.Abs(difference.y) >= threshold.y)
        {
            newPosition.y = target.position.y - Mathf.Sign(difference.y) * threshold.y;
        }

        // Kamerayý yumuþakça hareket ettir
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed * Time.deltaTime);
    }

    private Vector3 CalculateThreshold()
    {
        Camera cam = Camera.main;
        Vector3 aspect = new Vector3(cam.aspect * cam.orthographicSize, cam.orthographicSize);
        return new Vector2(aspect.x * 0.5f, aspect.y * 0.5f); // Ekranýn ortasýndan %50 pay býrak
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 thresh = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(thresh.x * 2, thresh.y * 2, 1));
    }
}
