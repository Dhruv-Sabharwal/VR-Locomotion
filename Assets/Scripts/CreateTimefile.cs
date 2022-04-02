using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

public class CreateTimefile : MonoBehaviour
{
    string path;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        path = Application.persistentDataPath + "/time.txt";
        Task asyncTask = StoreTime();
    }

    async Task StoreTime()
    {
        // if file does not already exist
        if (!File.Exists(path))
        {
            long time = System.DateTime.Now.Ticks;
            // create a new file when the player enters the menu scene
            using (StreamWriter writer = File.CreateText(path))
            {
                await writer.WriteAsync(sceneName + "\n" + time.ToString() + "\n");
            }
        }
    }
}
