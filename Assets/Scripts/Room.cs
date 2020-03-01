using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    public AudioClip announcement;
    public AudioClip done;

    private GameState gameState;
    private TextMeshProUGUI text;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null) {
            Debug.Log("Entered: " + gameObject.name);
            gameState.OnRoomTriggered(this);
        }
    }
}
