using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caffeine : MonoBehaviour
{
    public float amount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("BLAA");
            player.AddCaffeine(amount);
        }
    }
}
