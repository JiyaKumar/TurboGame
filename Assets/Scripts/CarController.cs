using System.Collections;
using System.Collections.Generic;// Scripts/CarController.cs
using UnityEngine;
using FlutterUnityIntegration;

public class CarController : MonoBehaviour
{
    public GameObject playerCar;
    private float baseSpeed = 5f;

    void Start()
    {
        UnityMessageManager.Instance.OnMessage += OnFlutterMessage;
    }

    void OnFlutterMessage(string message)
    {
        float accuracy;
        if (float.TryParse(message, out accuracy))
        {
            float speed = baseSpeed * (accuracy / 100f);
            Debug.Log("Received accuracy: " + accuracy + ", Speed: " + speed);
            playerCar.GetComponent<Car_Movement>().speed = speed;

        }
    }

    private void OnDestroy()
    {
        UnityMessageManager.Instance.OnMessage -= OnFlutterMessage;
    }
}
