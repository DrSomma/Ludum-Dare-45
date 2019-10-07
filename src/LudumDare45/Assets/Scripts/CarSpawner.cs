using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public Transform endPoint;
    public bool stopSpawn;

    public float delayMin;
    public float delayMax;

    private bool carMoveRight;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnCar", delayMin);

        carMoveRight = transform.position.x < endPoint.position.x;
    }

    void spawnCar()
    {
        if (stopSpawn)
            return;

        GameObject carPre = cars[Random.Range(0, cars.Length)];
        //Debug.Log("Spawn Car " + carPre.name);
        GameObject obj = Instantiate(carPre);
        obj.transform.position = transform.position;
        CarMovement movement = obj.GetComponent<CarMovement>();
        if(movement != null)
        {
            movement.endPoint = endPoint.position;
            movement.moveRight = carMoveRight;
            movement.canMove = true ;
        }

        Invoke("spawnCar", Random.Range(delayMin,delayMax));
    }
}
