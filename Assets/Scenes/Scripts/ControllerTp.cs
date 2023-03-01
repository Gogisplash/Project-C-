using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTp : MonoBehaviour
{
    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (this.name == "Tp")
        {
            destination = GameObject.Find("TpNewZone").transform.position;
        }
        col.transform.position = destination;
    }
}
