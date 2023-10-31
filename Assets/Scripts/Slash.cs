using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject came;
    //public GameObject prefab;

    // Start is called before the first frame update
    void Awake()
    {
        
    }
    public void slash()
    {
        RaycastHit hit;
        if (Physics.Raycast(came.transform.position, came.transform.forward, out hit))
        {
            if (hit.transform.tag == "Animal")
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
