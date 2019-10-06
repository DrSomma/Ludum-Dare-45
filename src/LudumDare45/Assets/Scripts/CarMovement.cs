using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool canMove = false;
    public bool moveRight = true;
    public bool randomSpeed = false;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    public Vector3 endPoint;
    private float speed;

    void Start()
    {
        if (randomSpeed)
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            speed = maxSpeed;
        }
    }

    void Update()
    {
        if (!canMove)
            return;

        //transform.position = Vector3.Lerp(transform.position, endPoint, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, endPoint) <= 0.5f)
        {
           Destroy(this.gameObject);
        }
    }
}
