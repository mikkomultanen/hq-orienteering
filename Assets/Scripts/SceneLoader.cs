using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            ScoreSingleton.Instance.Reset();
            SceneManager.LoadScene(nextScene);
        }
    }
}
