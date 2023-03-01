using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class LoseButton : MonoBehaviour
{
    private string saveSeparator = "%VALUE%";
    Vector3 pos;

    // public static event Action<Vector3> OnLoad;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadToLastSave()
    {
        string SaveString = File.ReadAllText(Application.dataPath + "/data.txt");
        string[] content = SaveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);
        pos = new Vector3(float.Parse(content[0]), float.Parse(content[1]), float.Parse(content[2]));
        SceneManager.LoadScene("Menu");

        //OnLoad?.Invoke(pos);
        //InstanceGame.Instance.PlayerInstance.transform.position = pos;
        

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
   
}
