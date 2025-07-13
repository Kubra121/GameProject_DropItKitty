using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float spriteWidth;
    private Transform cameraTransform;

    void Start()
    {
        // Sprite geni�li�ini al
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;

        // Ana kameray� referans al
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (CatController.isCatMoving)
        {
            // Kedi hangi y�ne gidiyorsa o y�ne kayd�r
            Vector3 direction = CatController.moveDirection == 1 ? Vector3.left : Vector3.right;
            transform.position += direction * scrollSpeed * Time.deltaTime;

            // Zemin ekran�n d���na ��kt�ysa yeniden konumland�r
            if (CatController.moveDirection == 1 && transform.position.x + spriteWidth < cameraTransform.position.x - (spriteWidth / 2))
            {
                // Sa�dan tekrar gelsin
                float newX = transform.position.x + spriteWidth * 2f;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else if (CatController.moveDirection == -1 && transform.position.x - spriteWidth > cameraTransform.position.x + (spriteWidth / 2))
            {
                // Soldan tekrar gelsin
                float newX = transform.position.x - spriteWidth * 2f;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }

    }
}
