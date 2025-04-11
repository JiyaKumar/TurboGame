using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public Transform carTransform;

    public float speed=4f;
    void Start()
    {

    }

    void Update()
    {
        transform.position-=new Vector3(0,speed*Time.deltaTime,0);
        
    }
}

