using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiKids : MonoBehaviour
{
    NavMeshAgent kid;
   
    
    public GameObject returnpoint;
    // Start is called before the first frame update

    void Start()
    {
        kid = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
      
     
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            kid.SetDestination(returnpoint.transform.position);
           
        }
    }
    
}
