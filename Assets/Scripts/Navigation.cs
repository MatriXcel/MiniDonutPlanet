using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour {

    IEnumerator DelaySceneLoad(string SceneToLoad)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ButtonPress(string SceneToLoad)
    {
        StartCoroutine(DelaySceneLoad(SceneToLoad));
    }
}
