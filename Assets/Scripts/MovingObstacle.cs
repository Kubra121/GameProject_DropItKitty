using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 3f;

    void Update()
    {
        if (CatController.isCatMoving)
        {
            Vector3 direction = CatController.moveDirection == 1 ? Vector3.left : Vector3.right;
            transform.position += direction * moveSpeed * Time.deltaTime;

            float screenLeft = Camera.main.transform.position.x - 30f;
            float screenRight = Camera.main.transform.position.x + 30f;

            if (transform.position.x < screenLeft || transform.position.x > screenRight)
            {
                Destroy(gameObject);
            }
        }

    }
}
