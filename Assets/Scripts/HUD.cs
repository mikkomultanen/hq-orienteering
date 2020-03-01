using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public TextMeshProUGUI mission;
    private GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        score.text = "" + ScoreSingleton.Instance.Score;
        mission.text = "Mission: " + gameState.Mission();
        time.text = ScoreSingleton.Instance.TimeRemainingString();
    }
}
