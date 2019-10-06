using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool canMove = false;
    public float speed = 5f;
    public bool moveRight = true;
    public Vector3 endPoint;

    void Start()
    {
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
