using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigger : MonoBehaviour
{
    float time;
    void Update()
    {
        transform.localScale = Vector3.one *  (1 + time);
        time += Time.deltaTime;
        if (time >5f)
        {
            time = 5f;
        }
    }
}
