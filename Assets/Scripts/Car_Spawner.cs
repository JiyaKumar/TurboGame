using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnInterval = 2f;
    public float spawnY = 6f; // Just above the screen
    public float[] lanePositions = new float[] { -2.5f, 0f, 2.5f }; // Adjust based on your lanes

    private void Start()
    {
        InvokeRepeating("SpawnCar", 1f, spawnInterval);
    }

    void SpawnCar()
    {
        float randomX = lanePositions[Random.Range(0, lanePositions.Length)];
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);
        Instantiate(carPrefab, spawnPos, Quaternion.identity);
    }
}
