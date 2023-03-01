using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    public GameObject Player;
    public string FirstLevel;
    public Transform SpawnPlayer;
    private string saveSeparator = "%VALUE%";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Save();
        }
    }

    public void LoadButton()
    {
        Load();
        SceneManager.LoadScene(FirstLevel);
    }

    public void StartGame()
    {
        Player.transform.position = new Vector3(40,1,135);
        SceneManager.LoadScene(FirstLevel);
    }
    void Save()
    {
        string[] content = new string[]
        {
            Player.transform.position.x.ToString(),
            Player.transform.position.y.ToString(),
            Player.transform.position.z.ToString()
        };
        string SaveString = string.Join(saveSeparator, content);
        File.WriteAllText(Application.dataPath + "/data.txt", SaveString);
        Debug.Log("Save pos ");
    }
    void Load()
    {
        string SaveString = File.ReadAllText(Application.dataPath + "/data.txt");
        string[] content = SaveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);

        Vector3 pos = new Vector3(float.Parse(content[0]), float.Parse(content[1]), float.Parse(content[2]));
        Player.transform.position = pos;
       
    }
}
