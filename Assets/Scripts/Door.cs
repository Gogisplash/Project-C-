using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animation open;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        open= GetComponent<Animation>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isOpen == false && Input.GetKey(KeyCode.E))
        {
         open.Play();
         isOpen = true; 
         }
            
        
    }
}
