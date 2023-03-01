using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinCondition : MonoBehaviour
{
    [SerializeField] int NbrChildrens = 0;
    [SerializeField] bool PlayerIsOnEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NbrChildrens == 3 && PlayerIsOnEnd == true) 
        {
            SceneManager.LoadScene("WinPanel");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Children")
        {
            NbrChildrens += 1;
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerIsOnEnd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerIsOnEnd = false;
        }
    }
}
