using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;


public class ActivateTarget : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public AudioClip completeTaskSFX;
    public GameObject doneMenuPrefab;

    public float xMin = -4;
    public float xMax = 4;
    public float zForward = 5;
    public float zMax = 100;
    float yPoistion = 0;
    int maxChildCount = 19;

    string path;
    string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/time.txt";
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (GameObject.FindGameObjectWithTag("TargetParent").transform.childCount == maxChildCount)
        {
            GameObject doneMenu = Instantiate(doneMenuPrefab, new Vector3(0, 0.5f, zMax), transform.rotation);

            long startTime = long.Parse(PlayerPrefs.GetString("startTime"));
            long endTime = long.Parse(PlayerPrefs.GetString("currTime"));

            System.TimeSpan elapsedTime = new System.TimeSpan(endTime - startTime);
            int minutes = elapsedTime.TotalMinutes < 0 ? 0 : Mathf.CeilToInt((int) elapsedTime.TotalMinutes);
            int seconds = Mathf.CeilToInt((int) elapsedTime.TotalSeconds % 60);

            doneMenu.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Task Completed in \n " + minutes + " min " + seconds + " seconds";
        }


        if (other.CompareTag("Player"))
        {
            // save the time

            Task asyncTask = StoreTime();


            Spwan();

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.green;

            AudioSource.PlayClipAtPoint(completeTaskSFX, transform.position);

            Destroy(gameObject.GetComponent<CapsuleCollider>());
               
        }

    }

    private void Spwan()
    {
        float xPosition = Random.Range(xMin, xMax);

        if (transform.position.z + zForward < zMax)
        {
            GameObject nextCylinder = Instantiate(cylinderPrefab, new Vector3(xPosition, yPoistion, transform.position.z + zForward), transform.rotation);
            nextCylinder.transform.SetParent(GameObject.FindGameObjectWithTag("TargetParent").transform);
        }


    }

    async Task StoreTime()
    {
        // append the time to the file
        using (StreamWriter sw = File.AppendText(path))
        {
            long time = System.DateTime.Now.Ticks;
            string timeString;

            if (GameObject.FindGameObjectWithTag("TargetParent").transform.childCount == 0)
            {
                timeString = sceneName + "\n" + time.ToString() + "\n";
                PlayerPrefs.SetString("startTime", time.ToString());
            }
            else
            {
                timeString = time.ToString() + "\n";
                PlayerPrefs.SetString("currTime", time.ToString());
            }
            await sw.WriteAsync(timeString);
        }
    }


}
