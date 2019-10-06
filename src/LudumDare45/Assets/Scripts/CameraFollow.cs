using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerMovment player;
    public float smoothSpeed = 2f;
    public float offsetX = 4;
    public float offsetY = 0;
    public Transform maxPosLeft;
    public Transform maxPosRight;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = player.gameObject.transform;
    }

    void LateUpdate()
    {
        float xPos = target.position.x + offsetX * (player.lookRight ? 1 : -1);

        Vector3 newPos = new Vector3(
                Mathf.Clamp(xPos, maxPosLeft.position.x,maxPosRight.position.x),
                target.position.y + offsetY,
                transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, newPos, smoothSpeed *  Time.deltaTime);
        transform.position = smoothPos;
    }
}
