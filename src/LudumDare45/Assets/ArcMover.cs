using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMover : MonoBehaviour
{
    /* Variables */
    float count = 0.0f;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 controllPos;
    void Start()
    {
        startPos = transform.position;
        endPos = Vector3.right * 1;
        controllPos = Vector3.up * 1;
        controllPos = startPos + (endPos - startPos) / 2 + Vector3.up * 4.0f;
    }

    void Update()
    {
        if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(startPos, controllPos, count);
            Vector3 m2 = Vector3.Lerp(controllPos, endPos, count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
    }

}
