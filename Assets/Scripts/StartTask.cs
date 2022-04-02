using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTask : MonoBehaviour
{
    public string sceneName;
    public AudioClip transitionSFX;

    public void OpenScene(string sceneName)
    {
        AudioSource.PlayClipAtPoint(transitionSFX, transform.position);
        StartCoroutine(DelaySceneLoad(sceneName));
    }

    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }
}
