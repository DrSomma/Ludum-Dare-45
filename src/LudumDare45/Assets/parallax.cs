using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform myCamera;
    public float speedCoefficient;
    Vector3 lastPos;

    void Update()
    {
        transform.position -= ((lastPos - myCamera.position) * speedCoefficient);
        lastPos = myCamera.position;
    }
}
