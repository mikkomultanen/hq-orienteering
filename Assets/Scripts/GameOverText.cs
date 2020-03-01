using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    public string title;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "" + title + "\nScore: " + ScoreSingleton.Instance.Score + "\npress \"space\" to restart";
    }
}
