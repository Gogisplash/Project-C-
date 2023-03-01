using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CheckPoint : MonoBehaviour
{
    //public GameObject Player;
    bool IsOnCheckPoint = false;
    private string saveSeparator = "%VALUE%";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnCheckPoint == true)
        {
            Save();
        }
    }

    void Save()
    {
       
        string[] content = new string[]
        {
            InstanceGame.Instance.PlayerInstance.transform.position.x.ToString(),
            InstanceGame.Instance.PlayerInstance.transform.position.y.ToString(),
            InstanceGame.Instance.PlayerInstance.transform.position.z.ToString()
        };
        string SaveString = string.Join(saveSeparator, content);
        File.WriteAllText(Application.dataPath + "/data.txt", SaveString);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsOnCheckPoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsOnCheckPoint = false;
        }
    }
}
