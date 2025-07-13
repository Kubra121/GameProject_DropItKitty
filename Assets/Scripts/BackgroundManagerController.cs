using System.Collections.Generic;
using UnityEngine;


public class BackgroundManagerController : MonoBehaviour
{
    public GameObject backgroundPrefab; // Tek bir prefab kullan�lacak
    public int numberOfBackgrounds = 3; // Ka� tane par�a olaca��
    public float backgroundWidth = 20f; // Prefab geni�li�i
    public Transform cameraTransform;
    public float backgroundY = -4.46f;
    public float startX = 3.03f;
    private List<GameObject> backgrounds = new List<GameObject>();

    void Start()
    {
        // Ba�lang��ta backgroundlar� s�rayla yerle�tir
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            Vector3 spawnPos = new Vector3(i * backgroundWidth, backgroundY, 1f);
            GameObject bg = Instantiate(backgroundPrefab, spawnPos, Quaternion.identity);
            backgrounds.Add(bg);
        }
    }

    void Update()
    {
        if (CatController.isCatMoving)
        {
            foreach (GameObject bg in backgrounds)
            {
                // E�er background kameran�n soluna fazlas�yla ge�tiyse, en sona ta��
                if (bg.transform.position.x + backgroundWidth < cameraTransform.position.x)
                {
                    // En �ndeki background'un pozisyonuna g�re yeni pozisyon belirle
                    float maxX = GetMaxXPosition();
                    Vector3 newPos = new Vector3(maxX + backgroundWidth, backgroundY, 1f);
                    bg.transform.position = newPos;
                }
            }
        }
        
    }

    float GetMaxXPosition()
    {
        float max = float.MinValue;
        foreach (GameObject bg in backgrounds)
        {
            if (bg.transform.position.x > max)
            {
                max = bg.transform.position.x;
            }
        }
        return max;
    }
}
