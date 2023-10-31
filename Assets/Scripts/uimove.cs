using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uimove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //btn.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.6f, -0.8f, 0));
    }

    public void openUrl(string URL)
    {
        Application.OpenURL(URL);
    }
}
