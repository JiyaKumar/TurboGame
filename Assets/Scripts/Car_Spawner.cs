using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    public GameObject[] car;
    void Start()
    {
        StartCoroutine(SpawnCars());

    }

    void Update()
    {

    }
    void Cars()
    {
        int rand=Random.Range(0,car.Length);
        float randXPos=Random.Range(-1.8f,1.8f);
        Instantiate(car[rand],new Vector3(randXPos,transform.position.y,transform.position.z),Quaternion.Euler(0,0,90));
       
    }
    IEnumerator SpawnCars()
    {
       
        while(true)
        {
             yield return new WaitForSeconds(3);
             Cars();

        }
    }
}

