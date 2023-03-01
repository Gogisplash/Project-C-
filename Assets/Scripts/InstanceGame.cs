using System.Collections;
using System.Collections.Generic;
using Engine.Utils;
using UnityEngine;

public class InstanceGame : Singleton <InstanceGame>
{
    public GameObject PlayerPrefab;
    public Canvas CanvaStatPrefab;
    public Canvas CanvasInstance;
    public GameObject PlayerInstance;
    // Start is called before the first frame update
    protected new void Awake()
    {
       // LoseButton.OnLoad += Load;
        if (PlayerInstance != null && CanvasInstance != null )
        {
            Destroy(PlayerInstance);
            Destroy(CanvasInstance);
           
        }
        else
        {
            PlayerInstance = Instantiate(PlayerPrefab);
            CanvasInstance = Instantiate(CanvaStatPrefab);
           

        }
       
       
    }
    //Essaie load with chekpoint
    //public void Load(Vector3 pos)
    //{
    //    PlayerInstance.transform.position = pos;    
    //}
}
