using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    public float minGap = 5f; // Her engelin bir öncekinin ne kadar ilerisinde doğacağı
    public float spawnStartX = 12f; // İlk engelin doğacağı X pozisyonu
    public Transform catTransform; // Kedi objesini buraya at (Inspector'dan)

    private float lastObstacleX;

    public GameObject[] tableItemPrefabs; // Bardak ve şişe prefab'ları



    void Start()
    {
        lastObstacleX = spawnStartX;
    }


    void Update()
    {
        // Eğer kedi ilerlediyse ve yeni bir engel oluşturulması gerekiyorsa
        if (catTransform.position.x + 20f > lastObstacleX)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclesPrefabs.Length);
        GameObject prefabToSpawn = obstaclesPrefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(lastObstacleX, 0f, 0f);
        GameObject obstacleInstance = null;

        if (prefabToSpawn.name.ToLower().Contains("table"))
        {
            spawnPosition.y = -3.588494f;
            obstacleInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Masa için daha düşük yükseklik
            SpawnItemAboveObstacle(spawnPosition + new Vector3(0f, 1.28f, 0f), obstacleInstance.transform);
        }
        else if (prefabToSpawn.name.ToLower().Contains("shelf"))
        {
            spawnPosition.y = Random.Range(-1.45f, 0.35f);
            obstacleInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Raf için daha düşük yükseklik (şişeyi biraz yukarıya al)
            SpawnItemAboveObstacle(spawnPosition + new Vector3(0f, 1.28f, 0f), obstacleInstance.transform);
        }

        float randomGap = Random.Range(minGap, minGap + 2f);
        lastObstacleX += randomGap;
    }




    void SpawnItemAboveObstacle(Vector3 basePosition, Transform parent)
    {
        int itemIndex = Random.Range(0, tableItemPrefabs.Length);
        GameObject itemPrefab = tableItemPrefabs[itemIndex];
        GameObject itemInstance = Instantiate(itemPrefab, basePosition, Quaternion.identity, parent);

        string itemName = itemPrefab.name.ToLower();
        string parentName = parent.name.ToLower();

        float targetY = basePosition.y;

        if (parentName.Contains("table"))
        {
            // Sabit konumlar
            if (itemName.Contains("cup"))
                targetY = -2.71068f;
            else if (itemName.Contains("bottle"))
                targetY = -2.188578f;
            else if (itemName.Contains("candle"))
                targetY = -2.5f;
            else if (itemName.Contains("glass"))
                targetY = -2.71f;
        }
        else if (parentName.Contains("shelf"))
        {
            float shelfY = parent.position.y;

            // Dinamik konum hesaplama: raf yüksekliği + item offset
            if (itemName.Contains("cup"))
                targetY = shelfY + 0.64f; 
            else if (itemName.Contains("bottle"))
                targetY = shelfY + 1.14f; 
            else if (itemName.Contains("candle"))
                targetY = shelfY + 0.9f;
            else if (itemName.Contains("glass"))
                targetY = shelfY + 0.7f;

        }
       
        // X pozisyonuna rastgelelik ekle
        itemInstance.transform.position = new Vector3(
            basePosition.x + Random.Range(-0.15f, 0.15f),
            targetY,
            basePosition.z
        );

        Rigidbody2D rb = itemInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (itemInstance.GetComponent<FallDetector>() == null)
            itemInstance.AddComponent<FallDetector>();
    }





}
