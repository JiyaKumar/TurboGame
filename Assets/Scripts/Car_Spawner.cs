using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    public GameObject carPrefab;

    void Start()
    {
        float xPos = 1.0f; // Adjust as needed
        Instantiate(carPrefab, new Vector3(xPos, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
